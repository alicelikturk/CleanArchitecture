﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.CurrentUser
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
    }
}
