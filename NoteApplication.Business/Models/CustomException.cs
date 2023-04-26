using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Business.Models
{
    public class CustomException: Exception
    {
        public int StatusCode { get; set; }
        public string ErorMessage { get; set; }
        public CustomException(int statusCode, string ErorMessage)
        {
            this.StatusCode = statusCode;
            this.ErorMessage = ErorMessage;
        }

    }
}
