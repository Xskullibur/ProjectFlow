using ProjectFlow.FileManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.DashBoard
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["StudentTeamID"] != null)
            {
                CheckFolderExist();
                DisplayFile();
            }
            else
            {
                Response.Redirect("StudentProject.aspx");
            }
        }

        public int GetTeamID()
        {
            return int.Parse(Session["StudentTeamID"].ToString());
        }

        public void CheckFolderExist()
        {
            string path = "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + path))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + path);
            }
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                CheckFolderExist();
                string filename = Path.GetFileName(FileUploadControl.FileName);
                string path = "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\";
                FileUploadControl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + path + filename);
                DisplayFile();
            }
        }  
        
        public void DisplayFile()
        {
            Info infomation = new Info();
            List<FileDetails> fileList = infomation.GetFiles(GetTeamID());
            FileGV.DataSource = fileList;
            FileGV.DataBind();
        }

        protected void FileGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = FileGV.SelectedRow;          
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\";
            string fileName = row.Cells[0].Text;
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AddHeader("Content-Disposition", "filename=" + fileName);
            Response.TransmitFile(path + fileName);
            Response.End();
        }
    }
}