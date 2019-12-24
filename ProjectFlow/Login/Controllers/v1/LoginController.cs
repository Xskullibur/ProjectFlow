using ProjectFlow.DAO;
using ProjectFlow.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectFlow.v1
{
    
    [ProjectFlowAuthorizationFilter(Roles = "Student")]
    public class LoginController : ProjectFlowAPIController
    {
        [ProjectFlowAuthenticationFilter]
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Login()
        {
            return Success("Login Successfully!");
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Test(int id)
        {
            return Ok("ASD");
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}