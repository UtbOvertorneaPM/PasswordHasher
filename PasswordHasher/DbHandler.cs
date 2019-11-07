﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHasher {


    public class DbHandler : IDbHandler {

        private UserContext _userContext;

        public void Login(IInputHandler inputHandler) {

            var server = inputHandler.Prompt("Input server name");
            var database = inputHandler.Prompt("Input the database name");
            var user = inputHandler.Prompt("Input database user name");
            var password = inputHandler.PromptPassword();
            
            _userContext = new UserContext(password, database, server, user);
        }

        public async Task<int> AddUserAsync(IUser user) {
            
            await _userContext.Users.AddAsync((User)user);
            return await _userContext.SaveChangesAsync();
        }

        ~DbHandler() {
            
            _userContext.Dispose();
        }
    }
}
