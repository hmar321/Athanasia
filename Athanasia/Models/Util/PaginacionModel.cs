namespace Athanasia.Models.Util
{
    public class PaginacionModel <T>
    {
        public List<T> Lista { get; set; }
        public int NumeroRegistros { get; set; }
    }
}
