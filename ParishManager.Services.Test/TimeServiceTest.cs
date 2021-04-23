using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services.Test
{
    [TestFixture]
    class TimeServiceTest
    {
        private TimeService _service;

        [SetUp]
        public void Setup()
        {
            _service = new TimeService();
        }

        [TestCase(0, "12 AM")]
        [TestCase(1, "1 AM")]
        [TestCase(2, "2 AM")]
        [TestCase(3, "3 AM")]
        [TestCase(4, "4 AM")]
        [TestCase(5, "5 AM")]
        [TestCase(6, "6 AM")]
        [TestCase(7, "7 AM")]
        [TestCase(8, "8 AM")]
        [TestCase(9, "9 AM")]
        [TestCase(10, "10 AM")]
        [TestCase(11, "11 AM")]
        [TestCase(12, "12 PM")]
        [TestCase(13, "1 PM")]
        [TestCase(14, "2 PM")]
        [TestCase(15, "3 PM")]
        [TestCase(16, "4 PM")]
        [TestCase(17, "5 PM")]
        [TestCase(18, "6 PM")]
        [TestCase(19, "7 PM")]
        [TestCase(20, "8 PM")]
        [TestCase(21, "9 PM")]
        [TestCase(22, "10 PM")]
        [TestCase(23, "11 PM")]
        public void ConvertTimeToString_ReturnsCorrectTimeString(int input, string expectedOutput)
        {
            var result = _service.ConvertTimeToString(input);

            Assert.AreEqual(expectedOutput, result);
        }

        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void ConvertTimeToString_ThrowsExceptionIfInvalidInput(int input)
        {
            Assert.Throws<ArgumentException>(delegate { _service.ConvertTimeToString(input); });
        }

        public void GetAllHours_ReturnsAllHoursInOrder()
        {
            var expectedList = new List<string>()
            {
                "12 AM",
                "1 AM",
                "2 AM",
                "3 AM",
                "4 AM",
                "5 AM",
                "6 AM",
                "7 AM",
                "8 AM",
                "9 AM",
                "10 AM",
                "11 AM",
                "12 PM",
                "1 PM",
                "2 PM",
                "3 PM",
                "4 PM",
                "5 PM",
                "6 PM",
                "7 PM",
                "8 PM",
                "9 PM",
                "10 PM",
                "11 PM"
            };

            var result = _service.GetAllHours().ToList();

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(24, result.Count());
            Assert.AreEqual(result[0], expectedList[0]);
            Assert.AreEqual(result[1], expectedList[1]);
            Assert.AreEqual(result[2], expectedList[2]);
            Assert.AreEqual(result[3], expectedList[3]);
            Assert.AreEqual(result[4], expectedList[4]);
            Assert.AreEqual(result[5], expectedList[5]);
            Assert.AreEqual(result[6], expectedList[6]);
            Assert.AreEqual(result[7], expectedList[7]);
            Assert.AreEqual(result[8], expectedList[8]);
            Assert.AreEqual(result[9], expectedList[9]);
            Assert.AreEqual(result[10], expectedList[10]);
            Assert.AreEqual(result[11], expectedList[11]);
            Assert.AreEqual(result[12], expectedList[12]);
            Assert.AreEqual(result[13], expectedList[13]);
            Assert.AreEqual(result[14], expectedList[14]);
            Assert.AreEqual(result[15], expectedList[15]);
            Assert.AreEqual(result[16], expectedList[16]);
            Assert.AreEqual(result[17], expectedList[17]);
            Assert.AreEqual(result[18], expectedList[18]);
            Assert.AreEqual(result[19], expectedList[19]);
            Assert.AreEqual(result[20], expectedList[20]);
            Assert.AreEqual(result[21], expectedList[21]);
            Assert.AreEqual(result[22], expectedList[22]);
            Assert.AreEqual(result[23], expectedList[23]);
        }

        [Test]
        public void GetUpcomingDates_ReturnsOneDateAsDefault()
        {
            var result = _service.GetUpcomingDates(DayOfWeek.Monday);
            Assert.AreEqual(1, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Tuesday);
            Assert.AreEqual(1, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Wednesday);
            Assert.AreEqual(1, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Thursday);
            Assert.AreEqual(1, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Friday);
            Assert.AreEqual(1, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Saturday);
            Assert.AreEqual(1, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Sunday);
            Assert.AreEqual(1, result.Count());
        } 

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(4578)]
        [TestCase(213)]
        public void GetUpcomingDates_ReturnsCorrectNumberOfDates(int numberOfDates)
        {
            var result = _service.GetUpcomingDates(DayOfWeek.Monday, numberOfDates);
            Assert.AreEqual(numberOfDates, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Tuesday, numberOfDates);
            Assert.AreEqual(numberOfDates, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Wednesday, numberOfDates);
            Assert.AreEqual(numberOfDates, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Thursday, numberOfDates);
            Assert.AreEqual(numberOfDates, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Friday, numberOfDates);
            Assert.AreEqual(numberOfDates, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Saturday, numberOfDates);
            Assert.AreEqual(numberOfDates, result.Count());

            result = _service.GetUpcomingDates(DayOfWeek.Sunday, numberOfDates);
            Assert.AreEqual(numberOfDates, result.Count());
        }

        [TestCase(DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Monday)]
        [TestCase(DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Wednesday)]
        [TestCase(DayOfWeek.Thursday)]
        [TestCase(DayOfWeek.Friday)]
        [TestCase(DayOfWeek.Saturday)]
        public void GetUpcomingDates_DatesAllCorrectDay(DayOfWeek dayOfWeek)
        {
            var result = _service.GetUpcomingDates(dayOfWeek, 1000);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.IsFalse(result.Any(x => x.DayOfWeek != dayOfWeek));
        }

        [Test]
        public void GetUpcomingDates_DatesAreOrderedCorrectly()
        {
            var result = _service.GetUpcomingDates(DayOfWeek.Tuesday, 100).ToList();

            var expectedSort = result.OrderBy(x => x.Ticks).ToList();

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedSort[i], result[i]);
            }
        }

        [TestCase(Int32.MinValue)]
        [TestCase(-34534)]
        [TestCase(-1)]
        public void GetUpcomingDates_ThrowsErrorForNegativeUpcomingDates(int numberOfDates)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetUpcomingDates(DayOfWeek.Thursday, numberOfDates));
        }

        [Test]
        public void GetUpcomingDates_DoesNotIncludeDateIfFirstDateIsToday()
        {
            var result = _service.GetUpcomingDates(DayOfWeek.Friday, 1).First();

            Assert.AreNotEqual(DateTime.Now.ToShortDateString(), result.ToShortDateString());
        }
    }
}
