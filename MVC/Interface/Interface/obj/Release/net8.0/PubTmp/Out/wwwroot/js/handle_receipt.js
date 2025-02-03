


ProductName.addEventListener('click', handle_receipt);
Quantity.onkeyup = handle_receipt;
UnitPrice.onkeyup = handle_receipt;
Discount.onkeyup = handle_receipt;

function handle_receipt(event) {

    TotalResult.value = +Quantity.value * +UnitPrice.value;
    TotalDiscount.value = Discount.value;
    TotalAmount.value = +TotalResult.value - +Discount.value;
}
