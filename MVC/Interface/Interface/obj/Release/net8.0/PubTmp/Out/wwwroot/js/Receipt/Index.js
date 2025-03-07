


window.index = (function () {
    function init() {
    };

    function returnReceipt (event)
    {
        event.preventDefault();
        Swal.fire({
            title: "هل تريد ارجاع الايصال؟",
            icon: "question",
            iconHtml: "؟",
            confirmButtonText: "نعم",
            cancelButtonText: "لا",
            showCancelButton: true,
            showCloseButton: true
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = event.target.href;
            }
        });
    };
    


    return {
        init,
        returnReceipt,
    }
})();



document.addEventListener('DOMContentLoaded', index.init);