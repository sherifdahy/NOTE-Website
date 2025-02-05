document.querySelector(".fa-box").parentNode.classList.add('active');

$(document).ready(function () {
    $('#datatable').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Arabic.json" // دعم اللغة العربية
        },
        "serverSide": true, // تفعيل وضع الخادم
        "filter": true, // تفعيل البحث
        "ajax": {
            "url": "/products/getData", // رابط API
            "type": "POST", // نوع الطلب
            "dataType": "json" // نوع البيانات
        },
        "columnDefs": [{
            "targets": [0], // العمود الأول (id)
            "visible": true, // مرئي
            "searchable": false // غير قابل للبحث
        }],
        "columns": [
            { "data": "id", "name": "id", "autoWidth": true },
            { "data": "internalId", "name": "internalId", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "description", "name": "description", "autoWidth": true },
            { "data": "unitPrice", "name": "unitPrice", "autoWidth": true },
            { "data": "unitType", "name": "unitType", "autoWidth": true },
            { "data": "code", "name": "code", "autoWidth": true },
            { "data": "codeType", "name": "codeType", "autoWidth": true },
            
            {
                "data": null, // عمود الحذف
                "render": function (data, type, row) {
                    return `<a href="/Products/Edit/${row.id}" class="btn btn-primary">Edit</a>
                            <a href="#" class="btn btn-danger" onclick="deleteProduct(${row.id})">Delete</a>`;
                },
                "orderable": false 
            }
        ]
    });
});

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
