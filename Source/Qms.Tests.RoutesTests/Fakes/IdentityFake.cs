namespace Qms.Tests.RoutesTests.Fakes
{
    using System;
    using System.Security.Principal;

    public class IdentityFake : IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string GetUserId()
        {
            return "123";
        }
    }
}
