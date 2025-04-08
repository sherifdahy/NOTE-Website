

const ItemComponent = {
    SubTaxTypeList: [],
    TaxTypeList: [],

    init: function  ()  {
        ItemComponent.initializeTaxSelect("/Accessor/GetTaxType");
        ItemComponent.initializeSelect("/products/getDataToDropdownList");
        ItemComponent.addListener();
    },

    addListener: function () {

        itemArea.addEventListener('change', () => {
            ItemComponent.handleItemComponent();
        });

        addItemBtn.addEventListener('click', () => {
            ItemComponent.addItemRow();
            SweetAlert.notification('info', '', 'تم اضافة بند للأيصال');
        });

        addTaxBtn.addEventListener('click', () => {
            ItemComponent.addTaxRow();
        });
    },

    addTaxRow: function () {
        let tr = document.createElement('tr');
        tr.innerHTML =
            `
                <td class="form-group">
                    <select class="form-select tax">
                        <option value="0">-- اختر نوع الضريبة --</option>
                    </select>
                </td>
                <td class=" form-group ">
                    <select class="form-select subTax" disabled>
                        <option value="0">-- اختر تفاصيل الضريبة --</option>
                    </select>
                </td>
                <td class="form-group ">
                    <input class="form-control rate" type="number" disabled />
                </td>
                <td >
                    <input class="form-control amount" type="number" disabled />
                </td>
                <td>
                    <button class="btn btn-danger" onclick="Page.ItemComponent.deleteRow(event)">
                        حذف
                    </button>
                </td>
            `;
        for (let item of ItemComponent.TaxTypeList) {
            let option = document.createElement('option');
            option.setAttribute('value', item.Code);
            option.innerHTML = item.Desc_ar;
            tr.querySelector('.tax').appendChild(option);
        }
        tr.querySelector('.tax').addEventListener('change', ItemComponent.taxTypeChange);
        tr.querySelector('.subTax').addEventListener('change', ItemComponent.subTaxTypeSelectChange);

        itemTaxTable.appendChild(tr);
    },

    initializeTaxSelect: function (url) {
        $.ajax({
            url: url,
            type: "get",
            success:  (result) => {
                ItemComponent.TaxTypeList = JSON.parse(result.taxtype);
                ItemComponent.SubTaxTypeList = JSON.parse(result.subtaxtype);
            },
            error: (xhr, status) => {
                alert(status);
            }
        })
    },

    taxTypeChange: (event) => {
        
        let _taxTypeSelect = event.target.closest('tr').querySelector('.tax');
        let _subTaxTypeSelect = event.target.closest('tr').querySelector('.subTax');
        let _rate = event.target.closest('tr').querySelector('.rate');
        let _amount = event.target.closest('tr').querySelector('.amount');


        if (_taxTypeSelect.value !== "0") {
            _subTaxTypeSelect.removeAttribute('disabled');
            _subTaxTypeSelect.innerHTML = '<option value="0">--- اختيار وصف الضريبة ---</option>';
            let temp = ItemComponent.SubTaxTypeList.filter((item, index) => item.TaxtypeReference == _taxTypeSelect.value);
            temp.forEach((item, index) => {
                let option = document.createElement('option');
                option.setAttribute('value', item.Code);
                option.innerHTML = item.Desc_ar;
                _subTaxTypeSelect.appendChild(option);
            });

        }
        else {
            _subTaxTypeSelect.setAttribute('disabled', true);

        }
        _rate.setAttribute('disabled', true);
        _amount.setAttribute('disabled', true);
        _rate.value = 0;
        _amount.value = 0;
    },

    HTML2ArrayOfObject: function () {
        let _taxableItems = [];
        for (let tr of itemTaxTable.rows) {
            let _taxableItem = new TaxableItem();
            _taxableItem.taxType = tr.querySelector('.tax').value;
            _taxableItem.subType = tr.querySelector('.subTax').value;
            _taxableItem.rate = tr.querySelector('.rate').value;
            _taxableItem.amount = tr.querySelector('.amount').value;
            _taxableItems.unshift(_taxableItem);
        }
        return _taxableItems;
    },

    subTaxTypeSelectChange: function (event)  {


        let _taxTypeSelect = event.target.closest('tr').querySelector('.tax');
        let _subTaxTypeSelect = event.target.closest('tr').querySelector('.subTax');
        let _rate = event.target.closest('tr').querySelector('.rate');
        let _amount = event.target.closest('tr').querySelector('.amount');


        // initialize input as disable
        _rate.setAttribute('disabled', true);
        _amount.setAttribute('disabled', true);
        _rate.value = 0.0;
        _amount.value = 0.0;

        

        if (_subTaxTypeSelect.value !== "0") {
            switch (_subTaxTypeSelect.value) {
                case ("V001"):
                case ("V002"):
                case ("V003"):
                case ("V004"):
                case ("V005"):
                case ("V006"):
                case ("V007"):
                case ("V008"):
                case ("V009"): {
                    _rate.removeAttribute('disabled');
                    _amount.removeAttribute('disabled');
                    _rate.onchange = () => {
                        _amount.value = ((+itemNetSale.value * +_rate.value) / 100).toFixed(2);
                    };
                    _amount.onchange = () => {
                        _rate.value = (1 - ((+itemNetSale.value - +_amount.value) / +itemNetSale.value)).toFixed(2);
                    };
                    break;
                }
                case ("Tbl001"): // T2 ضريبة الجدول نسبة
                case ("Tbl002"): //
                case ("Tbl003"): //
                case ("Tbl004"): //
                case ("Tbl005"): //
                case ("Tbl006"): //
                case ("Tbl01"):  //
                case ("W001"):   // T4 الخصم تحت حساب الضريبه
                case ("W002"):   //
                case ("W003"):   //
                case ("W004"):   //
                case ("W005"):   //
                case ("W006"):   //
                case ("W007"):   //
                case ("W008"):   //
                case ("W009"):   //
                case ("W010"):   //
                case ("W011"):   //
                case ("W012"):   //
                case ("W013"):   //
                case ("W014"):   //
                case ("W015"):   //
                case ("W016"):   //
                case ("Ent01"):  // T7 ضريبة الملاهي نسبة
                case ("ST01"):   // T5 ضريبه الدمغه نسبيه
                {
                    _rate.removeAttribute('disabled');
                    _rate.onchange = () => {
                        _amount.value = ((+itemNetSale.value * +_rate.value) / 100).toFixed(2);
                    };
                    break;
                }
                case ("Ent02"):  // T7 ضريبة الملاهي قطعية
                case ("ST02"):   // T6 ضريبه الدمغه قطعية
                case ("Tbl02"):  // T3 ضريبة الجدول قطعية
                    _amount.removeAttribute('disabled');
                    break;
                }
            }
        },

    deleteRow: function (event)  {
        event.target.closest("tr").remove();
    },

    handleItemComponent: function ()  {
        let result = 0;
        let TaxableItems = ItemComponent.HTML2ArrayOfObject();
        
        for (let taxableItem of TaxableItems) {
            switch (taxableItem.taxType) {
                case ("T4"): {
                    result -= +taxableItem.amount;
                    break;
                }
                default : {
                    result += +taxableItem.amount;
                    break;
                }
            }
        }

        itemTotalSale.value = (+Quantity.value * +UnitPrice.value).toFixed(2) || 0;
        itemNetSale.value = (+itemTotalSale.value - +Discount.value).toFixed(2) || 0;
        itemTotalTax.value = result.toFixed(2);
        itemTotal.value = (+itemNetSale.value + +itemTotalTax.value).toFixed(2) || 0;
    }, 
    
    addItemRow: function () {
        let _taxableItems = ItemComponent.HTML2ArrayOfObject();
        let index = table_item_body.rows.length;
        let tr = document.createElement('tr');
        let tempComm = new CommercialDiscountData();
        tempComm.amount = +Discount.value;
        tempComm.description = "xyz";
        tempComm.rate =((+Discount.value / +itemTotalSale.value) * 100).toFixed(2);
        tr.innerHTML = `
                        <td><input class="form-control InternalCode" name="itemData[${index}].InternalCode" value="${ProductName.options[ProductName.selectedIndex].getAttribute('data-internalcode') }"/></td>
                        <td><input class="form-control Description" name="itemData[${index}].Description" value="${Description.value}"/></td>
                        <td hidden><input class="form-control ItemType" name="itemData[${index}].ItemType" value="${ProductName.options[ProductName.selectedIndex].getAttribute('data-itemtype') }"/></td>
                        <td hidden><input class="form-control ItemCode" name="itemData[${index}].ItemCode" value="${ProductName.options[ProductName.selectedIndex].getAttribute('data-itemcode') }"/></td>
                        <td hidden><input class="form-control UnitType" name="itemData[${index}].UnitType" value="${ProductName.options[ProductName.selectedIndex].getAttribute('data-unittype') }"/></td>
                        <td><input class="form-control Quantity" name="itemData[${index}].Quantity" value="${+Quantity.value}"/></td>
                        <td><input class="form-control UnitPrice" name="itemData[${index}].UnitPrice" value="${+UnitPrice.value}"/></td>
                        <td><input class="form-control CommercialDiscountData" name="itemData[${index}].CommercialDiscountData" value='${+Discount.value > 0 ? JSON.stringify([tempComm]) : JSON.stringify([]) }'/></td>
                        <td hidden><input class="form-control NetSale" name="itemData[${index}].NetSale" value="${+itemNetSale.value}"/></td>
                        <td hidden><input class="form-control TotalSale" name="itemData[${index}].TotalSale" value="${+itemTotalSale.value}"/></td>
                        <td hidden><input class="form-control Total" name="itemData[${index}].Total" value="${+itemTotal.value}"/></td>
                        <td><input class="form-control TaxableItems" name="itemData[${index}].TaxableItems" value='${JSON.stringify(_taxableItems)}'/></td>
                        <td>
                            <button type="button" onClick="Page.ItemComponent.deleteRow(event)" class="btn btn-danger">حذف</button>
                        </td>
                        `;
        table_item_body.appendChild(tr);
    },

    initializeSelect: function (url)  {
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