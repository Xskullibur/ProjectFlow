using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace ProjectFlow.Login
{
    public class ProjectFlowIdentity : ClaimsIdentity
    {
        public Student Student { get; private set; }

        public ProjectFlowIdentity(Student student, ClaimsIdentity identity): this(identity) {
            this.Student = student;
        }

        public ProjectFlowIdentity(ClaimsIdentity identity) : base(identity) {}


    }
}