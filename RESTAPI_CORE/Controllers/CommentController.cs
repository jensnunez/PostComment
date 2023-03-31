using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RestSharp;

using RESTAPI_CORE.Modelos;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using MoreLinq.Extensions;
using System;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Reflection;

namespace RESTAPI_CORE.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
     [Authorize]
    [ApiController]


    public class CommentController : ControllerBase
    {
        private static readonly RestClient restClient = new RestClient("https://jsonplaceholder.typicode.com/posts");

        private readonly string cadenaSQL;
        public CommentController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }
        public class RootObject
        {
            public List<Post> items { get; set; }
        }


        



        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Comment> lista = new List<Comment>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_list_comments", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Comment
                            {
                                postId = rd["postId"].ToString(),
                                name = rd["name"].ToString(),
                                email = rd["email"].ToString(),
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
            List<Comment> lista = new List<Comment>();
            Comment oPost = new Comment();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_list_comments", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Comment
                            {
                                postId = rd["postId"].ToString(),
                                name = rd["name"].ToString(),
                                email = rd["email"].ToString(),
                                body = rd["body"].ToString(),
                                id = rd["id"].ToString(),

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

        // obetener todos los comentarios de un post


        [HttpGet]
        //[Route("Obtener")] // => Obtener?idProducto=13 | ampersand
        [Route("ObtenerPost/{Postid}")]
        public IActionResult ObtenerPost(string Postid)
        {
            List<Comment> lista = new List<Comment>();
            Comment oPost = new Comment();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_list_commentsPost", conexion);
                    cmd.Parameters.AddWithValue("postId", Postid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Comment
                            {
                                postId = rd["postId"].ToString(),
                                name = rd["name"].ToString(),
                                email = rd["email"].ToString(),
                                body = rd["body"].ToString(),
                                id = rd["id"].ToString(),

                            });
                        }

                    }
                }

                

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = oPost });

            }
        }



        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Comment objeto)
        {
            try
            {

                int filas = 0;
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("[sp_save_comment]", conexion);
                    cmd.Parameters.AddWithValue("name", objeto.name);
                    cmd.Parameters.AddWithValue("body", objeto.body);
                    cmd.Parameters.AddWithValue("postId", objeto.postId);
                    cmd.Parameters.AddWithValue("email", objeto.email);

                    cmd.CommandType = CommandType.StoredProcedure;
                    filas = cmd.ExecuteNonQuery();
                }
                if (filas > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, new { mensaje = "agregado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { mensaje = "Este comentario ya ha sido creado" });
                }


            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpPut]
        [Route("Editar/{postid}")]
        public IActionResult Editar(string postid, [FromBody] Comment objeto)
        {
            try
            {
                int filas = 0;
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("[sp_edit_comment]", conexion);
                    cmd.Parameters.AddWithValue("idcomments", postid);
                    cmd.Parameters.AddWithValue("name", objeto.name);
                    cmd.Parameters.AddWithValue("body", objeto.body);
                    cmd.Parameters.AddWithValue("postId", objeto.postId);
                    cmd.Parameters.AddWithValue("email", objeto.email);

                    cmd.CommandType = CommandType.StoredProcedure;
                    filas = cmd.ExecuteNonQuery();
                }
                if (filas > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, new { mensaje = "actualizado" });
                }
                else
                {
                   
                    return StatusCode(StatusCodes.Status208AlreadyReported, new { mensaje = "Este comentario ya ha sido creado" });
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
                    var cmd = new SqlCommand("[sp_delete_comment]", conexion);
                    cmd.Parameters.AddWithValue("@idcomments", postid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    filas = cmd.ExecuteNonQuery();
                }
                if (filas > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status302Found);
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
