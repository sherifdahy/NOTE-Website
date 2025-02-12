


function LoadDataInTaxTable() {

    item_TaxTable.innerHTML = '';
    arrayOfTax.forEach((item, index) => {

        let tr = document.createElement('tr');
        tr.innerHTML = `
        <td>${item.TaxType}</td>
        <td>${item.SubType}</td>
        <td>${item.Rate}</td>
        <td>${item.Amount}</td>
        <td>
            <button class="btn btn-danger" onclick="delete_tax(${index})">Delete</button>    
        </td>
        `;
        item_TaxTable.appendChild(tr);
    })
}

function addTax() {
    let taxtype = TaxTypeSelect.options[TaxTypeSelect.selectedIndex];
    let subtaxtype = SubTaxTypeSelect.options[SubTaxTypeSelect.selectedIndex];
    let percent = (+item_TaxPercent.value).toFixed(2);
    let amount = (+item_TaxAmount.value).toFixed(2);
    if (taxtype.value != 0 && subtaxtype.value != 0 && (percent > 0 && percent <= 100) && amount > -1) {
        arrayOfTax.push(new TaxableItem({
            TaxType: taxtype.value,
            SubType: subtaxtype.value,
            Amount: amount,
            Rate: percent,
        }));
        LoadDataInTaxTable();
        handle_item();
        clearTaxField();
    }
    else {
        alert("حدث اخطأ اثناء اضافة الضريبة,من فضلك تأكد من ملئ الحقول بشكل صحيح.");
    }

}
function delete_tax(index) {
    arrayOfTax = arrayOfTax.filter((item, i) => i !== index);
    LoadDataInTaxTable();
    handle_item();

}


function clearTaxField() {
    TaxTypeSelect.selectedIndex = 0;
    SubTaxTypeSelect.selectedIndex = 0;
    item_TaxPercent.value = 0.0;
    item_TaxAmount.value = 0.0;

}
function clearItemField() {
    clearTaxField();
    LoadDataInTaxTable();

    $('#ProductName').empty();
    Quantity.value = 0.0;
    Discount.value = 0.0;
    handle_item();

}
function handle_tax(event) {
    if (event.target.id === "item_TaxPercent") {
        item_TaxAmount.value = ((+item_TotalBefore.value - +item_TotalDiscount.value) * (+item_TaxPercent.value / 100)).toFixed(2);
    }
    else if (event.target.id === "item_TaxAmount") {
        item_TaxPercent.value = ((+item_TotalBefore.value - +item_TotalDiscount.value) * (+item_TaxAmount.value / 100)).toFixed(2);
    }
}




function handle_item() {

    let total = 0;
    for (let i in arrayOfTax) {

        total = total + +arrayOfTax[i].Amount;
    }

    item_TotalBefore.value = (+Quantity.value * +UnitPrice.value).toFixed(2) || 0;
    item_TotalDiscount.value = (+Discount.value).toFixed(2) || 0;
    item_TotalTax.value = total.toFixed(2);
    item_TotalAfter.value = (+item_TotalBefore.value - +item_TotalDiscount.value + +item_TotalTax.value).toFixed(2) || 0;


}



function addItem() {
    console.log(arrayOfTax);
    let item = new Item({
        InternalCode: ProductName.options[ProductName.selectedIndex].getAttribute('data-internalcode'),
        Description: Description.value,
        ItemType: ProductName.options[ProductName.selectedIndex].getAttribute('data-itemtype'),
        ItemCode: ProductName.options[ProductName.selectedIndex].getAttribute('data-itemcode'),
        UnitType: ProductName.options[ProductName.selectedIndex].getAttribute('data-unittype'),
        Quantity: +Quantity.value,
        UnitPrice: +UnitPrice.value,
        NetSale: (+item_TotalBefore.value - +item_TotalDiscount.value),
        TotalSale: +item_TotalBefore.value,
        Total: +item_TotalAfter.value,
        TaxableItems: arrayOfTax,
        CommercialDiscountData: +Discount.value > 0 ? [new CommercialDiscountData({
            Amount: +item_TotalDiscount.value,
            Description: "xyz",
            Rate: (+item_TotalDiscount.value / +item_TotalBefore.value).toFixed(2)
        })] : [],
    });
    arrayOfItem.push(item);
    LoadDataInItemsTable();
    arrayOfTax = [];
    clearItemField();


}



