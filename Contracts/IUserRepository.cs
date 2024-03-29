﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {

        Task<User> GetUserDetails(bool trackChanges, string userId);
        public void UpdateUserDetails(User user);

    }
}
