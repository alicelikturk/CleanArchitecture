using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Dto
{
    public class PermissionStoreView
    {
        public Guid Id{ get; set; }
        public Guid ParentClaimId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
