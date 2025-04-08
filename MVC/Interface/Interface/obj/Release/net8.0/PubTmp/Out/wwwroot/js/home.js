


let page = (function () {
    function init() {
        document.querySelector('.fa-house').parentElement.classList.add('active');
    }

    return {
        init,
    }
})();

document.addEventListener('DOMContentLoaded', page.init());