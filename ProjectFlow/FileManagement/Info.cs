using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectFlow.FileManagement
{
    public class Info
    {
       public IEnumerable<FileDetails> GetFiles(int TeamID)
       {
            List<FileDetails> fileList = new List<FileDetails> { };
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\";

            foreach(string file in Directory.GetFiles(path))
            {
                string status = "";
                string name = Path.GetFileName(file);

                if (name.Substring(0, 20).Equals("(ENCRYPTED_WITH_KEY)"))
                {
                    status = "Encrypted With Key";
                    name = name.Substring(20);
                }
                else if(name.Substring(0, 11).Equals("(ENCRYPTED)"))
                {
                    status = "Encrypted";
                    name = name.Substring(11);
                }
                else
                {
                    status = "Not Encrypted";
                }

                FileDetails details = new FileDetails(name, status);
                fileList.Add(details);
            }
            
            return fileList;
       }

    }
}