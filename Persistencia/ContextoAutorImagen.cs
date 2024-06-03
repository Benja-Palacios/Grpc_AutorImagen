using Grpc_AutorImagen.Model;
using Microsoft.EntityFrameworkCore;

namespace Grpc_AutorImagen.Persistencia
{
    public class ContextoAutorImagen : DbContext
    {
        public ContextoAutorImagen(DbContextOptions<ContextoAutorImagen> options) : base(options)
        {
        }

        public DbSet<AutorImagen> AutoresImagenes { get; set; }
    }
}
