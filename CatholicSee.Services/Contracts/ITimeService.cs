using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Services.Contracts
{
    public interface ITimeService
    {
        string ConvertTimeToString(int hour);
        IEnumerable<string> GetAllHours();
        IEnumerable<DateTime> GetUpcomingDates(DayOfWeek Day, int numberOfDates = 1);
    }
}
