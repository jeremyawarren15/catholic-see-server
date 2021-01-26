using FakeItEasy;
using NUnit.Framework;
using ParishManager.Core.Entities;
using ParishManager.Data;
using ParishManager.Data.Contracts;
using ParishManager.Services.Contracts;

namespace ParishManager.Services.Test
{
    public class ParishServiceTest
    {
        private IParishService _service;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _service = new ParishService(_unitOfWork);
        }

        [Test]
        public void Create_ReturnsCreatedUser()
        {
            var name = "Test Parish";
            var returnedParish = new Parish()
            {
                Id = 1,
                ParishName = name
            };

            A.CallTo(() => _unitOfWork.Parishes.Add(A<Parish>.Ignored)).Returns(returnedParish);

            var result = _service.Create(new Parish() { ParishName = name });

            A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
            Assert.AreEqual(name, result.ParishName);
            Assert.AreEqual(returnedParish.Id, result.Id);
        }
    }
}