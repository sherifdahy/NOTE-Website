


window.index = (function () {
    function init() {
        document.querySelector('.fa-folder-open').parentNode.classList.add('active');
        document.querySelector('#receiptDropdown').classList.add('show');
    };

    
    


    return {
        init,
    }
})();



document.addEventListener('DOMContentLoaded', index.init);