﻿namespace RESTAPI_CORE.Modelos
{
    public class CommentM
    {

        public int ? id { get; set; }
        public int postId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }
}