using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.TechChallenge.Entities;
using Xunit;

namespace WooliesX.TechChallenge.BusinessLogic.Tests
{

    public class UserManagerTests
    {
        [Fact]
        public void GetUser()
        {            
            Environment.SetEnvironmentVariable("AuthorName", "FirstName Lastname");
            string expectedUsername = Environment.GetEnvironmentVariable("AuthorName");
                    
            Environment.SetEnvironmentVariable("ResourceToken", "062ef2b2-0e19-4730-8d56-28sdf6sdfs");
            string expectedUserToken = Environment.GetEnvironmentVariable("ResourceToken");

            UserManager umgr = new UserManager();
            var result = umgr.GetUser();
            Assert.Equal(expectedUsername, result.Name);
            Assert.Equal(expectedUserToken, result.Token);

        }
    }
}
