1. crear una base de datos puede llevar el nombre que quieran
2. correr script api2 sobre la nueva base de datos.
3. descargar el proyecto del repositorio 
4. favor definir la ruta de conexion a la base de datos en el archivo de conexion appsettings.json
5. debe instalar las dependencias del proyecto:
NewtonsftJson , FluentValitation.aspnetcore, systemdatasqlclient, systemIdentityModel, UtenticationJwt, RestSharp
5. ejecutar api desde visual studio 2022 el net core es 6
6. la aplicacion tiene autenticacion por jwt
7. el primer endpoint es http://localhost:5124/api/Autenticacion/Validar
este no requiere token y simplemente va a validar que el usuario este creado en la tabla usuarios
nota: para los demas endpoint es requerido el token

para el crud post los endpoint son:
http://localhost:5124/api/Post/Lista es por get 
http://localhost:5124/api/Post/Obtener/{postid} es por get y lleva parametro el postid
http://localhost:5124/api/Post/Guardar es por post el json es:
{
  "userId": "string",
  "id": "string",
  "title": "string",
  "body": "string"
}
http://localhost:5124/api/Post/Editar/{postid} es por put
{
  "userId": "string",
  "id": "string",
  "title": "string",
  "body": "string"
}
http://localhost:5124/api/Post/Eliminar/{postid} es por delete 

para el crud comments los endpoint son:
http://localhost:5124/api/Comment/Lista es por get
http://localhost:5124/api/Comment/Obtener/{id} es por get
http://localhost:5124/api/Comment/ObtenerPost/{postid} es por get
http://localhost:5124/api/Comment/Guardar es por post el json es:
{
  "id": "string",
  "postId": "string",
  "name": "string",
  "email": "string",
  "body": "string"
}
http://localhost:5124/api/Comment/Editar/{postid} es por put  el json es:
{
  "id": "string",
  "postId": "string",
  "name": "string",
  "email": "string",
  "body": "string"
}
http://localhost:5124/api/Comment/Eliminar/{postid} es por delete  el json es:

los siguientes dos endpoint son para llenar masivamente la base de datos consumiendo la api https://jsonplaceholder.typicode.com la cual obtiene un json lo serializa y luego lo migra a la base de datos local, el insert es optimo porque cuando obtengo el token lo serializo y lo itero convirtiendolo en una lista luego dicha lista la asigno a un datable por medio de un foreach esto con el fin de obtener un objeto datatable con los datos del json inicial y luego hago una sola peticion a la base de datos y genera un insert masivo, otra forma de hacer era en el mismo forech por medio de una variable string concatener la instruccion "inser into post values(@para, @param, etc)" la cadena al final contendria la instruccion de un insert masivo y luego hacer una consulta a la base de datos y generarla.
http://localhost:5124/api/JsonPlaceHolder/ListaplacePost
http://localhost:5124/api/JsonPlaceHolder/ListaplaceComments

