using Grpc.Core;
using Grpc_AutorImagen;
using Grpc_AutorImagen.Model;
using Grpc_AutorImagen.Persistencia;
using Microsoft.EntityFrameworkCore;

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
                    Contenido = request.Contenido.ToByteArray(),
                    IdAutorLibro = (int)request.IdAutorLibro
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

        public override async Task<ImagenResponse> ObtenerImagen(ImagenConsultaRequest request, ServerCallContext context)
        {
            var imagen = await _contexto.AutoresImagenes
                                        .FirstOrDefaultAsync(i => i.IdAutorLibro == request.IdAutorLibro);
            if (imagen == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Imagen no encontrada"));
            }

            return new ImagenResponse
            {
                Contenido = Google.Protobuf.ByteString.CopyFrom(imagen.Contenido),
                IdAutorLibro = imagen.IdAutorLibro
            };
        }

        public override async Task<ListaImagenesResponse> ObtenerTodasImagenes(EmptyRequest request, ServerCallContext context)
        {
            var imagenes = await _contexto.AutoresImagenes.ToListAsync();
            var respuesta = new ListaImagenesResponse();

            foreach (var imagen in imagenes)
            {
                respuesta.Imagenes.Add(new ImagenResponse
                {
                    Contenido = Google.Protobuf.ByteString.CopyFrom(imagen.Contenido),
                    IdAutorLibro = imagen.IdAutorLibro
                });
            }

            return respuesta;
        }


    }
}
