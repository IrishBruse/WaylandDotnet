#!/bin/sh
# Make wayland-2 a nested river with a free window-manager slot.
# Leftover river sessions and any previous copy of this example are stopped.
# The compositor that owns the current session display is left running.
set -eu

runtime="${XDG_RUNTIME_DIR:-/run/user/$(id -u)}"
session_display="${WAYLAND_DISPLAY:-}"
nested_display=wayland-2
script_dir=$(CDPATH= cd -- "$(dirname "$0")" && pwd)

main_pid=""
main_display=""
fallback_pid=""
fallback_display=""

for lock in "$runtime"/wayland-*.lock; do
    [ -e "$lock" ] || continue
    base=$(basename "$lock" .lock)
    pid=$(fuser "$lock" 2>/dev/null | awk '{print $1}') || true
    [ -n "${pid:-}" ] || continue
    comm=$(ps -o comm= -p "$pid" | awk '{print $1}')
    if [ "$comm" != "river" ]; then
        if [ -z "$main_pid" ] || [ "$base" = "$session_display" ]; then
            main_pid=$pid
            main_display=$base
        fi
    elif [ "$base" = "$session_display" ] && [ -z "$fallback_pid" ]; then
        fallback_pid=$pid
        fallback_display=$base
    fi
done

if [ -z "$main_pid" ]; then
    main_pid=$fallback_pid
    main_display=$fallback_display
fi

if [ -z "$main_pid" ]; then
    echo "could not find a Wayland compositor to nest in" >&2
    exit 1
fi

if [ "$main_display" = "$nested_display" ]; then
    echo "the session compositor already owns $nested_display" >&2
    exit 1
fi

echo "keeping $main_display (pid $main_pid)"

is_protected() {
    local pid=$1
    local parent
    while :; do
        [ "$pid" = "$main_pid" ] && return 0
        [ "$pid" -le 1 ] && return 1
        parent=$(ps -o ppid= -p "$pid" 2>/dev/null | tr -d ' ') || return 1
        [ -n "$parent" ] || return 1
        pid=$parent
    done
}

leftover_displays=""
for lock in "$runtime"/wayland-*.lock; do
    [ -e "$lock" ] || continue
    base=$(basename "$lock" .lock)
    [ "$base" = "$main_display" ] && continue
    pid=$(fuser "$lock" 2>/dev/null | awk '{print $1}') || true
    [ -n "${pid:-}" ] || continue
    comm=$(ps -o comm= -p "$pid" | awk '{print $1}')
    if [ "$base" = "$nested_display" ] && [ "$comm" = "river" ]; then
        continue
    fi
    if [ "$comm" = "river" ]; then
        leftover_displays="$leftover_displays $base"
    fi
done

on_leftover_display() {
    local disp=$1
    local item
    for item in $leftover_displays; do
        [ "$item" = "$disp" ] && return 0
    done
    return 1
}

stop_pid() {
    local pid=$1
    local comm
    [ "$pid" -le 1 ] && return 0
    [ "$pid" = "$$" ] && return 0
    [ "$pid" = "$PPID" ] && return 0
    is_protected "$pid" && return 0
    comm=$(ps -o comm= -p "$pid" 2>/dev/null | awk '{print $1}') || return 0
    [ -n "$comm" ] || return 0
    echo "stopping $comm (pid $pid)"
    kill -TERM "$pid" 2>/dev/null || true
}

