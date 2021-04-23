using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services
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

        public IEnumerable<DateTime> GetUpcomingDates(DayOfWeek Day, int numberOfDates = 1)
        {
            var list = new List<DateTime>();
            return list;
        }
    }
}
