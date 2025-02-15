﻿using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebaseApp
{
    public class UserRepository
    {
        string webAPIKey = "AIzaSyCLVByGextYo82VE3mOa2K9tjGSjgLnxPY";
        FirebaseAuthProvider authProvider;
        
        public UserRepository()
        {
           authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        public async Task<bool> Register(string email,string password)
        {
            
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }

        public async Task<string>SignIn(string email,string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);            
            if(!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }

        public async Task<bool>ResetPassword(string email)
        {
          await authProvider.SendPasswordResetEmailAsync(email);
            return true;
        }

        public async Task<string>ChangePassword(string token,string password)
        {
           var auth= await authProvider.ChangeUserPassword(token, password);
            return auth.FirebaseToken;
        }
    }
}
