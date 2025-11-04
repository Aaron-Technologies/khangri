using System.Security.Claims;

namespace Khangri.UI.Helpers
{
    public class UsernameHelper
    {
        private IHttpContextAccessor _contextAccessor;

        public UsernameHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string FullUserName
        {
            get
            {
                var fullName = String.Empty;
                var user = _contextAccessor?.HttpContext?.User;
                if (user != null)
                {
                    var fName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                    fullName = fName ?? fullName;
                    var lName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                    fullName = fullName + " " + lName ?? String.Empty;
                }


                return fullName;
            }
        }

        public string UserEmail
        {
            get
            {
                var userEmail = String.Empty;
                var user = _contextAccessor?.HttpContext?.User;
                if (user != null)
                {
                   
                     userEmail= user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                }


                return userEmail;
            }
        }

        public string UserType
        {
            get
            {
                var userType = String.Empty;
                var user = _contextAccessor?.HttpContext?.User;
                if (user != null)
                {
                    userType = user.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value ?? String.Empty;

                }
                return userType;
            }
        }

        public string UserId
        {
            get
            {
                var userId = String.Empty;
                var user = _contextAccessor?.HttpContext?.User;
                if (user != null)
                {
                    userId = user.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? String.Empty;

                }
                return userId;
            }
        }
    }
}
