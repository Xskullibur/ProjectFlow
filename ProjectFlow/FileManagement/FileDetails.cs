using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.FileManagement
{
    public class FileDetails
    {
        public string Name { get; set; }
        public string Status { get; set; }

        public FileDetails(){

        }

        public FileDetails(string name, string status)
        {
            Name = name;
            Status = status;
        }
    }
}