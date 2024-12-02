using System.Resources;

namespace PWMS.Application.Common.Models;

public static class EnumTranslator
{
    public static string GetDisplayDescription(this Enum enumValue, Type resourceType)
    {
        var resourceManager = new ResourceManager(resourceType);

        var translatedDescription = resourceManager.GetString(enumValue.ToString());

        if (string.IsNullOrEmpty(translatedDescription))
            return enumValue.ToString();

        return translatedDescription;
    }
}
