window.$docsify = window.$docsify || {};
window.$docsify.plugins = [].concat(
  window.$docsify.plugins || [],
  function pageNav(hook) {
    function navTypeFromHeading(heading) {
      if (heading.classList.contains("interface")) {
        return "interface";
      }
      if (heading.classList.contains("request")) {
        return "request";
      }
      if (heading.classList.contains("event")) {
        return "event";
      }
      if (heading.classList.contains("enum")) {
        return "enum";
      }
      return null;
    }

    function linkTextFromHeading(heading, source) {
      var members = source.querySelectorAll(".method, .event, .enum");
      for (var i = 0; i < members.length; i++) {
        var text = members[i].textContent.trim();
        if (text) {
          return text;
        }
      }

      if (heading.classList.contains("decleration")) {
        var full = source.textContent.replace(/\s+/g, " ").trim();
        var dot = full.lastIndexOf(".");
        if (dot !== -1) {
          return full.slice(dot + 1);
        }
        return full;
      }

      var titleSpan = source.querySelector("span");
      if (titleSpan) {
        return titleSpan.textContent.replace(/\s+/g, " ").trim();
      }

      return heading.textContent.replace(/\s+/g, " ").trim();
    }

    function linkFromHeading(heading) {
      var source = heading.querySelector("a[id], a[href*='id=']");
      if (!source) {
        return null;
      }

      var anchor = document.createElement("a");
      anchor.href = source.getAttribute("href");
      anchor.textContent = linkTextFromHeading(heading, source);

      var type = navTypeFromHeading(heading);
      if (type) {
        anchor.classList.add("nav-" + type);
      } else if (!heading.classList.contains("decleration")) {
        anchor.classList.add("nav-section");
      }

      return anchor;
    }

    function appendHeadingLink(parent, heading) {
      var anchor = linkFromHeading(heading);
      if (!anchor) {
        return;
      }

      var item = document.createElement("li");
      item.appendChild(anchor);
      parent.appendChild(item);
    }

    function buildProtocolNav(section) {
      var interfaces = section.querySelectorAll("h2.decleration.interface");
      if (!interfaces.length) {
        return null;
      }

      var list = document.createElement("ul");
      list.className = "page-nav-custom";

      interfaces.forEach(function (iface) {
        var ifaceItem = document.createElement("li");
        var ifaceLink = linkFromHeading(iface);
        if (!ifaceLink) {
          return;
        }
        ifaceItem.appendChild(ifaceLink);

        var subList = document.createElement("ul");
        var node = iface.nextElementSibling;
        while (
          node &&
          !(node.tagName === "H2" && node.classList.contains("decleration"))
        ) {
          if (node.tagName === "H3" && node.classList.contains("decleration")) {
            appendHeadingLink(subList, node);
          }
          node = node.nextElementSibling;
        }

        if (subList.children.length > 0) {
          ifaceItem.appendChild(subList);
        }

        list.appendChild(ifaceItem);
      });

      return list;
    }

    function buildSectionNav(section) {
      var headings = section.querySelectorAll("h2");
      if (!headings.length) {
        return null;
      }

      var list = document.createElement("ul");
      list.className = "page-nav-sections page-nav-custom";

      headings.forEach(function (heading) {
        if (heading.classList.contains("decleration")) {
          return;
        }
        appendHeadingLink(list, heading);
      });

      return list.children.length > 0 ? list : null;
    }

    function isEnhancedLink(link) {
      return (
        link.classList.contains("page-nav-title") ||
        link.classList.contains("nav-section") ||
        link.classList.contains("nav-interface") ||
        link.classList.contains("nav-request") ||
        link.classList.contains("nav-event") ||
        link.classList.contains("nav-enum")
      );
    }

    function needsEnhance(nav) {
      if (!nav) {
        return false;
      }

      var links = nav.querySelectorAll("a");
      for (var i = 0; i < links.length; i++) {
        if (!isEnhancedLink(links[i])) {
          return true;
        }
      }

      return links.length > 0 && !nav.hasAttribute("data-page-nav-ready");
    }

    function setNavReady(nav, ready) {
      if (ready) {
        nav.setAttribute("data-page-nav-ready", "");
      } else {
        nav.removeAttribute("data-page-nav-ready");
      }
    }

    var enhancing = false;

    function enhancePageNav() {
      if (enhancing) {
        return;
      }

      var nav = document.querySelector(".app-nav");
      if (!nav) {
        return;
      }

      var section = document.querySelector(".markdown-section");
      if (!section) {
        return;
      }

      if (!needsEnhance(nav)) {
        return;
      }

      enhancing = true;
      setNavReady(nav, false);

      nav.querySelectorAll("p, .page-nav-label").forEach(function (el) {
        el.remove();
      });

      nav.querySelectorAll("ul").forEach(function (ul) {
        ul.remove();
      });
      nav.querySelectorAll(".page-nav-title").forEach(function (el) {
        el.remove();
      });

      var hasContent = false;

      var protocolNav = buildProtocolNav(section);
      if (protocolNav) {
        nav.appendChild(protocolNav);
        hasContent = true;
        setNavReady(nav, true);
        enhancing = false;
        return;
      }

      var h1 = section.querySelector("h1");
      if (h1) {
        var titleSource = h1.querySelector("a[href]");
        var titleLink = document.createElement("a");
        titleLink.className = "page-nav-title";
        titleLink.href = titleSource
          ? titleSource.getAttribute("href")
          : location.hash.split("?")[0] || "#/";
        titleLink.textContent = h1.textContent.trim();
        nav.appendChild(titleLink);
        hasContent = true;
      }

      var sectionNav = buildSectionNav(section);
      if (sectionNav) {
        nav.appendChild(sectionNav);
        hasContent = true;
      }

      setNavReady(nav, hasContent);
      enhancing = false;
    }

    function ensureNavObserver() {
      var nav = document.querySelector(".app-nav");
      if (!nav || nav._pageNavObserving) {
        return;
      }

      nav._pageNavObserving = true;
      new MutationObserver(function () {
        if (needsEnhance(nav)) {
          enhancePageNav();
        }
      }).observe(nav, { childList: true, subtree: true });
    }

    function scheduleEnhance() {
      enhancePageNav();
      ensureNavObserver();
      requestAnimationFrame(function () {
        enhancePageNav();
        requestAnimationFrame(enhancePageNav);
      });
    }

    hook.beforeEach(function () {
      var nav = document.querySelector(".app-nav");
      if (nav) {
        setNavReady(nav, false);
      }
    });

    hook.doneEach(scheduleEnhance);
    hook.ready(function () {
      scheduleEnhance();
    });
  },
);
