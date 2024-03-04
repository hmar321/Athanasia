using System.Text.RegularExpressions;

namespace Athanasia.Extension
{
    public static class Extensions
    {
        public static string Normalizar(this string cadena)
        {
            cadena=Regex.Replace(cadena, @"[^a-zA-Z0-9\s]", string.Empty).ToLower();
            return cadena;
        }
    }
}
