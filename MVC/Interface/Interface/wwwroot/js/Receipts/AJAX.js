var subtypeList;

$(document).ready(function () {
    initializeTaxSelect();

})

window.initializeTaxSelect = function  () {
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


TaxTypeSelect.addEventListener('change', function (event) {
    SubTaxTypeSelect.innerHTML = '<option value="0">--- Select Sub tax ---</option>';
    for (let item in subtypeList) {


        if (subtypeList[item].TaxtypeReference == TaxTypeSelect.value) {
            let option = document.createElement('option');
            option.setAttribute('value', subtypeList[item].Code);
            option.innerHTML = subtypeList[item].Desc_ar;
            SubTaxTypeSelect.appendChild(option);
        }
    }

})