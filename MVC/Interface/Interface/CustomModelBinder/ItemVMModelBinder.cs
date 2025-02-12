using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ItemVMModelBinder : IModelBinder
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
            // تحويل الـ JSON string إلى ICollection<ItemVM>
            var items = JsonConvert.DeserializeObject<ICollection<ItemVM>>(value);

            // التحقق من أن الـ array ليست فارغة
            if (items == null || items.Count == 0)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "يجب أن تحتوي البيانات على عنصر واحد على الأقل.");
                return Task.CompletedTask;
            }

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