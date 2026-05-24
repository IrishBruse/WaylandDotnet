// Docsify clears sidebar .active when ?id= is in the URL (scrollIntoView only
// knows about navbar TOC keys). Re-highlight the page link from the route path.
(function () {
  function fixSidebarActive(vm) {
    var sidebarNav = document.querySelector(".sidebar-nav");
    if (!sidebarNav || !vm.route.query.id) {
      return;
    }

    var router = vm.router;
    var hash = decodeURI(router.toURL(vm.route.path));
    var links = Array.from(sidebarNav.querySelectorAll("a"));
    var target;

    links
      .sort(function (a, b) {
        return b.getAttribute("href").length - a.getAttribute("href").length;
      })
      .forEach(function (a) {
        var href = decodeURI(a.getAttribute("href")).split("?")[0];
        var node = a.parentNode;

        if (hash.indexOf(href) === 0 && !target) {
          target = a;
          node.classList.add("active");
        } else {
          node.classList.remove("active");
        }
      });
  }

  window.$docsify.plugins = [].concat(window.$docsify.plugins || [], function (hook, vm) {
    hook.doneEach(function () {
      fixSidebarActive(vm);
    });

    hook.ready(function () {
      fixSidebarActive(vm);
      window.addEventListener("hashchange", function () {
        fixSidebarActive(vm);
      });
    });
  });
})();
