using Microsoft.AspNetCore.Http;
using CleanArchitecture.Application.Common.Interfaces.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanArchitecture.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId
        {
            get
            {
                Guid id;
                Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"), out id);
                return id;
            }
        }
    }
}
