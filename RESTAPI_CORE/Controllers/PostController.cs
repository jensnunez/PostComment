using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RESTAPI_CORE.Modelos;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using MoreLinq.Extensions;


namespace RESTAPI_CORE.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]


    public class PostController : ControllerBase
    {        

        private readonly string cadenaSQL;
        public PostController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }


        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Post> lista = new List<Post>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_list_post", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Post
                            {
                                userId = rd["userId"].ToString(),
                                title = rd["title"].ToString(),
                                body = rd["body"].ToString(),
                                id = rd["id"].ToString(),

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(lista));
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });

            }
        }

        // obtener por id

        [HttpGet]
        //[Route("Obtener")] // => Obtener?idProducto=13 | ampersand
        [Route("Obtener/{Id}")]
        public IActionResult Obtener(string Id)
        {
            List<Post> lista = new List<Post>();
            Post oPost = new Post();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_list_post", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Post
                            {
                                id = rd["Id"].ToString(),
                                userId = rd["userId"].ToString(),
                                title = rd["title"].ToString(),
                                body = rd["body"].ToString(),
                            });
                        }

                    }
                }

                oPost = lista.Where(item => item.id == Id).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oPost });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = oPost });

            }
        }




        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Post objeto)
        {
            try
            {
                
                int filas = 0;
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("[sp_save_post]", conexion);
                    cmd.Parameters.AddWithValue("title", objeto.title);
                    cmd.Parameters.AddWithValue("body", objeto.body);
                    cmd.Parameters.AddWithValue("userId", objeto.userId);

                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    filas = cmd.ExecuteNonQuery();
                }
                if (filas > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, new { mensaje = "agregado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { mensaje = "Este Post ya ha sido creado" });
                }


            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpPut]
        [Route("Editar/{postid}")]
        public IActionResult Editar(string postid, [FromBody] Post objeto)
        {
            try
            {
                int filas = 0;
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("[sp_edit_post]", conexion);
                    cmd.Parameters.AddWithValue("postid", postid);
                    cmd.Parameters.AddWithValue("title", objeto.title);
                    cmd.Parameters.AddWithValue("body", objeto.body);
                    cmd.Parameters.AddWithValue("userId", objeto.userId);

                    cmd.CommandType = CommandType.StoredProcedure;
                    filas = cmd.ExecuteNonQuery();
                }
                if (filas > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, new { mensaje = "agregado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { mensaje = "Este Post ya ha sido creado" });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }


        [HttpDelete]
        [Route("Eliminar/{postid}")]
        public IActionResult Eliminar(string postid)
        {
            try
            {
                int filas = 0;
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    var cmd = new SqlCommand("[sp_delete_post]", conexion);
                    cmd.Parameters.AddWithValue("@postid", postid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    filas = cmd.ExecuteNonQuery();
                }
                if (filas > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status302Found, new { mensaje = "Este post no existe" });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
