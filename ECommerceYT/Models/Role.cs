using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class Role
    {
        public Int32 RoleId { get; set; }
        public string RoleName { get; set; }

        public Role()
        {
            
        }

        public Role(int roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}