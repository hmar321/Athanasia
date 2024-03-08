using System.Text;
using System.Text.RegularExpressions;

namespace Athanasia.Extension
{
    public static class Extensions
    {
        public static string Limpiar(this string cadena)
        {

            string limpio = cadena.Normalize(NormalizationForm.FormD).ToLower().Trim();
            limpio = limpio.Replace("á", "a")
                  .Replace("é", "e")
                  .Replace("í", "i")
                  .Replace("ó", "o")
                  .Replace("ú", "u")
                  .Replace("ñ", "n")
                  .Replace("ü", "u")
                  .Replace("ä", "a")
                  .Replace("ö", "o")
                  .Replace("ü", "u")
                  .Replace("ß", "ss")
                  .Replace(" ", "").ToUpper();
            limpio = Regex.Replace(limpio, @"[^A-Z0-9\s]", "");
            return limpio;
        }
    }
}
