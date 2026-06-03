window.$docsify = window.$docsify || {};
window.$docsify.plugins = [].concat(
  window.$docsify.plugins || [],
  function siteFooter(hook) {
    hook.doneEach(function () {
      const section = document.querySelector(".markdown-section");
      if (!section) {
        return;
      }

      section.querySelector(".site-footer")?.remove();

      const footer = document.createElement("div");
      footer.className = "site-footer";
      footer.innerHTML = `
<p align="center">
  <a href="https://github.com/IrishBruse/WaylandDotnet">GitHub</a> &bull;
  <a href="https://www.nuget.org/packages/WaylandDotnet">NuGet</a>
</p>`;
      section.appendChild(footer);
    });
  },
);
