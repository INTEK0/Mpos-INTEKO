using System;

namespace Licence.Helpers.Services
{
    public interface IGuidKeyService
    {
        Guid Current { get; }
        string CurrentString { get; }
}
}
