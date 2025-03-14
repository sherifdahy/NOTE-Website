﻿
import ItemComponent from './modules/ItemComponent.js';
import TaxComponent from './modules/TaxComponent.js' 


window.Page = (function () {

    let now = new Date();
    
    function init() {
        document.getElementsByClassName('fa-plus')[0].parentNode.classList.add('active');
        TaxComponent.init();
        ItemComponent.init();
        addListeners();
        setDateTime();
        handleFooter();
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
        itemTaxRate.addEventListener('keyup', (event) => {
            TaxComponent.Handler(event);
        });
        itemTaxAmount.addEventListener('keyup', (event) => {
            TaxComponent.Handler(event);
        });

        
        
        ["keyup", "change"].forEach(eventType => {
            itemArea.addEventListener(eventType, () => {
                ItemComponent.Handler(TaxComponent.arrayOfTax);
            });
        });
        addItemBtn.addEventListener('click', () => {
            let item = ItemComponent.addItem(TaxComponent.arrayOfTax);
            let tr = ItemComponent.castingModelItemToHtmlRow(item);
            table_item_body.appendChild(tr);
            handleFooter();
            notification("info","تم اضافة بند للأيصال");
            

        });

        sendBtn.addEventListener('click', Send);
    };

    function notification(icon,title) {
        const Toast = Swal.mixin({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.onmouseenter = Swal.stopTimer;
                toast.onmouseleave = Swal.resumeTimer;
            }
        });
        Toast.fire({
            icon: icon,
            title: title
        });
    }

    function Send(event) {
        event.preventDefault();
        Swal.fire({
            title: "هل تريد الارسال؟",
            icon: "question",
            iconHtml: "؟",
            confirmButtonText: "نعم",
            cancelButtonText: "لا",
            showCancelButton: true,
            showCloseButton: true
        }).then((result) => {
            if (result.isConfirmed) {
                event.target.closest('form').submit();
            }
        });
    };
    function deleteRow (button) {
        button.closest("tr").remove();
        handleFooter();
    };
    function handleFooter()
    {

        let totalSales = 0;
        let netAmount = 0;
        let totalAmount = 0;
        let totalCommercialDiscount = 0;
        let taxTotals = [];
       
        for (let i = 0; i < table_item_body.rows.length; i++)
        {
            let item = ItemComponent.castingHtmlRowToModelItem(table_item_body.rows[i]);
            totalSales += item.TotalSale;
            netAmount += item.NetSale;
            totalAmount += item.Total;
            totalCommercialDiscount += item.CommercialDiscountData.length > 0 ? item.CommercialDiscountData[0].Amount : 0;

            for (let j = 0; j < item.TaxableItems.length; j++)
            {
                if (taxTotals.length > 0) {
                    for (let z = 0; z < taxTotals.length; z++) {
                        if (taxTotals[z].TaxType == item.TaxableItems[j].TaxType) {
                            taxTotals[z].Amount += item.TaxableItems[j].Amount;
                        }
                        else
                        {
                            let temp = new TaxTotal({
                                Amount : item.TaxableItems[j].Amount,
                                TaxType : item.TaxableItems[j].TaxType,
                            });
                            taxTotals.push(temp);
                        }
                    }
                }
                else {
                    let temp = new TaxTotal({
                        Amount : item.TaxableItems[j].Amount,
                        TaxType : item.TaxableItems[j].TaxType,
                    });
                    taxTotals.push(temp);
                }
            }

        }

        table_item_footer.innerHTML = ` <tr>
                                            <td colspan="4"></td>
                                            <td>اجمالي الضريبة</td>
                                            <td colspan="2"><input name="TotalCommercialDiscount" class="form-control" value="${totalCommercialDiscount}" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                            <td>اجمالي المبيعات</td>
                                            <td colspan="2"><input name="TotalSales" class="form-control" value="${totalSales}" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                            <td>الاجمالي بعد الضريبة</td>
                                            <td colspan="2"><input name="NetAmount" class="form-control" value="${netAmount}" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                            <td>الاجمالي</td>
                                            <td colspan="2"><input name="TotalAmount" class="form-control" value="${totalAmount}" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                            <td>الضريبة</td>
                                            <td colspan="2"><input name="TaxTotals" class="form-control" value='${JSON.stringify(taxTotals)}' /></td>
                                        </tr>
                                        `;
    };
    function DeleteTax(index) {
        TaxComponent.Delete(index);
        TaxComponent.LoadDataInTaxTable();
        ItemComponent.Handler(TaxComponent.arrayOfTax);
    };

    
    function setDateTime() {
        var timezoneOffset = now.getTimezoneOffset() * 60000;
        document.getElementById('DateTime').value = (new Date(now - timezoneOffset)).toISOString().slice(0, 16);
    };
    
    return {
        init,
        DeleteTax,
        ItemComponent,
        deleteRow,
        notification,
    }
})();


document.addEventListener('DOMContentLoaded', Page.init);




