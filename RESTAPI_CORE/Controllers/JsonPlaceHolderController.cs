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


    public class JsonPlaceHolderController : ControllerBase
    {
        private static readonly RestClient restClient = new RestClient("https://jsonplaceholder.typicode.com/posts");

        private readonly string cadenaSQL;
        public JsonPlaceHolderController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }
        public class RootObject
        {
            public List<Post> items { get; set; }
        }


        [HttpGet]
        [Route("ListaplacePost")]
        public IActionResult GetAllPost_RestSharp()
        {
            PostM product = new PostM();
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("posts", Method.Get);
            var response = client.ExecuteGet(request);


            object dataObj = JsonConvert.DeserializeObject(response.Content);
            string json = JsonConvert.SerializeObject(dataObj);
            dynamic dynJson = JsonConvert.DeserializeObject(json);
            List<PostM> lista = new List<PostM>();
            foreach (var item in dynJson)
            {
                lista.Add(new PostM
                {
                    userId = item.userId,
                    title = item.title,
                    body = item.body,
                });
            }


            // var resultjson = JsonConvert.DeserializeObject<RootObject>(json);//aqui poner tu json (responseJSON)  

            //foreach (var item in resultjson.items)
            //{
            //    //en cada recorrido obtienes cada propiedad de var resultjson y cada uno de ellos contiene los datos del json.
            //    string rd = item.body;
            //    string rd2 = item.title;
            //}
            //using (var conexion = new SqlConnection(cadenaSQL))
            //{
            //    conexion.Open();
            //    string query = "INSERT INTO posts (userId, title, body) VALUES (@param1, @param2,@param3)";
            //    SqlCommand cmd = new SqlCommand(query, conexion);

            //    foreach (var item in lista)
            //    {
            //        cmd.Parameters.Clear();
            //        cmd.Parameters.AddWithValue("@param1", item.userId);
            //        cmd.Parameters.AddWithValue("@param2", item.title);
            //        cmd.Parameters.AddWithValue("@param3", item.body);
            //        cmd.ExecuteNonQuery();
            //    }


            //}

            var table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("userId", typeof(int));
            table.Columns.Add("title", typeof(string));
            table.Columns.Add("body", typeof(string));
            foreach (var itemDetail in lista)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.id,
                        itemDetail.userId,
                        itemDetail.title,
                        itemDetail.body
                    });
            }
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (SqlTransaction transaction = conexion.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conexion, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = "posts";
                            bulkCopy.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            conexion.Close();
                            throw;
                        }
                    }
                }
            }
            return StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(lista));
        }


        [HttpGet]
        [Route("ListaplaceComments")]
        public IActionResult GetAllComments_RestSharp()
        {
            CommentM product = new CommentM();
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("comments", Method.Get);
            var response = client.ExecuteGet(request);

            object dataObj = JsonConvert.DeserializeObject(response.Content);
            string json = JsonConvert.SerializeObject(dataObj);
            dynamic dynJson = JsonConvert.DeserializeObject(json);
            List<CommentM> lista = new List<CommentM>();
            foreach (var item in dynJson)
            {
                lista.Add(new CommentM
                {
                    id = item.id,
                    postId = item.postId,
                    name = item.name,
                    email = item.email,
                    body = item.body,                   
                });
            }

            var table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("postId", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("email", typeof(string));
            table.Columns.Add("body", typeof(string));
           
            foreach (var itemDetail in lista)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.id,
                        itemDetail.postId,
                        itemDetail.name,
                             itemDetail.email,
                        itemDetail.body,
                      
                    });
            }
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (SqlTransaction transaction = conexion.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conexion, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = "comments";
                            bulkCopy.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            conexion.Close();
                            throw;
                        }
                    }
                }
            }
            return StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(lista));
        }



    }
}
