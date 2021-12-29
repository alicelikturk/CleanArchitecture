using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Dto
{
    public class RoleView
    {
        public RoleView()
        {
            this.PermissionStoreViews = new List<PermissionStoreView>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public List<PermissionStoreView> PermissionStoreViews { get; set; }

        ////create 


        ////edit


        ////view
        //public Guid? UserId { get; set; }
        //public string? UserName { get; set; }
        //public Guid? RoleId { get; set; }
        //public string? RoleName { get; set; }
        //public bool IsSelected { get; set; }
    }
}
