
import ItemComponent from './modules/ItemComponent.js';
import TaxComponent from './modules/TaxComponent.js' 
window.Page = (function () {

    let now = new Date();
    

    function init() {
        
        TaxComponent.init();
        ItemComponent.init();

        addListeners();
        setDateTime();
    };
    
    function addListeners() {
        // Tax 
        taxAreaToggleBtn.addEventListener('click', TaxComponent.toggleTaxArea);
        TaxTypeSelect.addEventListener('change', () => {
            TaxComponent.TaxTypeChange();
        });
        addTaxBtn.addEventListener('click', () => {
            TaxComponent.addTax();
            TaxComponent.LoadDataInTaxTable();
            ItemComponent.Handler(TaxComponent.arrayOfTax);
        });
        itemTaxRate.addEventListener('keyup', () => {
            TaxComponent.Handler(event);
        });
        itemTaxAmount.addEventListener('keyup', () => {
            TaxComponent.Handler(event);
        });

        // Item
        itemAreaToggleBtn.addEventListener('click', ItemComponent.toggleItemArea);
        ["keyup", "change"].forEach(eventType => {
            itemArea.addEventListener(eventType, () => {
                ItemComponent.Handler(TaxComponent.arrayOfTax);
            });
        });
        addItemBtn.addEventListener('click', () => {
            ItemComponent.addItem(TaxComponent.arrayOfTax);
            ItemComponent.LoadDataInItemsTable();
            
        });
        
        //Page
        sendBtn.addEventListener('click', () => {
            Send(event);

        });


    };

    function DeleteTax(index) {
        TaxComponent.Delete(index);
        TaxComponent.LoadDataInTaxTable();
        ItemComponent.Handler(TaxComponent.arrayOfTax);
    }
    function DeleteItem(index) {
        ItemComponent.Delete(index);
        ItemComponent.LoadDataInItemsTable();

    }

    function calc() {
        let TotalSales = 0;
        let NetAmount = 0;
        let TotalAmount = 0;
        let TotalCommercialDiscount = 0;
        let TaxTotals = [];
        if (itemData.value !== '') {
            JSON.parse(itemData.value).forEach(item => {
                TotalSales += item.TotalSale;
                NetAmount += item.NetSale;
                TotalAmount += item.Total;
                TotalCommercialDiscount += item.CommercialDiscountData.length > 0 ? item.CommercialDiscountData[0].Amount : 0;


            });
        }
        
        return [TotalSales, NetAmount, TotalAmount, TotalCommercialDiscount, JSON.stringify(TaxTotals)];
    }
    function Send(event) {
        
        let arr  = calc();

        TotalSales.value = arr[0];
        NetAmount.value = arr[1];
        TotalAmount.value = arr[2];
        TotalCommercialDiscount.value = arr[3];
        TaxTotals.value = arr[4];


        




        //$.ajax({
        //    url: '/Receipts/_receiptPartial',
        //    type: 'POST',
        //    contentType: "application/json",
        //    data: JSON.stringify(receipt),
        //    success: function (response) {
        //        location.reload();
        //        showMessage(response.message, 'success');
        //    },
        //    error: function (xhr) {
        //        let errorMessage = xhr.responseJSON?.message || "حدث خطأ أثناء الإرسال.";
        //        showMessage(errorMessage, 'danger');
        //    },
        //    complete: function () {
        //        event.target.removeAttribute('disabled');
        //        event.target.innerHTML = 'إرسال';
        //    }
        //});
    }
    function setDateTime() {
        var timezoneOffset = now.getTimezoneOffset() * 60000;
        document.getElementById('DateTime').value = (new Date(now - timezoneOffset)).toISOString().slice(0, 16);
    };
    function showMessage(message, type) {
        let alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type} position-fixed bottom-0 start-50 translate-middle-x`;
        alertDiv.setAttribute('role', 'alert');
        alertDiv.innerHTML = message;

        document.body.appendChild(alertDiv);

        setTimeout(() => {
            alertDiv.remove();
        }, 4000);
    };
    return {
        init,
        DeleteTax,
        DeleteItem,
    }
})();
document.addEventListener('DOMContentLoaded', Page.init);



