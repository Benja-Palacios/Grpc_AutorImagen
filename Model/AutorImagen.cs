using System.ComponentModel.DataAnnotations.Schema;

namespace Grpc_AutorImagen.Model
{
    [Table("imagen")]
    public class AutorImagen
    {
        public int Id { get; set; }
        public byte[] Contenido { get; set; }
        public string IdAutorLibro {  get; set; }
    }
}
