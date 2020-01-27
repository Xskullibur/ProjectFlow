﻿using System;
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
    public partial class RoomDetail : System.Web.UI.Page
    {

        public List<ActionItem> ActionItems { get => ViewState["ActionItems"] as List<ActionItem>; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int roomID = (int) Session["DetailRoomID"];
                var roomBLL = new RoomBLL();
                var room = roomBLL.GetRoomByRoomID(roomID);

                var actionItemBLL = new ActionItemBLL();
                //Get list of ActionItems from Database
                var listOfActionItems = actionItemBLL.GetListOfRoomActionItemsFromRoom(room);

                //Set the ActionItems in ViewState
                ViewState["ActionItems"] = listOfActionItems.Select(roomActionItem => new ActionItem
                {
                    ActionItemID = roomActionItem.actionItemID,
                    PersonName = roomActionItem.personName,
                    Type = roomActionItem.type,
                    Topic = roomActionItem.topic
                }).ToList();

                //Add list into table
                foreach (var item in ActionItems)
                {
                    AddNewActionItem(item);
                }

                DisplayRoomInfo(room);
                var transcriptBLL = new TranscriptBLL();
                DisplayTranscript(transcriptBLL.GetListOfTranscripts(room));


            }


        }

        /// <summary>
        /// Display room informations
        /// </summary>
        private void DisplayRoomInfo(Room room)
        {
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


        private void DisplayTranscript(List<Transcript> transcripts)
        {
            transcriptTxtBox.Text = "";

            foreach (var transcript in transcripts)
            {
                transcriptTxtBox.Text += transcript.aspnet_Users.UserName + ":" + transcript.transcript1 + "<br/>";
            }

        }

        private void AddNewActionItem(ActionItem actionItem)
        {

            materialTable.AddRow(new string[] {
                            actionItem.Type,
                            actionItem.PersonName,
                            actionItem.Topic
                        });
        }

    }
}