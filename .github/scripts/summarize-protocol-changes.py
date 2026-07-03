#!/usr/bin/env python3
"""Build PR title and body from protocol XML changes for update-scanner."""

from __future__ import annotations

import os
import re
import subprocess
import sys
import xml.etree.ElementTree as ET
from dataclasses import dataclass, field
from pathlib import Path

PROTOCOLS_DIR = Path("WaylandDotnet.Scanner/Protocols")
BODY_PATH = Path(os.environ.get("PR_BODY_PATH", ".github/pr-body.md"))
TITLE_MAX = 256


@dataclass
class InterfaceChanges:
    version_bump: tuple[int, int] | None = None
    new: bool = False
    new_requests: list[str] = field(default_factory=list)
    new_events: list[str] = field(default_factory=list)
    new_enums: list[str] = field(default_factory=list)
    enum_entries: list[tuple[str, str]] = field(default_factory=list)
    deprecated_entries: list[tuple[str, str, str]] = field(default_factory=list)
    doc_updates: list[tuple[str, str]] = field(default_factory=list)


@dataclass
class FileChanges:
    rel_path: str
    namespace: str
    protocol_name: str
    interfaces: dict[str, InterfaceChanges] = field(default_factory=dict)


def run_git(*args: str) -> str:
    result = subprocess.run(
        ["git", *args],
        capture_output=True,
        text=True,
        check=True,
    )
    return result.stdout


def changed_xml_files() -> list[str]:
    output = run_git("diff", "--name-only", "HEAD", "--", str(PROTOCOLS_DIR))
    return sorted(line for line in output.splitlines() if line.endswith(".xml"))


def read_head(path: str) -> str | None:
    try:
        return run_git("show", f"HEAD:{path}")
    except subprocess.CalledProcessError:
        return None


def read_worktree(path: str) -> str:
    return Path(path).read_text(encoding="utf-8")


def normalize_text(text: str | None) -> str:
    if not text:
        return ""
    return re.sub(r"\s+", " ", text.strip())


def description_text(element: ET.Element) -> str:
    node = element.find("description")
    if node is None:
        return ""
    return normalize_text("".join(node.itertext()))


def child_map(parent: ET.Element, tag: str) -> dict[str, ET.Element]:
    return {child.get("name", ""): child for child in parent.findall(tag) if child.get("name")}


def compare_interfaces(
    old_iface: ET.Element | None,
    new_iface: ET.Element,
) -> InterfaceChanges:
    changes = InterfaceChanges(new=old_iface is None)

    if old_iface is not None:
        old_version = int(old_iface.get("version", "1"))
        new_version = int(new_iface.get("version", "1"))
        if old_version != new_version:
            changes.version_bump = (old_version, new_version)

    for tag, bucket, label in (
        ("request", changes.new_requests, "request"),
        ("event", changes.new_events, "event"),
        ("enum", changes.new_enums, "enum"),
    ):
        old_items = child_map(old_iface, tag) if old_iface is not None else {}
        new_items = child_map(new_iface, tag)
        for name, item in new_items.items():
            if name not in old_items:
                bucket.append(name)
                continue
            old_desc = description_text(old_items[name])
            new_desc = description_text(item)
            if old_desc and new_desc and old_desc != new_desc:
                changes.doc_updates.append((label, name))

        if tag == "enum":
            for enum_name, enum in new_items.items():
                old_enum = old_items.get(enum_name)
                old_entries = child_map(old_enum, "entry") if old_enum is not None else {}
                for entry_name, entry in child_map(enum, "entry").items():
                    if entry_name not in old_entries:
                        changes.enum_entries.append((enum_name, entry_name))
                        continue
                    deprecated = entry.get("deprecated-since")
                    old_deprecated = old_entries[entry_name].get("deprecated-since")
                    if deprecated and deprecated != old_deprecated:
                        changes.deprecated_entries.append((enum_name, entry_name, deprecated))

    return changes


def analyze_file(rel_path: str) -> FileChanges | None:
    old_xml = read_head(rel_path)
    new_xml = read_worktree(rel_path)
    if old_xml == new_xml:
        return None

    rel = Path(rel_path)
    namespace = rel.parts[-2].lower() if len(rel.parts) >= 2 else "unknown"
    protocol_name = rel.stem

    old_root = ET.fromstring(old_xml) if old_xml else None
    new_root = ET.fromstring(new_xml)

    file_changes = FileChanges(
        rel_path=rel_path,
        namespace=namespace,
        protocol_name=protocol_name,
    )

    old_interfaces = (
        {iface.get("name", ""): iface for iface in old_root.findall("interface")}
        if old_root is not None
        else {}
    )
    for iface in new_root.findall("interface"):
        name = iface.get("name", "")
        if not name:
            continue
        iface_changes = compare_interfaces(old_interfaces.get(name), iface)
        if (
            iface_changes.new
            or iface_changes.version_bump
            or iface_changes.new_requests
            or iface_changes.new_events
            or iface_changes.new_enums
            or iface_changes.enum_entries
            or iface_changes.deprecated_entries
            or iface_changes.doc_updates
        ):
            file_changes.interfaces[name] = iface_changes

    return file_changes


def protocol_label(file_changes: FileChanges) -> str:
    return f"{file_changes.namespace} `{file_changes.protocol_name}`"


