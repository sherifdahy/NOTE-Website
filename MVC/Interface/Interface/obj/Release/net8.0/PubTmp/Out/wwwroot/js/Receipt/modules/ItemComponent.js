
const ItemComponent = {

    
    init: function () {
        ItemComponent.InitializeSelect("/products/getDataToDropdownList");
        ItemComponent.LoadDataInItemsTable();

    },
    Handler: function (taxes) {
        
        let result = 0;
        for (let i in taxes) {

            result = result + +taxes[i].Amount;
        }

        itemTotalSale.value = (+Quantity.value * +UnitPrice.value).toFixed(2) || 0;
        itemNetSale.value = (+itemTotalSale.value - +Discount.value).toFixed(2) || 0;
        itemTotalTax.value = result.toFixed(2);
        itemTotal.value = (+itemNetSale.value + +itemTotalTax.value).toFixed(2) || 0;
    },

    addItem : function (taxes) {
        let item = new Item({
            InternalCode: ProductName.options[ProductName.selectedIndex].getAttribute('data-internalcode'),
            Description: Description.value,
            ItemType: ProductName.options[ProductName.selectedIndex].getAttribute('data-itemtype'),
            ItemCode: ProductName.options[ProductName.selectedIndex].getAttribute('data-itemcode'),
            UnitType: ProductName.options[ProductName.selectedIndex].getAttribute('data-unittype'),
            Quantity: +Quantity.value,
            UnitPrice: +UnitPrice.value,
            NetSale: +itemNetSale.value,
            TotalSale: +itemTotalSale.value,
            Total: +itemTotal.value,
            TaxableItems: taxes,
            CommercialDiscountData: +Discount.value > 0 ? [new CommercialDiscountData({
                Amount: +Discount.value,
                Description: "xyz",
                Rate: (+Discount.value / +itemTotalSale.value).toFixed(2)
            })] : [],
        });

        
        ItemComponent.updateItemData(item);
    },

    updateItemData: function (item) {
        if (!itemData.value) {
            itemData.value = JSON.stringify([item]);
            return;
        }
        let temp = JSON.parse(itemData.value);
        temp.push(item);
        itemData.value = JSON.stringify(temp);
    },
    


    LoadDataInItemsTable: function () {
        


        table_item_body.innerHTML = '';
        table_item_footer.innerHTML = '';
        let footerTotal = 0;
        let footerDiscountTotal = 0;
        let footerTaxTotal = 0;
        if (itemData.value !== '') {
            JSON.parse(itemData.value).forEach((item, index) => {
                let tr = document.createElement('tr');
                tr.innerHTML = `
            <td>${item.InternalCode}</td>
            <td>${item.Description}</td>
            <td>${item.UnitPrice}</td>
            <td>${item.Quantity}</td>
            <td>${item.CommercialDiscountData.length != 0 ? item.CommercialDiscountData[0].Amount : 0}</td>
            <td>${item.TaxableItems.length > 0 ? ItemComponent.calculate_tax_item(item.TaxableItems) : 0}</td>
            <td>
                
                <button class="btn btn-danger" onclick="Page.DeleteItem(${index})">
                    <i class="fa-solid fa-trash"></i>
                </button>

            </td>

            `;
                footerTotal += item.Total;
                footerDiscountTotal += item.CommercialDiscountData.length != 0 ? item.CommercialDiscountData[0].Amount : 0;
                footerTaxTotal += item.TaxableItems.length > 0 ? ItemComponent.calculate_tax_item(item.TaxableItems) : 0;
                table_item_body.appendChild(tr);
            });

            let tr = document.createElement('tr');
            tr.innerHTML = `
            <td colspan=2>اجمالي الخصم : ${footerDiscountTotal}</td>
            <td colspan=2>اجمالي الضريبة : ${footerTaxTotal}</td>
            <td colspan=3>الاجمالي بعد الخصم والضريبة : ${footerTotal}</td>

        `;
            table_item_footer.appendChild(tr);
        }
    },
    calculate_tax_item: function (taxes) {
        let result = 0.0;
        taxes.forEach(item => {
            result += item.Amount;
        });
        return result;

    },
    Delete: function (index) {

        itemData.value = JSON.stringify(
            JSON.parse(itemData.value).filter((item, i) => i !== index)
        );
    },
    toggleItemArea : function () {
        itemArea.classList.toggle('visually-hidden');
    },
    InitializeSelect: function (url) {

        $('#ProductName').select2({
            theme: 'bootstrap4',
            width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
            placeholder: $(this).data('placeholder'),
            allowClear: Boolean($(this).data('allow-clear')),
            closeOnSelect: !$(this).attr('multiple'),
            dropdownParent: $('#itemArea'),
            ajax: {
                url: url, // رابط API الذي سيتم إرسال الطلب إليه
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
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-internalcode', item.internalId);
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemtype', item.codeType);
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-unittype', item.unitType);
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemcode', item.code);
                return item.name; // كيفية عرض العنصر المحدد
            }
        });
        
    }
}

export default ItemComponent;