for proc in /proc/[0-9]*; do
    pid=${proc#/proc/}
    [ -r "$proc/environ" ] || continue
    disp=$(tr '\0' '\n' < "$proc/environ" 2>/dev/null | sed -n 's/^WAYLAND_DISPLAY=//p' | head -n 1) 2>/dev/null || true
    [ -n "${disp:-}" ] || continue
    on_leftover_display "$disp" || continue
    stop_pid "$pid"
done

nested_river=$(fuser "$runtime/$nested_display.lock" 2>/dev/null | awk '{print $1}') || true
for pid in $(pgrep -x river || true); do
    [ "$pid" = "${nested_river:-}" ] && continue
    is_protected "$pid" && continue
    stop_pid "$pid"
done

sleep 0.4

for proc in /proc/[0-9]*; do
    pid=${proc#/proc/}
    [ -r "$proc/environ" ] || continue
    disp=$(tr '\0' '\n' < "$proc/environ" 2>/dev/null | sed -n 's/^WAYLAND_DISPLAY=//p' | head -n 1) 2>/dev/null || true
    [ -n "${disp:-}" ] || continue
    on_leftover_display "$disp" || continue
    is_protected "$pid" && continue
    [ "$pid" = "$$" ] && continue
    kill -KILL "$pid" 2>/dev/null || true
done

for pid in $(pgrep -x river || true); do
    [ "$pid" = "${nested_river:-}" ] && continue
    is_protected "$pid" && continue
    kill -KILL "$pid" 2>/dev/null || true
done

for pid in $(pgrep -x Xwayland || true); do
    is_protected "$pid" && continue
    echo "stopping Xwayland (pid $pid)"
    kill -TERM "$pid" 2>/dev/null || true
done

sleep 0.2

for pid in $(pgrep -x Xwayland || true); do
    is_protected "$pid" && continue
    kill -KILL "$pid" 2>/dev/null || true
done

for disp in $leftover_displays; do
    lock="$runtime/$disp.lock"
    holder=$(fuser "$lock" 2>/dev/null | awk '{print $1}') || true
    if [ -n "${holder:-}" ]; then
        echo "leaving $disp (still held by pid $holder)" >&2
        continue
    fi
    rm -f "$runtime/$disp" "$runtime/$disp.lock"
done

# River allows one window manager. Drop any previous example so the next
# client can bind. This does not stop river or the session compositor.
for pid in $(pgrep -x RiverWindowMana || true); do
    stop_pid "$pid"
done
for pid in $(pgrep -x dotnet || true); do
    cmd=$(tr '\0' ' ' < /proc/"$pid"/cmdline 2>/dev/null) || continue
    case "$cmd" in
        *RiverWindowManager.csproj*) stop_pid "$pid" ;;
    esac
done
sleep 0.3
for pid in $(pgrep -x RiverWindowMana || true); do
    is_protected "$pid" && continue
    kill -KILL "$pid" 2>/dev/null || true
done

nested_lock="$runtime/$nested_display.lock"
holder=$(fuser "$nested_lock" 2>/dev/null | awk '{print $1}') || true
holder_comm=""
if [ -n "${holder:-}" ]; then
    holder_comm=$(ps -o comm= -p "$holder" | awk '{print $1}')
fi
if [ "$holder_comm" = "river" ]; then
    echo "river pid $holder on $nested_display"
    exit 0
fi
if [ -n "${holder:-}" ]; then
    echo "$nested_display is in use by $holder_comm (pid $holder)" >&2
    exit 1
fi
rm -f "$runtime/$nested_display" "$nested_lock"

# addSocketAuto starts at wayland-0. Hold every lower socket the session
# compositor does not already own so this river binds wayland-2.
if [ "$main_display" != "wayland-0" ]; then
    exec 3>>"$runtime/wayland-0.lock"
    flock -n 3 || true
fi
if [ "$main_display" != "wayland-1" ]; then
    exec 4>>"$runtime/wayland-1.lock"
    flock -n 4 || true
fi

cd "$script_dir"
mkdir -p "$script_dir/../../.tmp"
log_dir=$(CDPATH= cd -- "$script_dir/../../.tmp" && pwd)
# sleep keeps river alive. The example is the window manager, started separately
# with WAYLAND_DISPLAY=wayland-2. Close the placeholder fds in the child so
# river does not keep those lower sockets locked.
if [ "$main_display" != "wayland-0" ] && [ "$main_display" != "wayland-1" ]; then
    setsid -f env WAYLAND_DISPLAY="$main_display" river -no-xwayland -log-level info \
        -c "sleep infinity" >"$log_dir/river-wm.log" 2>&1 < /dev/null 3>&- 4>&-
elif [ "$main_display" != "wayland-0" ]; then
    setsid -f env WAYLAND_DISPLAY="$main_display" river -no-xwayland -log-level info \
        -c "sleep infinity" >"$log_dir/river-wm.log" 2>&1 < /dev/null 3>&-
elif [ "$main_display" != "wayland-1" ]; then
    setsid -f env WAYLAND_DISPLAY="$main_display" river -no-xwayland -log-level info \
        -c "sleep infinity" >"$log_dir/river-wm.log" 2>&1 < /dev/null 4>&-
else
    setsid -f env WAYLAND_DISPLAY="$main_display" river -no-xwayland -log-level info \
        -c "sleep infinity" >"$log_dir/river-wm.log" 2>&1 < /dev/null
fi

i=0
bound=""
while [ "$i" -lt 50 ]; do
    pid=$(fuser "$nested_lock" 2>/dev/null | awk '{print $1}') || true
    if [ -n "${pid:-}" ]; then
        comm=$(ps -o comm= -p "$pid" | awk '{print $1}')
        if [ "$comm" = "river" ]; then
            bound=$pid
            break
        fi
    fi
    i=$((i + 1))
    sleep 0.1
done

if [ "$main_display" != "wayland-0" ]; then
    exec 3>&- 2>/dev/null || true
    holder=$(fuser "$runtime/wayland-0.lock" 2>/dev/null | awk '{print $1}') || true
    if [ -z "${holder:-}" ] && [ ! -S "$runtime/wayland-0" ]; then
        rm -f "$runtime/wayland-0.lock"
    fi
fi
if [ "$main_display" != "wayland-1" ]; then
    exec 4>&- 2>/dev/null || true
    holder=$(fuser "$runtime/wayland-1.lock" 2>/dev/null | awk '{print $1}') || true
    if [ -z "${holder:-}" ] && [ ! -S "$runtime/wayland-1" ]; then
        rm -f "$runtime/wayland-1.lock"
    fi
fi

if [ -z "$bound" ]; then
    echo "river did not bind $nested_display" >&2
    exit 1
fi

echo "river pid $bound on $nested_display"
