﻿namespace MyRecipeBook.Comunication.Responses
{
    public class ResponseErrorJson
    {
        public IList<string> Errors { get; set; }

        public ResponseErrorJson(IList<string> errors) => Errors = errors;
        public bool TokenIsExpired { get; set; }

        public ResponseErrorJson(string error) 
        {
            Errors = [error];
        }
    }
}
