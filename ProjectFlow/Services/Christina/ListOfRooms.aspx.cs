using ProjectFlow.BLL;
using ProjectFlow.Login;
using ProjectFlow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Services.Christina
{

    public enum FilterBy
    {
        SelfCreated, Default, Students
    }

    /// <summary>
    /// Display a list of room in a grid view
    /// </summary>
    public partial class ListOfRooms : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Set title
                this.SetHeader("Past created rooms");


                //Display the list of room included in this project team
                RefreshRoomsGridView();

                RefreshSearchUserList();

            }
        }

        private void RefreshSearchUserList()
        {
            var projectTeam = (Master as ServicesWithContent).CurrentProjectTeam;
            TeamMemberBLL memberBLL = new TeamMemberBLL();
            var usersList = memberBLL.GetUsersByTeamID(projectTeam.teamID);

            searchList.DataSource = usersList;

            searchList.DataBind();
        }

        private void RefreshRoomsGridView(FilterBy filterBy = FilterBy.Default, List<Student> FilterStudents = null)
        {

            var roomBLL = new RoomBLL();
            var projectTeam = (Master as ServicesWithContent).CurrentProjectTeam;
            List<Room> listOfRooms = null;

            //Filtering
            switch (filterBy)
            {
                case FilterBy.SelfCreated:
                    Student student = (User.Identity as ProjectFlowIdentity).Student;
                    listOfRooms = roomBLL.GetListOfRoomsBelongingToAProjectTeamAndCreator(projectTeam, student.aspnet_Users);
                    break;
                case FilterBy.Default:
                    listOfRooms = roomBLL.GetListOfRoomsBelongingToAProjectTeam(projectTeam);
                    break;
                case FilterBy.Students:
                    listOfRooms = roomBLL.GetListOfRoomsBelongingToAProjectTeamAndListOfStudent(projectTeam, FilterStudents.Select(x => x.aspnet_Users).ToList());
                    break;
                default:
                    break;
            }

            roomsGridView.DataSource = listOfRooms;
            roomsGridView.DataBind();

            //Store inside sessio
            Rooms = listOfRooms;

        }

        private List<Room> Rooms
        {
            get => Session["Rooms"] as List<Room>;
            set
            {
                Session["Rooms"] = value;
            }
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            //Make gridview row clickable
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(roomsGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Go to this room";
            }
        }


        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {

            int index = roomsGridView.SelectedRow.RowIndex;
            var room = Rooms[index + (roomsGridView.PageIndex * roomsGridView.PageSize)];

            //Go To Room aspx
            
            Session["DetailRoomID"] = room.roomID;
            Response.Redirect("RoomDetail.aspx");
        }

        protected void roomsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            roomsGridView.PageIndex = e.NewPageIndex;
            RefreshRoomsGridView(FilterBy.Default);
        }

        protected void searchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Student> filterList = new List<Student>();
            foreach(ListItem item in searchList.Items)
            {
                if (item.Selected)
                {
                    //filterList.Add(item.Value as Student);
                }
            }
        }
    }
}