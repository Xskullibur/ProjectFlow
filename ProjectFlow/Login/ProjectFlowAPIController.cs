using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProjectFlow.Login
{
    public abstract class ProjectFlowAPIController : ApiController 
    {

        /// <summary>
        /// Response Success with json format
        /// {
        ///  "status": "success",
        ///  "content": <content> <- object pass to the <param name="content"/> parameter
        /// }
        /// </summary>
        /// <param name="content">Object which will be shown in the json response and formated to json</param>
        /// <returns></returns>
        [NonAction]
        public IHttpActionResult Success(object content)
        {
            return Ok(new ProjectFlowResponse
            {
                status = "success",
                content = content
            });
        }

        public delegate IHttpActionResult IHttpActionResultMethod(string msg);

        /// <summary>
        /// Response Error with json format
        /// {
        ///  "status": "error",
        ///  "error_message": <param name="errorMsg">
        ///  "content": <content> <- object pass to the <param name="content"/> parameter
        /// }
        /// </summary>
        /// <param name="errorMsg">error message which will be shown in the json response</param>
        /// <param name="content">Object which will be shown in the json response and formated to json</param>
        /// <param name="httpActionResultMethod">Api Controller method which implements IHttpActionResult</param>
        /// <returns></returns>
        [NonAction]
        public IHttpActionResult Error(string errorMsg, object content, IHttpActionResultMethod httpActionResultMethod)
        {
            var responseError = new ProjectFlowResponseError
            {
                status = "error",
                error_messages = errorMsg,
                content = content
            };
            string reponseErrorJson = JsonConvert.SerializeObject(responseError);

            return httpActionResultMethod(reponseErrorJson);
        }

        public IHttpActionResult Error(string errorMsg, object content)
        {
            return Error(errorMsg, content, BadRequest);
        }

    }

    public class ProjectFlowResponse 
    {
        public string status { get; set; }
        public object content { get; set; }

    }

    public class ProjectFlowResponseError : ProjectFlowResponse
    {

        public string error_messages { get; set; }


    }


}