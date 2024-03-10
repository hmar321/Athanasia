namespace Athanasia.Helpers
{
    public enum Estados { Activo = 1, Pendiente = 2, Eliminado = 3 }
    public class HelperEstados
    {
        public static int GetEstadoId(Estados estado)
        {
            int dato = -1;
            switch (estado)
            {
                case Estados.Activo:
                    dato = 1;
                    break;
                case Estados.Pendiente:
                    dato = 2;
                    break;
                case Estados.Eliminado:
                    dato = 3;
                    break;
            }
            return dato;
        }
    }
}
