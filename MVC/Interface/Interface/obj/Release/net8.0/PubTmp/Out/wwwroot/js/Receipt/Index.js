


const index = (function () {
    function init() {
        document.getElementsByClassName('fa-receipt')[0].parentNode.classList.add('active');
        InitializeDataTable();
    };


    function InitializeDataTable() {
        $('#receipts_dataTable').DataTable({
            select: {
                style: 'multi'
            },
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Arabic.json" // إذا كنت تستخدم لغة عربية
            },
            "serverSide": true, // تفعيل وضع الخادم

            "filter": true, // تفعيل البحث
            "ajax": {
                "url": "/receipts/index", // رابط API
                "type": "POST", // نوع الطلب
                "dataType": "json" // نوع البيانات
            },
            "columnDefs": [{
                "targets": [0], // العمود الأول (id)
                "visible": false, // مرئي
                "searchable": false // غير قابل للبحث
            }],
            "columns": [
                { "data": "id", "name": "id", "autoWidth": true },
                { "data": "receiptNumber", "name": "receiptNumber", "autoWidth": true },
                { "data": "dateTime", "name": "dateTime", "autoWidth": true },
                { "data": "type", "name": "type" },
                { "data": "issuesName", "name": "issuesName", "autoWidth": true },
                { "data": "issuesId", "name": "issuesId", "autoWidth": true },
                { "data": "total", "name": "total", "autoWidth": true },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return `
                        <div class="dropdown">
                          <button class="btn btn-primary " type="button" id="dropdownMenu2" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="fa-solid fa-bars"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenu2">
                            <li><button class="dropdown-item" type="button">Action</button></li>
                            <li><button class="dropdown-item" type="button">Another action</button></li>
                            <li><button class="dropdown-item" type="button">Something else here</button></li>
                          </ul>
                        </div>


                        `;
                    },
                    "orderable": false,
                    "autoWidth": true
                }

            ],

        });    
    }
    function deleteProduct(id) {
        if (confirm("هل أنت متأكد من حذف هذا المنتج؟")) {
            $.ajax({
                url: "/products/delete/" + id,
                type: "DELETE",
                success: function (response) {
                    alert("تم الحذف بنجاح");
                    $('#datatable').DataTable().ajax.reload(); // إعادة تحميل الجدول
                },
                error: function () {
                    alert("حدث خطأ أثناء الحذف");
                }
            });
        }
    }


    return {
        init,
    }
})();



document.addEventListener('DOMContentLoaded', index.init);