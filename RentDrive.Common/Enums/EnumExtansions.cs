using System.ComponentModel;
using System.Reflection;

namespace RentDrive.Common.Enums
{
    public static class EnumExtansions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field?.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return value.ToString();
        }
    }
}
