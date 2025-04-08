
import ItemComponent from './modules/ItemComponent.js';

window.Page = (function () {
    
    function init() {
        activateReceiptDropdown();
        addListeners();
        updateDateTime();
        handlePage();
        ItemComponent.init();
    };
    function activateReceiptDropdown() {
        const plusIcon = document.querySelector('.fa-plus');
        if (plusIcon) {
            plusIcon.parentNode.classList.add('active');
        }
        const receiptDropdown = document.querySelector('#receiptDropdown');
        if (receiptDropdown) {
            receiptDropdown.classList.add('show');
        }
    }

    function addListeners() {
        const observerItemsTable = new MutationObserver((mutations) => {
            mutations.forEach((mutation) => {
                if (mutation.type === 'childList') {
                    mutation.addedNodes.forEach((node) => {
                        if (node.nodeName === 'TR') {
                            handlePage();
                        }
                    });
                }
            });
        });

        observerItemsTable.observe(table_item_body, {
            childList: true,
            subtree: false 
        });
        
        
    };

    function handlePage () {
        let _totalSales = 0;
        let _netAmount = 0;
        let _totalAmount = 0;
        let _totalCommercialDiscount = 0;
        let _taxTotals = [];

        for (let tr of table_item_body.rows) {

            _totalAmount += +tr.querySelector('.Total').value;
            _totalSales += +tr.querySelector('.TotalSale').value;
            _netAmount += +tr.querySelector('.NetSale').value;
            let _tempCommerialDiscountData = JSON.parse(tr.querySelector('.CommercialDiscountData').value);
            let _tempTaxableItems = JSON.parse(tr.querySelector('.TaxableItems').value);

            _totalCommercialDiscount += _tempCommerialDiscountData.length > 0 ? _tempCommerialDiscountData[0].amount : 0;

            if (_taxTotals.length) {
                for (let _taxableItem of _tempTaxableItems) {
                    let index = _taxTotals.findIndex((_tax) => _tax.taxType == _taxableItem.taxType);
                    if (index >= 0) {
                        _taxTotals[index].amount += +_taxableItem.amount;
                    }
                    else {
                        let _temp = new TaxTotal();
                        _temp.amount = +_taxableItem.amount;
                        _temp.taxType = _taxableItem.taxType
                        _taxTotals.push(_temp);
                    }
                }
            }
            else {
                for (let _taxableItem of _tempTaxableItems) {
                    let _temp = new TaxTotal();
                    _temp.amount = +_taxableItem.amount;
                    _temp.taxType = _taxableItem.taxType
                    _taxTotals.push(_temp);
                }
            }

            
            

        }

        TotalCommercialDiscount.value = _totalCommercialDiscount;
        TotalSales.value = _totalSales;
        NetAmount.value = _netAmount;
        TotalAmount.value = _totalAmount;
        TaxTotals.value = JSON.stringify(_taxTotals);
    };
    function updateDateTime() {
        let now = new Date();
        var timezoneOffset = now.getTimezoneOffset() * 60000;
        document.getElementById('DateTime').value = (new Date(now - timezoneOffset)).toISOString().slice(0, 16);
    };
    
    return {
        init,
        ItemComponent,
    }
})();


document.addEventListener('DOMContentLoaded', Page.init);




