namespace Athanasia.Helpers
{
    public enum Roles { Admin = 1, Cliente = 2 }
    public class HelperRoles
    {
        public static int GetRolId(Roles rol)
        {
            int dato = -1;
            switch (rol)
            {
                case Roles.Admin:
                    dato = 1;
                    break;
                case Roles.Cliente:
                    dato = 2;
                    break;
            }
            return dato;
        }
    }
}
