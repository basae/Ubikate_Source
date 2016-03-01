using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Ubikate.Core
{
    public class User : IIdentity
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public User(long id, string name, string authenticationType, bool isAuthenticated)
        {
            Name = name;
            Id = id;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
        }
    }
}