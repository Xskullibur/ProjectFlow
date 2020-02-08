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
                FileInfo myFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\" + name);
                double sizeInByte = (myFile.Length)/1024;
                string date = File.GetCreationTime(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\" + name).ToString("MM/dd/yyyy hh:mm tt");

                if (name.StartsWith("(ENCRYPTED_WITH_KEY)"))
                {
                    status = "Encrypted With Key";
                    name = name.Substring(20);
                }
                else if(name.StartsWith("(ENCRYPTED)"))
                {
                    status = "Encrypted";
                    name = name.Substring(11);
                }
                else if(name.StartsWith("(PLAIN)"))
                {
                    status = "Not Encrypted";
                    name = name.Substring(7);
                }

                FileDetails details = new FileDetails(name, status, sizeInByte.ToString() + " KB", date);
                fileList.Add(details);
            }
            
            return fileList;
       }

        public IEnumerable<FileDetails> SearchFiles(int TeamID, string search)
        {
            List<FileDetails> fileList = new List<FileDetails> { };
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\";

            foreach (string file in Directory.GetFiles(path))
            {
                string status = "";
                string name = Path.GetFileName(file);               
                
                FileInfo myFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\" + name);
                double sizeInByte = (myFile.Length) / 1024;
                string date = File.GetCreationTime(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\" + name).ToString("MM/dd/yyyy hh:mm tt");

                if (name.StartsWith("(ENCRYPTED_WITH_KEY)"))
                {
                    status = "Encrypted With Key";
                    name = name.Substring(20);
                }
                else if (name.StartsWith("(ENCRYPTED)"))
                {
                    status = "Encrypted";
                    name = name.Substring(11);
                }
                else if (name.StartsWith("(PLAIN)"))
                {
                    status = "Not Encrypted";
                    name = name.Substring(7);
                }

                if (name.ToLower().Contains(search.ToLower()))
                {
                    FileDetails details = new FileDetails(name, status, sizeInByte.ToString() + " KB", date);
                    fileList.Add(details);
                }             
            }

            return fileList;
        }


        //get file names
        public List<string> getfilenames(int TeamID)
        {
            List<string> filenamelist = new List<string>();
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + TeamID.ToString() + "\\";
            foreach (string file in Directory.GetFiles(path))
            {
                filenamelist.Add(Path.GetFileName(file));
            }
            return filenamelist;
        }
    }
}