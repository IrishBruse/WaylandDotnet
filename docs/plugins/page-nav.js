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

      var full = source.textContent.replace(/\s+/g, " ").trim();
      var dot = full.lastIndexOf(".");
      if (dot !== -1) {
        return full.slice(dot + 1);
      }

      return full;
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
      list.className = "page-nav-sections";

      headings.forEach(function (heading) {
        if (heading.classList.contains("decleration")) {
          return;
        }
        appendHeadingLink(list, heading);
      });

      return list.children.length > 0 ? list : null;
    }

    function enhancePageNav() {
      var nav = document.querySelector(".app-nav");
      if (!nav) {
        return;
      }

      var section = document.querySelector(".markdown-section");
      if (!section) {
        return;
      }

      nav.querySelectorAll("p").forEach(function (p) {
        if (
          !p.classList.contains("page-nav-label") &&
          p.textContent.trim().toLowerCase() === "outline"
        ) {
          p.remove();
        }
      });

      nav.querySelectorAll("ul").forEach(function (ul) {
        ul.remove();
      });
      nav.querySelectorAll(".page-nav-title").forEach(function (el) {
        el.remove();
      });

      var protocolNav = buildProtocolNav(section);
      if (protocolNav) {
        nav.appendChild(protocolNav);
        return;
      }

      var h1 = section.querySelector("h1");
      if (h1) {
        var titleLink = document.createElement("a");
        titleLink.className = "page-nav-title";
        titleLink.href = h1.id
          ? "#" + h1.id
          : location.hash.split("?")[0] || "#/";
        titleLink.textContent = h1.textContent.trim();
        nav.appendChild(titleLink);
      }

      var sectionNav = buildSectionNav(section);
      if (sectionNav) {
        nav.appendChild(sectionNav);
      }
    }

    function scheduleEnhance() {
      enhancePageNav();
      setTimeout(enhancePageNav, 0);
      setTimeout(enhancePageNav, 50);
      setTimeout(enhancePageNav, 200);
    }

    hook.doneEach(scheduleEnhance);
  },
);
