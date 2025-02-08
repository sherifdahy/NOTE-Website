class Receipt {
    constructor(data = {}) {
        this.Header = data.Header || null;
        this.DocumentType = data.DocumentType || null;
        this.Seller = data.Seller || null;
        this.itemData = data.itemData || [];
        this.TotalSales = data.TotalSales || 0;
        this.TotalCommercialDiscount = data.TotalCommercialDiscount || 0;
        this.NetAmount = data.NetAmount || 0;
        this.TotalAmount = data.TotalAmount || 0;
        this.TaxTotals = data.TaxTotals || [];
        this.PaymentMethod = data.PaymentMethod || null;
    }
}


class Header {
    constructor(data = {}) {
        this.DateTimeIssued = data.DateTimeIssued || null;
        this.ReceiptNumber = data.ReceiptNumber || null;
        this.Uuid = data.Uuid || null;
        this.PreviousUUID = data.PreviousUUID || null;
        this.ReferenceOldUUID = data.ReferenceOldUUID || null;
        this.Currency = data.Currency || null;
        this.ExchangeRate = data.ExchangeRate || 0;
        this.SOrderNameCode = data.SOrderNameCode || null;
        this.OrderdeliveryMode = data.OrderdeliveryMode || null;
    }
}


class DocumentType {
    constructor(data = {}) {
        this.ReceiptType = data.ReceiptType || null;
        this.TypeVersion = data.TypeVersion || null;
    }
}

class Seller {
    constructor(data = {}) {
        this.Rin = data.Rin || null;
        this.CompanyTradeName = data.CompanyTradeName || null;
        this.BranchCode = data.BranchCode || null;
        this.BranchAddress = data.BranchAddress || null;
        this.DeviceSerialNumber = data.DeviceSerialNumber || null;
        this.ActivityCode = data.ActivityCode || null;
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