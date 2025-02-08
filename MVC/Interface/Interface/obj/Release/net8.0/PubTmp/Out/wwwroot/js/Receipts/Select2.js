$(document).ready(function () {
    $('#ProductName').select2({
        theme: 'bootstrap4',
        width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
        placeholder: $(this).data('placeholder'),
        allowClear: Boolean($(this).data('allow-clear')),
        closeOnSelect: !$(this).attr('multiple'),
        ajax: {
            url: '/products/getDataToDropdownList', // رابط API الذي سيتم إرسال الطلب إليه
            dataType: 'json', // نوع البيانات المتوقعة من الخادم
            delay: 500, // تأخير بين كتابة المستخدم وإرسال الطلب (بالمللي ثانية)
            data: function (params) {
                return {
                    query: params.term, // الكلمة التي يكتبها المستخدم في حقل البحث
                    page: params.page || 1 // رقم الصفحة (للتقسيم pagination)
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                // تحويل البيانات الواردة من الخادم إلى التنسيق الذي تفهمه Select2
                return {
                    results: data.items, // البيانات التي سيتم عرضها في القائمة
                    pagination: {
                        more: data.more // هل هناك المزيد من النتائج لتحميلها؟
                    }
                };
            },
            cache: true // تخزين النتائج مؤقتًا لتحسين الأداء
        },
        minimumInputLength: 1, // الحد الأدنى لعدد الأحرف قبل إرسال الطلب
        placeholder: 'ابحث عن عنصر...', // نص افتراضي في حقل البحث
        templateResult: function (item) {
            if (item.loading) {
                return "جاري التحميل...";
            }
            return item.name; // كيفية عرض العنصر في القائمة
        },
        templateSelection: function (item) {
            UnitPrice.value = item.unitPrice || 0;
            Description.value = item.description || '';
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-internalcode', item.internalId) ;
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemtype', item.codeType);
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-unittype', item.unitType);
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemcode', item.code);


            return item.name; // كيفية عرض العنصر المحدد
        }
    });
    $('selectpicker').select2({
        theme: 'bootstrap4',
    });
});