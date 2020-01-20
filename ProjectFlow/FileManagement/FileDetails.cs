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
        public string Size { get; set; }
        public string Date { get; set; }

        public FileDetails(){

        }

        public FileDetails(string name, string status, string size, string date)
        {
            Name = name;
            Status = status;
            Size = size;
            Date = date;
        }
    }
}