function Send(event) {
    event.preventDefault();
    let receipt = new Receipt();
    receipt.ReceiptNumber = ReceiptNumber.value;
    receipt.DateTime = DateTime.value;
    receipt.IssuedName = IssuedName.value;
    receipt.IssuedNumber = IssuedNumber.value;
    receipt.itemData = arrayOfItem.length > 0 ? arrayOfItem : null;
    receipt.TotalSales = +receipt_TotalBefore.value;
    receipt.NetAmount = +receipt_TotalBefore.value - +receipt_TotalDiscount.value;
    receipt.TotalAmount = +receipt_TotalAfter.value;
    receipt.PaymentMethod = PaymentMethod.value;
    $.ajax({
        url: '/Receipts/_receiptPartial',
        type: 'post',
        contentType: "application/json",
        data: JSON.stringify(receipt),
        dataType: "html",
        success: function (result) {
            document.getElementById('_receiptPartial').innerHTML = result;
            initializeSelect2();
            initializeTaxSelect();
        },
        error: function (xhr, status) {
            alert(status);
        }
    })


}



window.initializeTaxSelect = function () {
    $.ajax({
        url: "/Receipts/gettaxtype",
        type: "get",
        success: function (result) {

            let temp = JSON.parse(result.taxtype);
            for (let i = 0; i < temp.length; i++) {
                let option = document.createElement('option');
                option.setAttribute('value', temp[i].Code);
                option.innerHTML = temp[i].Desc_ar;
                TaxTypeSelect.appendChild(option);
            }
            subtypeList = JSON.parse(result.subtaxtype);
        },
        error: function (xhr, status) {
            alert(status);
        }
    })
}




function LoadDataInItemsTable() {
    itemsTable.innerHTML = '';

    let totalDiscount = 0.0;
    let TotalAmount = 0.0;
    let Total = 0.0;
    let totalTax = 0.0;
    arrayOfItem.forEach((item, index) => {
        let tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${item.InternalCode}</td>
            <td>${item.Description}</td>
            <td>${item.UnitPrice}</td>
            <td>${item.Quantity}</td>
            <td>${item.CommercialDiscountData.length != 0 ? item.CommercialDiscountData[0].Amount : 0}</td>
            <td>${item.TaxableItems.length > 0 ? +calculate_tax_item(item.TaxableItems) : 0}</td>
            <td>
                <button class="btn btn-primary">تعديل</button>
                <button class="btn btn-danger" onclick="deleteItem(${index})">حذف</button>

            </td>

        `;

        itemsTable.appendChild(tr);

        totalDiscount += item.CommercialDiscountData.length != 0 ? item.CommercialDiscountData[0].Amount : 0;
        TotalAmount += item.TotalSale;
        totalTax += item.TaxableItems.length > 0 ? +calculate_tax_item(item.TaxableItems) : 0;
        Total += item.Total;

    });
    receipt_TotalBefore.value = TotalAmount;
    receipt_TotalDiscount.value = totalDiscount;
    receipt_TotalTax.value = totalTax;
    receipt_TotalAfter.value = Total
}
function deleteItem(index) {
    arrayOfItem = arrayOfItem.filter((item, i) => i !== index);
    LoadDataInItemsTable();
}
function calculate_tax_item(arrayOfTax) {
    let result = 0.0;
    arrayOfTax.forEach(item => {
        result += item.Amount;
    });
    return result;

}



window.initializeSelect2 = function () {
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
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-internalcode', item.internalId);
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemtype', item.codeType);
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-unittype', item.unitType);
            ProductName.options[ProductName.selectedIndex]?.setAttribute('data-itemcode', item.code);


            return item.name; // كيفية عرض العنصر المحدد
        }
    });
}

function TaxTypeChange() {
    SubTaxTypeSelect.innerHTML = '<option value="0">--- Select Sub tax ---</option>';
    for (let item in subtypeList) {


        if (subtypeList[item].TaxtypeReference == TaxTypeSelect.value) {
            let option = document.createElement('option');
            option.setAttribute('value', subtypeList[item].Code);
            option.innerHTML = subtypeList[item].Desc_ar;
            SubTaxTypeSelect.appendChild(option);
        }
    }
}