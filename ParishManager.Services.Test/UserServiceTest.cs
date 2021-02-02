using FakeItEasy;
using NUnit.Framework;
using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Test
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService service;
        private IUnitOfWork unitOfWork;

        [SetUp]
        public void Setup()
        {
            unitOfWork = A.Fake<IUnitOfWork>();

            service = new UserService(unitOfWork);
        }

        [Test]
        public void IsAdminForParish_UserNotAssociatedToParish_ReturnsFalse()
        {
            A.CallTo(() => unitOfWork.UserParishAssociations.Get(A<string>.Ignored, A<int>.Ignored))
                .Returns(null);

            var result = service.IsAdminForParish("dummy", 23423);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsAdminForParish_UserNotAdminOfParish_ReturnsFalse()
        {
            var association = new UserParishAssociation()
            {
                IsAdmin = false
            };

            A.CallTo(() => unitOfWork.UserParishAssociations.Get(A<string>.Ignored, A<int>.Ignored))
                .Returns(association);

            var result = service.IsAdminForParish("dummy", 23423);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsAdminForParish_UserIsAdmin_ReturnsTrue()
        {
            var association = new UserParishAssociation()
            {
                IsAdmin = true
            };

            A.CallTo(() => unitOfWork.UserParishAssociations.Get(A<string>.Ignored, A<int>.Ignored))
                .Returns(association);

            var result = service.IsAdminForParish("dummy", 23423);

            Assert.True(result);
        }
    }
}
