using Athanasia.Data;
using Athanasia.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

#region VIEWS
//create view V_LIBRO
//as
//select

//    ISNULL(l.ID_LIBRO, -1) ID_LIBRO,
//    ISNULL(l.TITULO, 'Sin título') TITULO,
//    ISNULL(l.SINOPSIS, 'Sin sinopsis') SINOPSIS,
//    ISNULL(l.FECHA_PUBLICACION, '1-01-01') FECHA_PUBLICACION,
//    ISNULL(l.PORTADA, 'Sin portada') PORTADA,
//    ISNULL(c.NOMBRE, 'Sin genero') CATEGORIA,
//    ISNULL(a.NOMBRE, 'Autor desconocido') AUTOR,
//    STRING_AGG(g.NOMBRE, ', ') GENEROS,
//    ISNULL(s.NOMBRE, 'Sin saga') SAGA
//from LIBRO l
//left join CATEGORIA c on l.ID_CATEGORIA=c.ID_CATEGORIA
//left join AUTOR a on l.ID_AUTOR=a.ID_AUTOR
//left join SAGA s on s.ID_SAGA=l.ID_SAGA
//left join GENEROS_LIBROS gl on gl.ID_LIBRO=l.ID_LIBRO
//left join GENERO g on g.ID_GENERO=gl.ID_GENERO
//GROUP BY l.ID_LIBRO, l.TITULO, c.NOMBRE, a.NOMBRE, s.NOMBRE, l.SINOPSIS, l.FECHA_PUBLICACION, l.PORTADA
#endregion
namespace Athanasia.Repositories
{
    public class RepositoryAthanasia
    {
        private AthanasiaContext context;

        public RepositoryAthanasia(AthanasiaContext context)
        {
            this.context = context;
        }

        public async Task<List<LibroView>> GetLibrosViewAsync()
        {
            var consulta = from datos in this.context.LibrosView
                           select datos;
            return await consulta.ToListAsync();
        }
    }
}
