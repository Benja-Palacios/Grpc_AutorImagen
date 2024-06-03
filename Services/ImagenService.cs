using Grpc.Core;
using Grpc_AutorImagen;
using Grpc_AutorImagen.Model;
using Grpc_AutorImagen.Persistencia;

namespace Grpc_AutorImagen.Services
{
    public class ImagenService : AutorImagenService.AutorImagenServiceBase
    {
        private readonly ContextoAutorImagen _contexto;
        public ImagenService(ContextoAutorImagen contexto)
        {
            _contexto = contexto;
        }

        public override async Task<Respuesta> GuardarImagen(ImagenRequest request, ServerCallContext context)
        {
            try
            {
                // Guardar la imagen en la base de datos
                var imagen = new AutorImagen
                {
                    Contenido = request.Contenido.ToByteArray()
                };

                _contexto.AutoresImagenes.Add(imagen);
                await _contexto.SaveChangesAsync();

                return new Respuesta { Mensaje = "Imagen guardada correctamente." };
            }
            catch (Exception ex)
            {
                // Manejar cualquier error y devolver un mensaje de error
                return new Respuesta { Mensaje = "Error al guardar la imagen: " + ex.Message };
            }
        }

    }
}