def build_title(file_changes: list[FileChanges]) -> str:
    highlights: list[str] = []
    for file_change in file_changes:
        for iface_name, iface in file_change.interfaces.items():
            if iface.version_bump:
                old, new = iface.version_bump
                highlights.append(f"{iface_name} v{old}->{new}")
            elif iface.new:
                highlights.append(f"new {iface_name}")

    labels = [protocol_label(item) for item in file_changes]
    if len(labels) == 1:
        prefix = f"Sync {labels[0]} protocol"
    elif len(labels) <= 3:
        prefix = f"Sync {', '.join(labels)} protocols"
    else:
        prefix = f"Sync {len(labels)} protocols from upstream"

    if highlights:
        detail = ", ".join(highlights[:4])
        if len(highlights) > 4:
            detail += ", ..."
        title = f"{prefix}: {detail}"
    else:
        title = prefix

    return title[:TITLE_MAX]


def render_interface(name: str, changes: InterfaceChanges) -> list[str]:
    lines: list[str] = []
    heading = f"#### `{name}`"
    if changes.version_bump:
        old, new = changes.version_bump
        heading += f" (v{old} -> v{new})"
    lines.append(heading)

    if changes.new:
        lines.append("- New interface.")

    if changes.version_bump and not changes.new:
        _, new = changes.version_bump
        lines.append(f"- Interface version bumped to **{new}**.")

    for request in changes.new_requests:
        lines.append(f"- **New `{request}` request**.")
    for event in changes.new_events:
        lines.append(f"- **New `{event}` event**.")
    for enum_name in changes.new_enums:
        lines.append(f"- **New `{enum_name}` enum**.")
    for enum_name, entry_name in changes.enum_entries:
        lines.append(f"- **New `{entry_name}` entry** on `{enum_name}` enum.")
    for enum_name, entry_name, since in changes.deprecated_entries:
        lines.append(
            f"- **`{entry_name}`** on `{enum_name}` enum deprecated since v{since}."
        )
    for kind, item_name in changes.doc_updates:
        lines.append(f"- **`{item_name}` {kind}** — documentation updated.")

    lines.append("")
    return lines


def changed_artifact_files() -> list[str]:
    output = run_git("diff", "--name-only", "HEAD")
    return [line for line in output.splitlines() if line]


def render_body(file_changes: list[FileChanges], artifact_files: list[str]) -> str:
    repo = os.environ.get("GITHUB_REPOSITORY", "IrishBruse/WaylandDotnet")
    sha = os.environ.get("GITHUB_SHA", "")
    run_id = os.environ.get("GITHUB_RUN_ID", "")

    lines = [
        "Automated update from "
        f"[update-scanner](https://github.com/{repo}/actions/workflows/update-scanner.yml).",
        "",
        f"Regenerated bindings with `WaylandDotnet.Scanner` from commit `{sha}` "
        f"(`dotnet run -- download` and `dotnet run` in `WaylandDotnet.Scanner/` per "
        f"[CONTRIBUTING.md](https://github.com/{repo}/blob/main/CONTRIBUTING.md)).",
        "",
    ]
    if run_id:
        lines.append(
            f"Workflow run: https://github.com/{repo}/actions/runs/{run_id}"
        )
        lines.append("")

    lines.extend(["## Protocol changes", ""])

    if file_changes:
        for file_change in file_changes:
            lines.append(
                f"### {protocol_label(file_change)} (`{file_change.rel_path}`)"
            )
            lines.append("")
            for iface_name, iface in sorted(file_change.interfaces.items()):
                lines.extend(render_interface(iface_name, iface))
    else:
        lines.append("No protocol XML changes detected.")
        lines.append("")

    xml_files = [path for path in artifact_files if path.endswith(".xml")]
    binding_files = [
        path
        for path in artifact_files
        if path.startswith("WaylandDotnet/Protocols/") and path.endswith(".cs")
    ]
    doc_files = [
        path for path in artifact_files if path.startswith("docs/Protocols/")
    ]
    other_files = [
        path
        for path in artifact_files
        if path not in xml_files and path not in binding_files and path not in doc_files
    ]

    lines.extend(["## Generated artifacts", ""])
    lines.append("| Area | Files |")
    lines.append("| --- | --- |")
    if xml_files:
        lines.append(f"| Source XML | {', '.join(f'`{path}`' for path in xml_files)} |")
    if binding_files:
        preview = binding_files[:8]
        suffix = ", ..." if len(binding_files) > 8 else ""
        lines.append(
            "| Bindings | "
            + ", ".join(f"`{path}`" for path in preview)
            + suffix
            + " |"
        )
    if doc_files:
        preview = doc_files[:8]
        suffix = ", ..." if len(doc_files) > 8 else ""
        lines.append(
            "| Docs | "
            + ", ".join(f"`{path}`" for path in preview)
            + suffix
            + " |"
        )
    if other_files:
        lines.append(
            "| Other | "
            + ", ".join(f"`{path}`" for path in other_files[:8])
            + (" |" if len(other_files) <= 8 else ", ... |")
        )
    lines.append("")

    lines.extend(
        [
            "## Test plan",
            "",
            "- [x] `dotnet build --configuration Release`",
            "- [x] `dotnet test --configuration Release --no-build`",
            "",
        ]
    )

    return "\n".join(lines)


def main() -> int:
    xml_files = changed_xml_files()
    file_changes = [
        change
        for path in xml_files
        if (change := analyze_file(path)) is not None
    ]
    artifact_files = changed_artifact_files()

    title = build_title(file_changes) if file_changes else "Update protocol bindings from upstream"
    body = render_body(file_changes, artifact_files)

    BODY_PATH.parent.mkdir(parents=True, exist_ok=True)
    BODY_PATH.write_text(body, encoding="utf-8")

    commit_message = title if len(title) <= 72 else "Update protocol bindings from upstream"

    print(f"title={title}")
    print(f"commit_message={commit_message}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
