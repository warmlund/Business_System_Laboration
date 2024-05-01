using System.ComponentModel;

namespace Business_System_Laboration_4.BaseClasses
{
    public static class EnumDescriptionExtractor<TEnum>
    {
        public static string GetDescription(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
