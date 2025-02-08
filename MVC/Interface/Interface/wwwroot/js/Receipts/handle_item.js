let arrayOfTax = [];
let arrayOfItem = [];
$('#ProductName').on('change.select2', function () {
    setTimeout(handle_item, 500);
});
document.addEventListener('DOMContentLoaded', function()  {
    document.getElementById("Quantity").addEventListener('keyup', handle_item);
    document.getElementById("UnitPrice").addEventListener('keyup', handle_item);
    document.getElementById("Discount").addEventListener('keyup', handle_item);
    document.getElementById("item_TaxPercent").addEventListener('keyup', handle_tax);
    document.getElementById("item_TaxAmount").addEventListener('keyup', handle_tax);
})


function LoadDataInTaxTable() {

    item_TaxTable.innerHTML = '';
    arrayOfTax.forEach((item, index) => {

        let tr = document.createElement('tr');
        tr.innerHTML = `
        <td>${item.TaxType.innerHTML}</td>
        <td>${item.SubType.innerHTML}</td>
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
            TaxType: taxtype,
            SubType: subtaxtype,
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
    arrayOfTax = arrayOfTax.filter((item,i)=> i !== index); 
    LoadDataInTaxTable();
    handle_item();

}


function clearTaxField() {
    TaxTypeSelect.selectedIndex = 0;
    SubTaxTypeSelect.selectedIndex  = 0;
    item_TaxPercent.value = 0.0 ;
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