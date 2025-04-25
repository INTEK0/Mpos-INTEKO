using System;
using fLicence.Helpers.Services;

namespace fLicence.Helpers.Implementations
{
    public class LocalDateTimeService : IDateTimeService
    {
        public DateTime Current => DateTime.UtcNow.AddHours(4);
        public string CurrentString => DateTime.UtcNow.AddHours(4).ToString("dd.MM.yyyy");
    }
}
