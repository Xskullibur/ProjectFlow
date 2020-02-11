using ProjectFlow.FileManagement;
using ProjectFlow.Utils;
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
            if (!Page.IsPostBack)
            {
                CheckFolderExist();
                DisplayFile();
                this.SetHeader("Encrypted File Sharing");
            }
            AddPrompt();
        }

        public int GetTeamID()
        {
            return (Master as ServicesWithContent).CurrentProjectTeam.teamID;
        }

        public void CheckFolderExist()
        {
            string path = "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + path))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + path);
            }
        }

        public void AddPrompt()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bootstrap-confirm", "$('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]'});", true);           
        }

        private void HideModel()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#uploadModal').modal('hide')", true);
        }

        private void ShowModel()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('#uploadModal').modal('show');", true);
        }

        private void ShowDecryptionModel()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#decryptionModal').modal('show')", true);
        }

        private void HideDecryptionModel()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "taskModal", "$('#decryptionModal').modal('hide')", true);
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                CheckFolderExist();
                Encryption encryption = new Encryption();
                string filename = Path.GetFileName(FileUploadControl.FileName);
                string path = "";
                string savedLocation = "";
                List<string> errorList = new List<string> { };

                if (filename.Contains("(ENCRYPTED_WITH_KEY)"))
                {
                    errorList.Add("Cannot contain (ENCRYPTED_WITH_KEY)<br>");
                }

                if (!IsKeyValid(KeyTB.Text) && OptionDP.SelectedIndex == 0)
                {
                    errorList.Add("Key must be 32 character<br>");
                }

                while(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\(ENCRYPTED_WITH_KEY)" +  filename)
                    || File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\(ENCRYPTED)" + filename)
                    || File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\(PLAIN)" + filename))               
                {
                    filename = "(copy)" + filename ;
                }

                if (OptionDP.SelectedIndex == 1)
                {
                    
                    if(errorList.Count == 0)
                    {                  
                        path = "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\(ENCRYPTED_WITH_KEY)";
                        savedLocation = AppDomain.CurrentDomain.BaseDirectory + path + filename;
                        FileUploadControl.SaveAs(savedLocation);
                        encryption.EncryptFileWithKey(savedLocation, KeyTB.Text);
                        HideModel();
                        Master.ShowAlert("File successfully uploaded", BootstrapAlertTypes.SUCCESS);                        
                    }
                    else
                    {
                        ShowError(errorList);
                    }
                }
                else if(OptionDP.SelectedIndex == 0)
                {
                    path = "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\(ENCRYPTED)";
                    savedLocation = AppDomain.CurrentDomain.BaseDirectory + path + filename;
                    FileUploadControl.SaveAs(savedLocation);
                    encryption.EncryptFile(savedLocation);
                    HideModel();
                    Master.ShowAlert("File successfully uploaded", BootstrapAlertTypes.SUCCESS);
                }                  
                else
                {
                    if (errorList.Count == 0)
                    {
                        path = "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\(PLAIN)";
                        FileUploadControl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + path + filename);
                        HideModel();
                        Master.ShowAlert("File successfully uploaded", BootstrapAlertTypes.SUCCESS);                      
                    }
                    else
                    {
                        ShowError(errorList);
                    }                       
                }
                DisplayFile();
                ClearModel();
            }
        }  
        
        public void DisplayFile()
        {
            Info infomation = new Info();
            List<FileDetails> fileList = infomation.GetFiles(GetTeamID());
            FileGV.DataSource = fileList;
            FileGV.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bootstrap-confirm", "$('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]'});", true);
        }

        public void SearchFile(string search)
        {
            Info infomation = new Info();
            List<FileDetails> fileList = infomation.SearchFiles(GetTeamID(), search);
            FileGV.DataSource = fileList;
            FileGV.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bootstrap-confirm", "$('[data-toggle=confirmation]').confirmation({rootSelector: '[data-toggle=confirmation]'});", true);
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
            ShowModel();
        }
         
        protected void FileGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Info infomation = new Info();
            List<FileDetails> fileList = infomation.GetFiles(GetTeamID());

            GridViewRow row = FileGV.SelectedRow;            
            TextBox key = (TextBox)row.FindControl("tableKeyTB");

            string fileName = fileList[row.RowIndex].Name;
            
            Encryption encryption = new Encryption();
            Decryption decryption = new Decryption();
            string storagePath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\";
          
            if (row.Cells[2].Text.Equals("Encrypted With Key"))
            {
                fileName = "(ENCRYPTED_WITH_KEY)" + fileName;
                string tempPath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\Temp\\";

                string theFile = storagePath + fileName;
                string destinationFolder = tempPath + encryption.GenerateKey(256).Substring(0, 8) + "\\";

                string newFileName = RemoveAdditionalInfo(fileName, 1);

                Directory.CreateDirectory(destinationFolder);
                //File.Copy(theFile, destinationFolder + newFileName);

                ViewState["theFile"] = theFile;
                ViewState["destinationFolder"] = destinationFolder;
                ViewState["newFileName"] = newFileName;

                ShowDecryptionModel();

            }
            else if (row.Cells[2].Text.StartsWith("Encrypted"))
            {

                fileName = "(ENCRYPTED)" + fileName;
                string tempPath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\Temp\\";

                string theFile = storagePath + fileName;
                string destinationFolder = tempPath + encryption.GenerateKey(256).Substring(0, 8) + "\\";

                string newFileName = RemoveAdditionalInfo(fileName, 2);

                Directory.CreateDirectory(destinationFolder);
                File.Copy(theFile, destinationFolder + newFileName);

                decryption.DecryptFile(destinationFolder + newFileName);
                DownloadFile(newFileName, destinationFolder, true);
            }
            else
            {           
                DownloadFile("(PLAIN)" + fileName, storagePath, false);
            }            
        }

        protected void OptionDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(OptionDP.SelectedIndex == 0)
            {
                KeyTB.Visible = false;
                GenKeyBtn.Visible = false;
                ShowModel();
            }
            else if(OptionDP.SelectedIndex == 2)
            {
                KeyTB.Visible = false;
                GenKeyBtn.Visible = false;
                ShowModel();
            }
            else if(OptionDP.SelectedIndex == 1)
            {
                KeyTB.Visible = true;
                GenKeyBtn.Visible = true;
                ShowModel();
            }
        }

        protected void GenKeyBtn_Click(object sender, EventArgs e)
        {
            Encryption encryption = new Encryption();
            KeyTB.Text = encryption.GenerateKey(256).Substring(0, 32);
            ShowModel();
        }      
        
        private void DownloadFile(string FileName, string Path, bool isEncrypted)
        {              
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AddHeader("Content-Disposition", "filename=" + FileName);
            Response.TransmitFile(Path + FileName);
            Response.Flush();
            if (isEncrypted)
            {
                File.Delete(Path + FileName);
            }            
            Response.End();
        }

        private void ClearModel()
        {
            KeyTB.Text = "";
            KeyTB.Visible = false;
            GenKeyBtn.Visible = false;
            OptionDP.SelectedIndex = 0;
            errorLabel.Text = "";
        }

        private string RemoveAdditionalInfo(string path, int option)
        {
            if(option == 1)  //encrypted with key
            {
                return path.Replace("(ENCRYPTED_WITH_KEY)", "");
            }
            else if(option == 2)
            {
                return path.Replace("(ENCRYPTED)", "");
            }
            else
            {
                return path.Replace("(PLAIN)", "");
            }
        }

        protected void FileGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Info infomation = new Info();
            List<FileDetails> fileList = infomation.GetFiles(GetTeamID());

            GridViewRow row = (GridViewRow)FileGV.Rows[e.RowIndex];
            string fileName = fileList[row.RowIndex].Name;
            string storagePath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\";

            if (row.Cells[2].Text.Equals("Encrypted With Key"))
            {
                fileName = "(ENCRYPTED_WITH_KEY)" + fileName;
                File.Delete(storagePath + fileName);
            }
            else if (row.Cells[2].Text.StartsWith("Encrypted"))
            {
                fileName = "(ENCRYPTED)" + fileName;
                File.Delete(storagePath + fileName);
            }
            else
            {
                fileName = "(PLAIN)" + fileName;
                File.Delete(storagePath + fileName);
            }
            Master.ShowAlert("File deleted", BootstrapAlertTypes.DANGER);
            DisplayFile();
        }

      
        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FileUpload.aspx");
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchFile(SearchTB.Text);
        }

        protected void showAllBtn_Click(object sender, EventArgs e)
        {
            DisplayFile();
        }

        public string ShowIcon(string input)
        {
            if (input.Equals("Encrypted"))
            {
                return "<i style=\"color: red;\" class=\"fas fa-lg fa-lock\"></i>";
            }
            else if (input.Equals("Encrypted With Key"))
            {
                return "<i style=\"color: red; \" class=\"fas fa-lg fa-key\"></i>";
            }
            else
            {
                return "<i style=\"color: red;\" class=\"fas fa-lg fa-lock-open\"></i>";
            }
        }

        protected void keyDownloadBtn_Click(object sender, EventArgs e)
        {
            Encryption encryption = new Encryption();
            Decryption decryption = new Decryption();
            if (deKeyTB.Text.Length == 32)
            {                
                try
                {
                    File.Copy(ViewState["theFile"].ToString(), ViewState["destinationFolder"].ToString() + ViewState["newFileName"].ToString());
                    decryption.DecryptFileWithKey(ViewState["destinationFolder"].ToString() + ViewState["newFileName"].ToString(), deKeyTB.Text);
                    DownloadFile(ViewState["newFileName"].ToString(), ViewState["destinationFolder"].ToString(), true);
                }
                catch (System.Security.Cryptography.CryptographicException exception)
                {
                    Master.ShowAlert("Key Is Wrong, decryption failed", BootstrapAlertTypes.DANGER);
                }
            }
            else
            {
                Master.ShowAlert("Key must have 32 characters", BootstrapAlertTypes.DANGER);
            }
            deKeyTB.Text = "";
            HideDecryptionModel();
        }

        protected void FileGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FileGV.PageIndex = e.NewPageIndex;
            DisplayFile();
        }

        protected void FileGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            FileGV.EditIndex = -1;
            DisplayFile();
        }

        protected void FileGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = FileGV.Rows[e.RowIndex];           
            TextBox newName = (TextBox)row.FindControl("editNameTB");

            Info infomation = new Info();
            List<FileDetails> fileList = infomation.GetFiles(GetTeamID());

            string fileName = fileList[e.RowIndex].Name;
            string newFileName = newName.Text;
            
            string storagePath = AppDomain.CurrentDomain.BaseDirectory + "\\FileManagement\\FileStorage\\" + GetTeamID().ToString() + "\\";
            try
            {
                if (newFileName.Equals("") || newFileName == null)
                {
                    Master.ShowAlert("File Name cannot be empty", BootstrapAlertTypes.DANGER);
                }
                else
                {
                    if (row.Cells[2].Text.Equals("Encrypted With Key"))
                    {
                        fileName = storagePath + "(ENCRYPTED_WITH_KEY)" + fileName;
                        newFileName = storagePath + "(ENCRYPTED_WITH_KEY)" + newFileName;
                        if (File.Exists(newFileName))
                        {
                            Master.ShowAlert("File Name already exist!", BootstrapAlertTypes.DANGER);
                        }
                        else
                        {
                            if (Path.HasExtension(newFileName))
                            {
                                File.Move(fileName, newFileName);
                                Master.ShowAlert("File Name Renamed!", BootstrapAlertTypes.SUCCESS);
                            }
                            else
                            {
                                Master.ShowAlert("File Name no extension", BootstrapAlertTypes.DANGER);
                            }

                        }
                    }
                    else if (row.Cells[2].Text.StartsWith("Encrypted"))
                    {
                        fileName = storagePath + "(ENCRYPTED)" + fileName;
                        newFileName = storagePath + "(ENCRYPTED)" + newFileName;
                        if (File.Exists(newFileName))
                        {
                            Master.ShowAlert("File Name already exist!", BootstrapAlertTypes.DANGER);
                        }
                        else
                        {
                            if (Path.HasExtension(newFileName))
                            {
                                File.Move(fileName, newFileName);
                                Master.ShowAlert("File Name Renamed!", BootstrapAlertTypes.SUCCESS);
                            }
                            else
                            {
                                Master.ShowAlert("File Name no extension", BootstrapAlertTypes.DANGER);
                            }
                        }
                    }
                    else
                    {
                        fileName = storagePath + "(PLAIN)" + fileName;
                        newFileName = storagePath + "(PLAIN)" + newFileName;
                        if (File.Exists(newFileName))
                        {
                            Master.ShowAlert("File Name already exist!", BootstrapAlertTypes.DANGER);
                        }
                        else
                        {
                            if (Path.HasExtension(newFileName))
                            {
                                File.Move(fileName, newFileName);
                                Master.ShowAlert("File Name Renamed!", BootstrapAlertTypes.SUCCESS);
                            }
                            else
                            {
                                Master.ShowAlert("File Name no extension", BootstrapAlertTypes.DANGER);
                            }
                        }
                    }
                }
            }
            catch (System.ArgumentException)
            {
                Master.ShowAlert("File Name contain illegal characters", BootstrapAlertTypes.DANGER);
            }
                       
            FileGV.EditIndex = -1;
            DisplayFile();
        }

        protected void FileGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            FileGV.EditIndex = e.NewEditIndex;
            DisplayFile();
        }
    }
}