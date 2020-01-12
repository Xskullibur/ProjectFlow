using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectFlow.FileManagement
{
    public class Upload
    {
        public void CheckFolderExist(int TeamID)
        {
            string path = "FileManagement\\FileStorage\\" + TeamID.ToString();
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + path)){
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + path);
            }
        }

        public void UploadFile(int TeamID, string FileName)
        {
            
        }
    }
}