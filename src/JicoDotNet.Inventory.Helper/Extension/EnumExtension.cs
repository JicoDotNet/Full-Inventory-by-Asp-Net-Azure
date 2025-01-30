namespace System
{
    using ComponentModel;
    using Linq;
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            return enumValue == null ? null
                : (
                    enumValue.GetType()
                        .GetMember(enumValue.GetType().ToString())[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute
                )?.Description;
        }
    }
}