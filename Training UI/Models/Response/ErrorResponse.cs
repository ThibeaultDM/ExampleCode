﻿namespace Training_UI.Models.Response
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }

        public override string ToString()
        {
            return $"{PropertyName}: {ErrorMessage}";
        }
    }
}