class Receipt {
    constructor(data = {}) {ReceiptNumber
        this.ReceiptNumber = data.ReceiptNumber || null;
        this.DateTime = data.DateTime || null;
        this.IssuedName = IssuedName || null;
        this.IssuedNumber = IssuedNumber || null;
        this.itemData = data.itemData || [];
        this.TotalSales = data.TotalSales || 0;
        this.TotalCommercialDiscount = data.TotalCommercialDiscount || 0;
        this.NetAmount = data.NetAmount || 0;
        this.TotalAmount = data.TotalAmount || 0;
        this.TaxTotals = data.TaxTotals || [];
        this.PaymentMethod = data.PaymentMethod || null;
    }
}




class Item {
    constructor(data = {}) {
        this.InternalCode = data.InternalCode || null;
        this.Description = data.Description || null;
        this.ItemType = data.ItemType || null;
        this.ItemCode = data.ItemCode || null;
        this.UnitType = data.UnitType || null;
        this.Quantity = data.Quantity || 0;
        this.UnitPrice = data.UnitPrice || 0;
        this.NetSale = data.NetSale || 0;
        this.TotalSale = data.TotalSale || 0;
        this.Total = data.Total || 0;
        this.TaxableItems = data.TaxableItems || [];
        this.CommercialDiscountData = data.CommercialDiscountData || [];
    }
}


class TaxableItem {
    constructor(data = {}) {
        this.TaxType = data.TaxType || null;
        this.SubType = data.SubType || null;
        this.Rate = data.Rate || 0;
        this.Amount = data.Amount || 0;
    }
}


class CommercialDiscountData {
    constructor(data = {}) {
        this.Description = data.Description || null;
        this.Amount = data.Amount || 0;
        this.Rate = data.Rate || 0;
    }
}


class TaxTotal {
    constructor(data = {}) {
        this.TaxType = data.TaxType || null;
        this.Amount = data.Amount || 0;
    }
}