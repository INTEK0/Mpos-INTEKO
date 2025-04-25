using System;

namespace fLicence.Helpers.Services
{
    public interface IDateTimeService
    {
        DateTime Current { get; }
        string CurrentString {  get; }
    }
}
