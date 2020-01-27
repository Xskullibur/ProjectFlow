using ProjectFlow.BLL;
using ProjectFlow.Login;
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
        SelfCreated, Default
    }

    /// <summary>
    /// Display a list of room in a grid view
    /// </summary>
    public partial class ListOfRooms : System.Web.UI.Page
    {

        private static int PAGE_SIZE = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Display the list of room included in this project team
                RefreshRoomsGridView();
            }
        }

        private void RefreshRoomsGridView(FilterBy filterBy = FilterBy.Default, int pageIndex = 0)
        {

            var roomBLL = new RoomBLL();
            var projectTeam = (Master as ServicesWithContent).CurrentProjectTeam;
            List<Room> listOfRooms = null;

            //Filtering
            switch (filterBy)
            {
                case FilterBy.SelfCreated:
                    Student student = (User.Identity as ProjectFlowIdentity).Student;
                    listOfRooms = roomBLL.GetListOfRoomsBelongingToAProjectTeamAndCreator(projectTeam, student.aspnet_Users, pageIndex, PAGE_SIZE);
                    break;
                case FilterBy.Default:
                    listOfRooms = roomBLL.GetListOfRoomsBelongingToAProjectTeam(projectTeam, pageIndex, PAGE_SIZE);
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
            var room = Rooms[index];

            //Go To Room aspx
            
            Session["DetailRoomID"] = room.roomID;
            Response.Redirect("RoomDetail.aspx");
        }

        protected void roomsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            roomsGridView.PageIndex = e.NewPageIndex;
            RefreshRoomsGridView(FilterBy.Default, roomsGridView.PageIndex);
        }
    }
}