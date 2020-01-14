using ProjectFlow.FileManagement;
using ProjectFlow.Utils.Alerts;
using ProjectFlow.Utils.Bootstrap;
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
                List<string> errorList = new List<string> { };

                if (filename.Contains("(ENCRYPTED_WITH_KEY)"))
                {
                    errorList.Add("Cannot contain (ENCRYPTED_WITH_KEY)<br>");
                }

                if (!IsKeyValid(KeyTB.Text) && OptionDP.SelectedIndex == 2)
                {
                    errorList.Add("Key must be 32 character<br>");
                }

                if (OptionDP.SelectedIndex == 2)
                {
                    
                    if(errorList.Count == 0)
                    {
                        Encryption encryption = new Encryption();
                        string path = "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\(ENCRYPTED_WITH_KEY)";
                        FileUploadControl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + path + filename);
                        encryption.EncryptFileWithKey(AppDomain.CurrentDomain.BaseDirectory + path + filename, KeyTB.Text);                       
                        Response.Redirect("FileUpload.aspx");
                        Master.ShowAlert("File successfully uploaded", BootstrapAlertTypes.SUCCESS);
                    }
                    else
                    {
                        ShowError(errorList);
                    }
                }                  
                else
                {
                    if (errorList.Count == 0)
                    {
                        string path = "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\";
                        FileUploadControl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + path + filename);
                        Response.Redirect("FileUpload.aspx");
                        Master.ShowAlert("File successfully uploaded", BootstrapAlertTypes.SUCCESS);
                    }
                    else
                    {
                        ShowError(errorList);
                    }                       
                }
                
            }
        }  
        
        public void DisplayFile()
        {
            Info infomation = new Info();
            List<FileDetails> fileList = infomation.GetFiles(GetTeamID());
            FileGV.DataSource = fileList;
            FileGV.DataBind();
        }
      
        private bool IsKeyValid(string key)
        {
            if(key.Length == 32)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ShowError(List<string> errorList)
        {
            string total = "";
            foreach(string error in errorList)
            {
                total += error;
            }
            errorLabel.Text = total;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#uploadModal').modal('show');", true);
        }
         
        protected void FileGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = FileGV.SelectedRow;
            string fileName = row.Cells[0].Text;
            TextBox key = (TextBox)row.FindControl("tableKeyTB");
            string storagePath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + Session["StudentTeamID"].ToString() + "\\";

            if (fileName.Contains("(ENCRYPTED_WITH_KEY)"))
            {
                if(key.Text.Length == 32)
                {
                    string tempPath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\Temp\\";
                    Encryption encryption = new Encryption();
                    Decryption decryption = new Decryption();

                    string theFile = storagePath + fileName;
                    string destinationFolder = tempPath + encryption.GenerateKey(256).Substring(0, 8) + "\\";

                    Directory.CreateDirectory(destinationFolder);
                    File.Copy(theFile, destinationFolder + fileName);
                    decryption.DecryptFileWithKey(destinationFolder + fileName, key.Text);
                    DownloadFile(fileName, destinationFolder);
                    //delete destination
                }
                else
                {
                    Master.ShowAlert("Key must have 32 characters", BootstrapAlertTypes.DANGER);
                }               
            }
            else
            {           
                DownloadFile(fileName, storagePath);
            }            
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
        
        private void DownloadFile(string FileName, string Path)
        {              
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AddHeader("Content-Disposition", "filename=" + FileName);
            Response.TransmitFile(Path + FileName);
            Response.End();
        }
    }
}