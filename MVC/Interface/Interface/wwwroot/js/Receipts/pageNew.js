document.querySelector('.new-receipt').parentNode.classList.add('active');
document.querySelector('.new-receipt').parentNode.parentNode.classList.toggle('d-none');



document.addEventListener('DOMContentLoaded', function () {
    var now = new Date();
    var timezoneOffset = now.getTimezoneOffset() * 60000;
    var localISOTime = (new Date(now - timezoneOffset)).toISOString().slice(0, 16);
    document.getElementById('DateTime').value = localISOTime;
});


function Send(event) {
    event.preventDefault();
    let receipt = new Receipt();
    receipt.ReceiptNumber = ReceiptNumber.value;
    receipt.DateTime = DateTime.value;
    receipt.IssuedName = IssuedName.value;
    receipt.IssuedNumber = IssuedNumber.value;
    receipt.itemData = 


    console.log(JSON.stringify(receipt));
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
            console.log(result);
        },
        error: function (xhr, status) {
            console.log(status);
        }
    })
        
        
}