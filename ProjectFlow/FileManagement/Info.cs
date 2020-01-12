using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectFlow.FileManagement
{
    public class Info
    {
       public List<FileDetails> GetFiles(int TeamID)
       {
            List<FileDetails> fileList = new List<FileDetails> { };
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\";

            foreach(string file in Directory.GetFiles(path))
            {
                FileDetails details = new FileDetails(Path.GetFileName(file));
                fileList.Add(details);
            }
            
            return fileList;
       }
    }
}