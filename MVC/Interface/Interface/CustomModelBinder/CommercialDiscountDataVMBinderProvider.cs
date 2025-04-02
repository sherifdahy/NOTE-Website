using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

public class CommercialDiscountDataVMBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(IList<CommercialDiscountDataVM>))
        {
            return new CommercialDiscountDataVMBinder();
        }

        return null;
    }
}