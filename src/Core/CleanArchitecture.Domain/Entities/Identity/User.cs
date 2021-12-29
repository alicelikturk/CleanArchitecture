using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>, IEntity
    {
        private Action<object, string> LazyLoader { get; set; }

        public User(Action<object, string> lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public User()
        {
            
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [IgnoreDataMember]
        public string FullName { get => $"{FirstName} {LastName}"; }

        //// [JsonIgnore]
        //public List<RefreshToken> RefreshTokens { get; set; }
        
    }
}
