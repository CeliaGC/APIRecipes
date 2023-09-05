using API.Attributes;
using API.IServices;
using Security.IServices;

namespace API.Middlewares
{
    public class RequestAuthorizationMiddleware
    {
        private readonly IUserSecurityService _userSecurityService;
        public RequestAuthorizationMiddleware(IUserSecurityService userSecurityService)
        {
            _userSecurityService = userSecurityService;
        }

        public void ValidateRequestAutorizathion(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                return;
            }
            EndpointAuthorizeAttribute authorization = new EndpointAuthorizeAttribute(context);

            if (authorization.Values.AllowsAnonymous)
            {
                return;
            }

            _userSecurityService.ValidateUserToken(context.Request.Headers.Authorization.ToString(),
                                                 authorization.Values.AllowedUserRols);
        }
    }
}

//using API.Attributes;
//using API.IServices;
//using Security.IServices;
//namespace API.Middlewares
//{
//    public class RequestAuthorizationMiddleware
//    {
//        private readonly IUserSecurityService _userSecurityService;
//        public RequestAuthorizationMiddleware(IUserSecurityService userSecurityService)
//        {
//            _userSecurityService = userSecurityService;
//        }
//        public void ValidateRequestAutorizathion(HttpContext context)
//        {
//            //AGREGADO ESTO
//            if (context.Request.Method == "OPTIONS")
//            {
//                return;
//            }
//            //HASTA ACÁ
//            EndpointAuthorizeAttribute authorization = new EndpointAuthorizeAttribute(context);
//            if (authorization.Values.AllowsAnonymous)
//            {
//                return;
//            }
//            _userSecurityService.ValidateUserToken(context.Request.Headers.Authorization.ToString(),
//                                                 authorization.Values.AllowedUserRols);
//        }
//    }
//}












