using ProjectFlow.BLL;
using ProjectFlow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Services.Christina
{
    public partial class CreateRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SetHeader("Create Room");

                //Set placeholder for textbox
                RoomNameTextBox.Attributes.Add("placeholder", "SCRUM Meeting @ 2pm");
                RoomDescriptionTextBox.Attributes.Add("placeholder", "e.g. Discuss the potential improvement for CRM System");

                RefreshSearchUserList();

            }
        }

        protected void CreateRoomEvent(object sender, EventArgs e)
        {
            List<string> selectedAttendees = new List<string>();
            foreach (ListItem item in searchList.Items)
            {
                if (item.Selected)
                {
                    selectedAttendees.Add(item.Value);
                }
            }

            if(selectedAttendees.Count == 0)
            {
                AttendeesErrorLbl.Text = "At least one attendees need to be selected!";
                return;
            }

            Response.Redirect($"Christina.aspx?RoomName={RoomNameTextBox.Text}&RoomDescription={RoomDescriptionTextBox.Text}&Attendees={String.Join(",", selectedAttendees.ToArray())}");
        }

        private void RefreshSearchUserList()
        {
            var projectTeam = (Master as ServicesWithContent).CurrentProjectTeam;
            TeamMemberBLL memberBLL = new TeamMemberBLL();
            var usersList = memberBLL.GetUsersByTeamID(projectTeam.teamID);

            searchList.DataSource = usersList;

            searchList.DataBind();
        }

    }
}