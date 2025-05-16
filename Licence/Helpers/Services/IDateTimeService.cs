using System;

namespace Licence.Helpers.Services
{
    public interface IDateTimeService
    {
        DateTime Current { get; }
        string CurrentString {  get; }
    }
}
