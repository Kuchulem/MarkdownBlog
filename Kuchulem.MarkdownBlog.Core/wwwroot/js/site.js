// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

for (let article of document.querySelectorAll(".article.card")) {
    article.onclick = e => {
        let target = e.target;

        if (target.tagName.toLowerCase() == "a")
            return true;

        while (!target.classList.contains("article")) {
            target = target.parentNode;
        }

        if (!target.classList.contains("article"))
            return;

        var url = target.querySelector('a.read').getAttribute('href');

        document.location = url;

        return false;
    }
}