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

        [Test]
        public void Delete_CallsNecessaryCalls()
        {
            var parish = new Parish();

            _service.Delete(parish);

            A.CallTo(() => _unitOfWork.Parishes.Remove(parish)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Delete_GetsAndRemovesParish()
        {
            var parishId = 100;

            var parish = new Parish()
            {
                Id = parishId
            };

            A.CallTo(() => _unitOfWork.Parishes.Get(parishId)).Returns(parish);

            _service.Delete(parishId);

            A.CallTo(() => _unitOfWork.Parishes.Remove(parish)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Get_CallsToRetrieveParishWithCorrectId()
        {
            var parishId = 4;

            _service.Get(parishId);

            A.CallTo(() => _unitOfWork.Parishes.Get(parishId)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Update_CreatesParishIfDoesNotExist()
        {
            var name = "Test Parish";
            var returnedParish = new Parish()
            {
                Id = 1,
                ParishName = name
            };

            A.CallTo(() => _unitOfWork.Parishes.Get(A<int>.Ignored)).Returns(null);
            A.CallTo(() => _unitOfWork.Parishes.Add(A<Parish>.Ignored)).Returns(returnedParish);

            var result = _service.Update(new Parish() { ParishName = name });

            A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
            Assert.AreEqual(name, result.ParishName);
            Assert.AreEqual(returnedParish.Id, result.Id);
        }

        [Test]
        public void Update_UpdatesExistingParish()
        {
            var name = "Test Parish";
            var expectedName = "I want this name";
            var returnedParish = new Parish()
            {
                Id = 1,
                ParishName = name
            };

            A.CallTo(() => _unitOfWork.Parishes.Get(A<int>.Ignored)).Returns(returnedParish);

            var result = _service.Update(new Parish() { ParishName = expectedName });

            A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Parishes.Add(A<Parish>.Ignored)).MustNotHaveHappened();
            Assert.AreEqual(expectedName, result.ParishName);
            Assert.AreEqual(returnedParish.Id, result.Id);
        }
    }
}