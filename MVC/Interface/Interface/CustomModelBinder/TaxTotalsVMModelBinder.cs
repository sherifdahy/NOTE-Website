using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TaxTotalsVMModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // الحصول على القيمة المرسلة
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            // إذا كانت القيمة فارغة، أضف خطأ
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "يجب ألا تكون البيانات فارغة.");
            return Task.CompletedTask;
        }

        try
        {
            // تحويل الـ JSON string إلى ICollection<TaxTotalVM>
            var items = JsonConvert.DeserializeObject<IList<TaxTotalVM>>(value);

            // إذا كانت البيانات صالحة، قم بتعيين النتيجة
            bindingContext.Result = ModelBindingResult.Success(items);
        }
        catch (JsonException ex)
        {
            // إذا حدث خطأ أثناء التحويل، أضف خطأ
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "تنسيق البيانات غير صالح.");
        }

        return Task.CompletedTask;
    }
}