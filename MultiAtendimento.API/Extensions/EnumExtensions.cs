using System.Reflection;
using System.ComponentModel;

namespace MultiAtendimento.API.Extensions
{
    public static class EnumExtensions
    {
        public static string ObterDescricao(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }
    }

}