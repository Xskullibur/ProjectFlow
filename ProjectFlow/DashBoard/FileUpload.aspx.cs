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
                if (!IsPostBack)
                {
                    CheckFolderExist();
                    DisplayFile();
                }               
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
                if(OptionDP.SelectedIndex == 2)
                {
                    Encryption encryption = new Encryption();
                    string path = "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\(ENCRYPTED)";
                    FileUploadControl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + path + filename);
                    encryption.EncryptFileWithKey(AppDomain.CurrentDomain.BaseDirectory + path + filename, KeyTB.Text);
                }
                else
                {
                    string path = "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\";
                    FileUploadControl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + path + filename);
                }
                
                Response.Redirect("FileUpload.aspx");
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

        protected void OptionDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(OptionDP.SelectedIndex == 0)
            {
                KeyTB.Visible = false;
                GenKeyBtn.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#uploadModal').modal('show');", true);
            }
            else if(OptionDP.SelectedIndex == 1)
            {
                KeyTB.Visible = false;
                GenKeyBtn.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#uploadModal').modal('show');", true);
            }
            else if(OptionDP.SelectedIndex == 2)
            {
                KeyTB.Visible = true;
                GenKeyBtn.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#uploadModal').modal('show');", true);
            }
        }

        protected void GenKeyBtn_Click(object sender, EventArgs e)
        {
            Encryption encryption = new Encryption();
            KeyTB.Text = encryption.GenerateKey(256).Substring(0, 32);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#uploadModal').modal('show');", true);
        }
    }
}