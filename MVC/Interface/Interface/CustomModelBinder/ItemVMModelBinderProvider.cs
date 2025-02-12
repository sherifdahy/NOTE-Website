using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

public class ItemVMModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // تحقق من أن النوع المراد ربطه هو ICollection<ItemVM>
        if (context.Metadata.ModelType == typeof(ICollection<ItemVM>))
        {
            return new ItemVMModelBinder();
        }

        return null;
    }
}