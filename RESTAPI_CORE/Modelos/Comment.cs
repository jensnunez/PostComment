namespace RESTAPI_CORE.Modelos
{
    public class Comment
    {
        public string ?id { get; set; }
        public string postId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
        
    }
}
