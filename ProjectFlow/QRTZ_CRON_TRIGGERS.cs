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
    
    public partial class QRTZ_CRON_TRIGGERS
    {
        public string SCHED_NAME { get; set; }
        public string TRIGGER_NAME { get; set; }
        public string TRIGGER_GROUP { get; set; }
        public string CRON_EXPRESSION { get; set; }
        public string TIME_ZONE_ID { get; set; }
    
        public virtual QRTZ_TRIGGERS QRTZ_TRIGGERS { get; set; }
    }
}
