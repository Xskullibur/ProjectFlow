//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.Issues = new HashSet<Issue>();
            this.TaskAllocations = new HashSet<TaskAllocation>();
        }
    
        public int taskID { get; set; }
        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int teamID { get; set; }
        public int milestoneID { get; set; }
        public int statusID { get; set; }
        public int priorityID { get; set; }
        public bool dropped { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual Milestone Milestone { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual ProjectTeam ProjectTeam { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskAllocation> TaskAllocations { get; set; }
    }
}
