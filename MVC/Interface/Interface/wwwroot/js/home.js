
let page = (function () {
    function init() {
        addListener();
        updateDateTime();
    };

    function addListener() {
        document.querySelector('.fa-house').parentElement.classList.add('active');
        ['receiptsStartDate', 'receiptsEndDate', 'invoicesStartDate', 'invoicesEndDate'].forEach(text => {
            document.getElementById(text).addEventListener('change', getReceiptDashboard);
        });
        ['invoicesStartDate', 'invoicesEndDate'].forEach(text  => {
            document.getElementById(text).addEventListener('change', getInvoiceDashboard);
        })
    }

    function updateDateTime() {
        let now = new Date();
        var timezoneOffset = now.getTimezoneOffset() * 60000;

        let thirtyDaysAgo = new Date(now - timezoneOffset);
        thirtyDaysAgo.setDate(thirtyDaysAgo.getDate() - 30);

        document.getElementById('receiptsStartDate').value = thirtyDaysAgo.toISOString().slice(0, 16);
        document.getElementById('receiptsEndDate').value = (new Date(now - timezoneOffset)).toISOString().slice(0, 16);

        document.getElementById('invoicesStartDate').value = thirtyDaysAgo.toISOString().slice(0, 16);
        document.getElementById('invoicesEndDate').value = (new Date(now - timezoneOffset)).toISOString().slice(0, 16);
    }
    function getReceiptDashboard() {
        document.querySelector('.box-body').innerHTML =
            `<div class="spinner-border d-flex justifiy-content-center" role="status">
                    <span class="visually-hidden">Loading...</span>
            </div>`;
        $.ajax({
            url: "/home/ReceiptsDashboard",
            method: "get",
            data: {
                "startDate": document.querySelector('#receiptsStartDate').value, 
                "endDate": document.querySelector('#receiptsEndDate').value,
            },
            success: function (result) {
                document.querySelector('.box-body').innerHTML = result;
            },
            error: function (xhr, status) {
                console.log(status);
            }

        })
    };
    function getInvoiceDashboard() {
       
    }

    return {
        init,
        getReceiptDashboard,
        getInvoiceDashboard,
    }
})();

document.addEventListener('DOMContentLoaded', () => {
    page.init();
    page.getReceiptDashboard();
    page.getInvoiceDashboard();
});