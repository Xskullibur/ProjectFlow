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
    
    public partial class Transcript
    {
        public int transcriptID { get; set; }
        public string transcript1 { get; set; }
        public System.Guid speakerId { get; set; }
        public int roomID { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Room Room { get; set; }
    }
}
