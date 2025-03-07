


const TaxComponent = {
    SubTaxTypeList: [],
    arrayOfTax: [],

    init: function () {
        TaxComponent.InitializeTaxSelect("/Receipts/gettaxtype");
    },
    
    toggleTaxArea: function () {
        taxArea.classList.toggle('visually-hidden');

    },

    InitializeTaxSelect: function InitializeTaxSelect(url) {
        $.ajax({
            url: url,
            type: "get",
            success: function (result) {

                let temp = JSON.parse(result.taxtype);
                for (let i = 0; i < temp.length; i++) {
                    let option = document.createElement('option');
                    option.setAttribute('value', temp[i].Code);
                    option.innerHTML = temp[i].Desc_ar;
                    TaxTypeSelect.appendChild(option);
                }
                TaxComponent.SubTaxTypeList = JSON.parse(result.subtaxtype);
            },
            error: function (xhr, status) {
                alert(status);
            }
        })
    },

    Handler: function (event) {
        if (event.target.id === "itemTaxRate") {
            itemTaxAmount.value = ((+itemNetSale.value) * (+itemTaxRate.value / 100)).toFixed(2);
        }
        else if (event.target.id === "itemTaxAmount") {
            itemTaxRate.value = ((+itemNetSale.value) * (+itemTaxAmount.value / 100)).toFixed(2);
        }
        
    },

    TaxTypeChange: function () {
        SubTaxTypeSelect.innerHTML = '<option value="0">--- اختيار وصف الضريبة ---</option>';
        for (let item in TaxComponent.SubTaxTypeList) {
            if (TaxComponent.SubTaxTypeList[item].TaxtypeReference == TaxTypeSelect.value) {
                let option = document.createElement('option');
                option.setAttribute('value', TaxComponent.SubTaxTypeList[item].Code);
                option.innerHTML = TaxComponent.SubTaxTypeList[item].Desc_ar;
                SubTaxTypeSelect.appendChild(option);
            }
        };
    },
    LoadDataInTaxTable: function () {

        itemTaxTable.innerHTML = '';
        TaxComponent.arrayOfTax.forEach((item, index) => {

            let tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${item.TaxType}</td>
                <td>${item.SubType}</td>
                <td>${item.Rate}</td>
                <td>${item.Amount}</td>
                <td class="text-start">
                    <button class="btn btn-danger" onclick="Page.DeleteTax(${index})">حذف</button>    
                </td>
            `;
            itemTaxTable.appendChild(tr);
        })
    },
    addTax: function () {
        if (this.arrayOfTax.length > 0) {
            for (let i = 0; i < this.arrayOfTax.length; i++) {
                if (this.arrayOfTax[i].TaxType == TaxTypeSelect.value) {
                    Page.notification("error", "لقد قمت بأضافة هذا النوع من الضريبة من قبل . ");
                    return;
                }
            }
        }
        TaxComponent.arrayOfTax.push(new TaxableItem({
            TaxType: TaxTypeSelect.value,
            SubType: SubTaxTypeSelect.value,
            Amount: +itemTaxAmount.value,
            Rate: +itemTaxRate.value,
        }));

        
    },

    Delete: function (index) {
        TaxComponent.arrayOfTax = TaxComponent.arrayOfTax.filter((item, i) => i !== index);
    },

}

export default TaxComponent;