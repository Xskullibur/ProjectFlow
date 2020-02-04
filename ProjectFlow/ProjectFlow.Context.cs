﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectFlow
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectFlowEntities : DbContext
    {
        public ProjectFlowEntities()
            : base("name=ProjectFlowEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<Attendee> Attendees { get; set; }
        public virtual DbSet<CommentForIssue> CommentForIssues { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Milestone> Milestones { get; set; }
        public virtual DbSet<Polling> Pollings { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTeam> ProjectTeams { get; set; }
        public virtual DbSet<QRTZ_BLOB_TRIGGERS> QRTZ_BLOB_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_CALENDARS> QRTZ_CALENDARS { get; set; }
        public virtual DbSet<QRTZ_CRON_TRIGGERS> QRTZ_CRON_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_FIRED_TRIGGERS> QRTZ_FIRED_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_JOB_DETAILS> QRTZ_JOB_DETAILS { get; set; }
        public virtual DbSet<QRTZ_LOCKS> QRTZ_LOCKS { get; set; }
        public virtual DbSet<QRTZ_PAUSED_TRIGGER_GRPS> QRTZ_PAUSED_TRIGGER_GRPS { get; set; }
        public virtual DbSet<QRTZ_SCHEDULER_STATE> QRTZ_SCHEDULER_STATE { get; set; }
        public virtual DbSet<QRTZ_SIMPLE_TRIGGERS> QRTZ_SIMPLE_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_SIMPROP_TRIGGERS> QRTZ_SIMPROP_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_TRIGGERS> QRTZ_TRIGGERS { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomActionItem> RoomActionItems { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskAllocation> TaskAllocations { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Transcript> Transcripts { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<VoiceRecording> VoiceRecordings { get; set; }
    }
}
