using System;
using WooliesX.TechChallenge.Entities;

namespace WooliesX.TechChallenge.BusinessLogic
{
    public class UserManager :IUserManager
    {
        public User GetUser()
        {
            return new User
            {
                Name = Environment.GetEnvironmentVariable("AuthorName"),
                Token = Environment.GetEnvironmentVariable("ResourceToken")
            };
        }
    }
}
