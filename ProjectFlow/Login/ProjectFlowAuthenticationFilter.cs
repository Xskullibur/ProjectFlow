using ProjectFlow.DAO;
using ProjectFlow.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace ProjectFlow.Login
{

    public class ProjectFlowAuthenticationFilter : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => true;

        public async System.Threading.Tasks.Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            //Get the http request
            HttpRequestMessage request = context.Request;
            //Get the http request authentication parameters 
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            //Check the authentication scheme
            if (authorization.Scheme != "Basic-ProjectFlow" && authorization.Scheme != "Basic") return;
            //Check if there is any credentials if not set error
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }


            (string, string)? usernameAndPassword = ExtractUserNameAndPassword(authorization.Parameter);
            if (usernameAndPassword == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
            }

            string username = usernameAndPassword.Value.Item1;
            string password = usernameAndPassword.Value.Item2;

            IPrincipal principal = await AuthenticateAsync(username, password, cancellationToken);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
            }

            //If the credentials are valid, set principal.
            else
            {
                context.Principal = principal;
            }

        }

        private async Task<ProjectFlowUser> AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
        {
            StudentDAO dao = new StudentDAO();
            Student student = dao.LoginValidate(username, password);
            if (student == null) return null;
            else
            {
                return new ProjectFlowUser(student);
            }
        }

        private (string, string)? ExtractUserNameAndPassword(string parameter)
        {
            string[] usernameAndPassword = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(parameter)).Split(':');

            // Must only contains two paramaters

            if (usernameAndPassword.Length != 2) return null;


            string username = usernameAndPassword[0];
            string password = usernameAndPassword[1];
            return (username, password);
        }

        public System.Threading.Tasks.Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Basic-ProjectFlow");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return System.Threading.Tasks.Task.FromResult(0);
        }
    }

    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }
    }
    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
        {
            Challenge = challenge;
            InnerResult = innerResult;
        }

        public AuthenticationHeaderValue Challenge { get; private set; }

        public IHttpActionResult InnerResult { get; private set; }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Only add one challenge per authentication scheme.
                if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(Challenge);
                }
            }

            return response;
        }
    }


}