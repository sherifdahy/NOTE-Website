div_receipts = document.querySelector("#Receipts-links");
div_incoices = document.querySelector("#Invoices-links");


document.querySelector("#Invoices-link").addEventListener('click',toggle_div_links);
document.querySelector("#Receipts-link").addEventListener('click',toggle_div_links);


function toggle_div_links(event)
{
    if(event.target.id === "Invoices-link")
    {

        div_incoices.classList.toggle('display-none');
    }
    else{
        div_receipts.classList.toggle('display-none');
        
    }
}