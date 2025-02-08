receipt_links = document.querySelector("#Receipt-div");
invoice_links = document.querySelector("#Invoice-div");


document.querySelector("#Invoices-dept").addEventListener('click',toggle_div_links);
document.querySelector("#Receipts-dept").addEventListener('click',toggle_div_links);


function toggle_div_links(event)
{
    if(event.target.id === "Invoices-dept")
    {
        invoice_links.classList.toggle('d-none');
    }
    else{
        receipt_links.classList.toggle('d-none');
    }
}