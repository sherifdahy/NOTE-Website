

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