using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NSubstitute;

namespace MSTest_UnitTesting_TestProject
{
    public interface IAuditService
    {
        void AddNewUser(User user, string message);
    }

    public class User
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public interface IUserService
    {
        void AddUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly IAuditService _auditService;

        public UserService(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public void AddUser(User user)
        {
            var userInfo = new User()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            _auditService.AddNewUser(user, $"Added user ");
        }
    }


    [TestClass]
        public class NSubstituteTests
        {
            [TestMethod]
            public void AddUser_Should_Call_Audit_Service_With_Added_User()
            {
                //Arrange
                var auditService = Substitute.For<IAuditService>();

                
                var userNameToValidate = "raja";
                var auditMessageToValidate = $"Added user ";

                var userInfo = new User()
                {
                    UserName = userNameToValidate,
                    FirstName = "Raja",
                    LastName = "Vignesh"
                };

                var sut = new UserService(auditService);

                //Act
                sut.AddUser(userInfo);

                //Assert: Testing username and Message
                auditService.Received().AddNewUser(Arg.Is<User>(u => u.UserName == userNameToValidate), auditMessageToValidate);
            }
        }
    }

