using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Utils
{
    public static class FileExtensions
    {
        public const string TXT = "txt";
        public const string JSON = "json";
        public const string UNKNOWN = "unknown";
    }
    public static class ErrorMessages{

        public const string GENERIC_ERROR = "An error ocurred";
        public const string ACCESS_DENIED = "Access denied";
        public const string FILE_CREATION_ERROR = "Error creating the file"; 

    }
}
