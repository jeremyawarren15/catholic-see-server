using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Services
{
    public class TimeService : ITimeService
    {
        public string ConvertTimeToString(int hour)
        {
            if (hour > 24 || hour < 0)
            {
                throw new ArgumentException("Invalid hour value provided");
            }

            if (hour < 12)
            {
                if (hour == 0)
                {
                    return "12 AM";
                }

                return $"{hour} AM";
            }

            if (hour == 12)
            {
                return "12 PM";
            }

            return $"{hour - 12} PM";
        }

        public IEnumerable<string> GetAllHours()
        {
            var hours = new List<string>();

            for (var i = 0; i < 24; i++)
            {
                hours.Add(ConvertTimeToString(i));
            }

            return hours;
        }

        public IEnumerable<DateTime> GetUpcomingDates(DayOfWeek day, int numberOfDates = 1)
        {
            if (numberOfDates <= 0)
            {
                throw new ArgumentOutOfRangeException("GetUpcomingDates() requires numberOfDates to be greater than 0.");
            }

            var list = new List<DateTime>();
            var currentDate = GetNextOccurringDate(day);

            for (int i = 0; i < numberOfDates; i++)
            {
                list.Add(currentDate.AddDays(7));
                currentDate = currentDate.AddDays(7);
            }

            return list;
        }

        public DateTime GetNextOccurringDate(DayOfWeek day)
        {
            var numberOfDaysUntilNextDay = (((int)day - (int) DateTime.Today.DayOfWeek + 7) % 7);
            if (numberOfDaysUntilNextDay == 0)
            {
                numberOfDaysUntilNextDay = 7;
            }

            return DateTime.Now.AddDays(numberOfDaysUntilNextDay);
        }
    }
}
