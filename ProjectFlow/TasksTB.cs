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
    
    public partial class TasksTB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TasksTB()
        {
            this.TaskAllocationTBs = new HashSet<TaskAllocationTB>();
        }
    
        public int taskID { get; set; }
        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int teamID { get; set; }
    
        public virtual ProjectTeamsTB ProjectTeamsTB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskAllocationTB> TaskAllocationTBs { get; set; }
    }
}
