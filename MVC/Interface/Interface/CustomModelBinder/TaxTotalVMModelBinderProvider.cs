using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

public class TaxTotalVMModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // تحقق من أن النوع المراد ربطه هو ICollection<TaxTotalVM>
        if (context.Metadata.ModelType == typeof(ICollection<TaxTotalVM>))
        {
            return new TaxTotalVMModelBinder();
        }

        return null;
    }
}