namespace Athanasia.Helpers
{
    public enum Formatos { TapaDura = 0, TapaBlanda = 1, Ebook = 2 }
    public class HelperFormatos
    {
        public static string GetFormato(Formatos formato)
        {
            string dato = "";
            switch (formato)
            {
                case Formatos.TapaDura:
                    dato = "Tapa Dura";
                    break;
                case Formatos.TapaBlanda:
                    dato = "Tapa Blanda";
                    break;
                case Formatos.Ebook:
                    dato = "Ebook";
                    break;
            }
            return dato;
        }
    }
}
