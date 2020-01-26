using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils;

namespace ProjectFlow.Services.Christina
{
    public partial class Christina : System.Web.UI.Page
    {

        public List<ActionItem> ActionItems { get => ViewState["ActionItems"] as List<ActionItem>; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get speakers belong to the current project team
                ServicesWithContent servicesWithContent = this.Master as ServicesWithContent;

                Project selectedProject = servicesWithContent.CurrentProject;
                var projectFlowIdentity = this.User.Identity as ProjectFlowIdentity;
                Student student = projectFlowIdentity.Student;

                ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();

                ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByStudentAndProject(student, selectedProject);

                //Create all the speakers in client side
                Student[] students = projectTeamBLL.GetTeamMembersFromProjectTeam(projectTeam).Select(tm => tm.Student).ToArray();
                InjectSpeaker(students);

                //Create viewstate list
                ViewState["ActionItems"] = new List<ActionItem>();

            }


        }

        /// <summary>
        /// Display room informations
        /// </summary>
        private void DisplayRoomInfo(Room room)
        {
            Session["Room"] = room;
            MeetingDate.Text = room.creationDate.ToShortDateString();
            MeetingTime.Text = room.creationDate.ToShortTimeString();

            Project project = (Master as ServicesWithContent).CurrentProject;
            Student student = (User.Identity as ProjectFlowIdentity).Student;


            ProjectTeamBLL projectTeamBLL = new ProjectTeamBLL();
            ProjectTeam projectTeam = projectTeamBLL.GetProjectTeamByStudentAndProject(student, project);
            StudentBLL studentBLL = new StudentBLL();

            AttendeesLbl.Text = String.Join(",", projectTeamBLL.GetTeamMembersFromProjectTeam(projectTeam).Select(x => studentBLL.GetStudentByTeamMember(x).lastName));

            MadeByLbl.Text = student.aspnet_Users.UserName;
        }

        /// <summary>
        /// Create speaker on the client side so all the speaker are displayed
        /// 
        /// Called during Page_Load
        /// 
        /// </summary>
        /// <param name="speakers"></param>
        private void InjectSpeaker(Student[] speakers)
        {

            string createSpeakers = @"
                <script type=""text/javascript"">
                    $(document).ready(function (){
            ";

            aspnet_UsersBLL aspnet_UsersBLL = new aspnet_UsersBLL();

            foreach (var speaker in speakers)
            {

                aspnet_Users aspnet_Users = aspnet_UsersBLL.Getaspnet_UsersByStudent(speaker);

                createSpeakers += $"speakers_circles.push(create_speaker('{aspnet_Users.UserName}', null));";
            }
            createSpeakers += @"
                    });
                </script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "create_speakers", createSpeakers, false);
        }

        protected void ExecuteEvent(object sender, EventArgs e)
        {
            string text = ExecuteTextBox.Text;

            InsertActionItems(text);

        }

        private void InsertActionItems(string text)
        {
            SemanticsParser parser = new SemanticsParser();

            try
            {
                List<ParseItem> parseItems = parser.Parse(text);
                ErrMsg.Text = "";
                ErrLine.Text = "";

                foreach (var parseItem in parseItems)
                {
                    var actionItem = parseItem.QueryActionItem;
                    if (parseItem.ParseItemType == ParseItemType.CREATE)
                    {
                        

                        var room = Session["Room"] as Room;

                        //Add action item into database
                        ActionItemBLL actionItemBLL = new ActionItemBLL();
                        actionItemBLL.AddActionItem(new RoomActionItem
                        {
                            personName = actionItem.PersonName,
                            topic = actionItem.Topic,
                            type = actionItem.Type,
                            roomID = room.roomID
                        });

                        AddNewActionItem(actionItem);
                    }
                    else if (parseItem.ParseItemType == ParseItemType.DELETE)
                    {

                        ////Locate index with the parsed object
                        //int index = ActionItems.First(parseItem.QueryActionItem);

                        //materialTable.RemoveRow(index);
                    }

                }

            }
            catch (ParseException ex)
            {
                ErrMsg.Text = ex.Message;
                if (!String.IsNullOrEmpty(ex.ErrorLine))
                {
                    ErrLine.Text = "<code>" + ErrorAt(ex.ErrorLine, ex.At, ex.Length) + "</code>";
                }
                else
                {
                    ErrLine.Text = "";
                }
            }
        }

        private void AddNewActionItem(ActionItem actionItem)
        {

            materialTable.AddRow(new string[] {
                            actionItem.Type,
                            actionItem.PersonName,
                            actionItem.Topic
                        });
            ActionItems.Add(actionItem);
        }

        private string[] ErrorIfMissingAttributeForActionItem(ActionItem actionItem)
        {

            var missingAttributes = new List<string>();

            if (String.IsNullOrWhiteSpace(actionItem.PersonName))
            {
                missingAttributes.Add("Person Name");
            }
            if (String.IsNullOrWhiteSpace(actionItem.Topic))
            {
                missingAttributes.Add("Topic");
            }
            if (String.IsNullOrWhiteSpace(actionItem.Type))
            {
                missingAttributes.Add("Type");
            }
            return missingAttributes.ToArray();
        }

        private string ErrorAt(string errorMsg, int at, int length = 1)
        {
            string msg = errorMsg.Substring(0, at);
            msg += @"<span class=""curly-underline"">" + errorMsg.Substring(at, length) + "</span>";
            msg += errorMsg.Substring(at + length, errorMsg.Length - (at + length));
            return msg;
        }

        protected void ShowCreateActionItemModalEvent(object sender, EventArgs e)
        {
            ShowModal();
        }

        private void ShowModal()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "actionItemCreateModal", "updateCode(); $('#actionItemCreateModal').modal('show');", true);
        }

        private void HideModal()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "actionItemCreateModal", "updateCode(); $('#actionItemCreateModal').modal('hide');", true);
        }

        protected void DeleteEvent(object sender, EventArgs e)
        {
            List<ActionItem> itemsToBeDeleted = new List<ActionItem>();
            bool[] selectedValues = materialTable.SelectedValues;
            for (int i = 0; i < selectedValues.Length; i++)
            {
                bool value = selectedValues[i];

                if (value)
                {
                    itemsToBeDeleted.Add(ActionItems[i]);
                }
            }

            foreach (var item in itemsToBeDeleted)
            {
                ActionItems.Remove(item);
            }

            
        }

        protected void ToggleSelectEvent(object sender, EventArgs e)
        {
            materialTable.ToSelectMode();
        }

        protected void CreateActionItemEvent(object sender, EventArgs e)
        {
            string text = GeneratedCodeLbl.Value;
            typeTxtBox.Text = "";
            personNameTxtBox.Text = "";
            topicTxtBox.Text = "";

            InsertActionItems(text);
            HideModal();
        }

        protected void UpdateRoomDetailEvent(object sender, EventArgs e)
        {
            RoomBLL roomBLL = new RoomBLL();

            int roomID;

            if (int.TryParse(RoomID.Value, out roomID))
            {
                Room room = roomBLL.GetRoomByRoomID(roomID);
                DisplayRoomInfo(room);
            }
            else
            {
                Response.Redirect("~/InvalidRequest.aspx");
            }
        }
    }
}