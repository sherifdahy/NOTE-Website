let page = (function () {
    function init() {
        
        document.querySelector('.fa-folder-open').parentNode.classList.add('active');
        document.querySelector('#receiptDropdown').classList.add('show');
        document.querySelector('#pageSizeSelect').addEventListener('change', changePageSize);
    };

    function changePageSize(event) {
        event.target.closest('form').submit();
    }
    return {
        init,
        changePageSize,
    }
})();



document.addEventListener('DOMContentLoaded', page.init);