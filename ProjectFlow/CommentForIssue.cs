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
    
    public partial class CommentForIssue
    {
        public int commentID { get; set; }
        public string comment { get; set; }
        public int createdBy { get; set; }
        public int issueID { get; set; }
    
        public virtual Issue Issue { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}
