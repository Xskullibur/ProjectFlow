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

        public override bool IsAuthenticated => IsStudent || IsTutor;

        public bool IsStudent { get => Student != null; }
        public bool IsTutor { get => Tutor != null; }

        public Student Student { get; private set; }
        public Tutor Tutor { get; private set; }

        public ProjectFlowIdentity(Student student, ClaimsIdentity identity) : this(identity) {
            this.Student = student;
        }

        public ProjectFlowIdentity(Tutor tutor, ClaimsIdentity identity) : this(identity)
        {
            this.Tutor = tutor;
        }

        public ProjectFlowIdentity(ClaimsIdentity identity) : base(identity) {}


    }
}