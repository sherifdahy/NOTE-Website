
const ItemComponent = {

    
    init: function () {
        ItemComponent.InitializeSelect("/products/getDataToDropdownList");
        

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

    

    castingModelItemToHtmlRow: function (item) {
        let index = table_item_body.rows.length;
        let tr = document.createElement('tr');

        tr.innerHTML = `
                        <td><input class="form-control" name="itemData[${index}].InternalCode" value="${item.InternalCode}"/></td>
                        <td><input class="form-control " name="itemData[${index}].Description" value="${item.Description}"/></td>
                        <td hidden><input class="form-control" name="itemData[${index}].ItemType" value="${item.ItemType}"/></td>
                        <td hidden><input class="form-control" name="itemData[${index}].ItemCode" value="${item.ItemCode}"/></td>
                        <td hidden><input class="form-control" name="itemData[${index}].UnitType" value="${item.UnitType}"/></td>
                        <td><input class="form-control" name="itemData[${index}].Quantity" value="${item.Quantity}"/></td>
                        <td><input class="form-control" name="itemData[${index}].UnitPrice" value="${item.UnitPrice}"/></td>
                        <td><input class="form-control" name="itemData[${index}].CommercialDiscountData" value='${item.CommercialDiscountData}'/></td>
                        <td hidden><input class="form-control" name="itemData[${index}].NetSale" value="${item.NetSale}"/></td>
                        <td hidden><input class="form-control" name="itemData[${index}].TotalSale" value="${item.TotalSale}"/></td>
                        <td hidden><input class="form-control" name="itemData[${index}].Total" value="${item.Total}"/></td>
                        <td><input class="form-control" name="itemData[${index}].TaxableItems" value='${item.TaxableItems}'/></td>
                        <td>
                            <button type="button" onClick="Page.deleteRow(event.target)" class="btn btn-danger">delete</button>
                        </td>
                        `;
        return tr;
    },

    castingHtmlRowToModelItem: function (tr) {
        let inputs = tr.querySelectorAll('input');

        let item = new Item({
            InternalCode: inputs[0].value,
            Description: inputs[1].value,
            ItemType: inputs[2].value,
            ItemCode: inputs[3].value,
            UnitType: inputs[4].value,
            Quantity: +inputs[5].value,
            UnitPrice: +inputs[6].value,
            CommercialDiscountData: JSON.parse(inputs[7].value),
            NetSale: +inputs[8].value,
            TotalSale: +inputs[9].value,
            Total: +inputs[10].value,
            TaxableItems: JSON.parse(inputs[11].value),
        });
        return item;
    },

    addItem: function (taxes) {
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
            TaxableItems: JSON.stringify(taxes),
            CommercialDiscountData: +Discount.value > 0 ? JSON.stringify([new CommercialDiscountData({
                Amount: +Discount.value,
                Description: "xyz",
                Rate: ((+Discount.value / +itemTotalSale.value)*100).toFixed(2)
            })]) : JSON.stringify([]),
        });

        return item;
    },

    calculate_tax_item: function (taxes) {
        let result = 0.0;
        taxes.forEach(item => {
            result += item.Amount;
        });
        return result;

    },
    
    toggleItemArea : function () {
        itemArea.classList.toggle('visually-hidden');
    },

    InitializeSelect: function (url) {
        $('#ProductName').select2({
            theme: 'bootstrap4',
            width: '100%', // استخدام عرض ثابت
            placeholder: 'ابحث عن عنصر...', // نص افتراضي في حقل البحث
            allowClear: true,
            closeOnSelect: true,
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
                        results: data.items.map(item => ({
                            id: item.internalId, // تأكد من أن هذا الحقل فريد
                            text: item.name, // النص الذي سيتم عرضه
                            unitPrice: item.unitPrice,
                            description: item.description,
                            internalId: item.internalId,
                            codeType: item.codeType,
                            unitType: item.unitType,
                            code: item.code
                        })),
                        pagination: {
                            more: data.more // هل هناك المزيد من النتائج لتحميلها؟
                        }
                    };
                },
                cache: true // تخزين النتائج مؤقتًا لتحسين الأداء
            },
            minimumInputLength: 1, // الحد الأدنى لعدد الأحرف قبل إرسال الطلب
            templateResult: function (item) {
                if (item.loading) {
                    return "جاري التحميل...";
                }
                return item.text; // كيفية عرض العنصر في القائمة
            },
            templateSelection: function (item) {
                UnitPrice.value = item.unitPrice || 0;
                Description.value = item.description || '';
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-internalcode', item.internalId);
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemtype', item.codeType);
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-unittype', item.unitType);
                ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemcode', item.code);
                return item.text; // كيفية عرض العنصر المحدد
            }
        });
    }
}

export default ItemComponent;