using System;
using Licence.Helpers.Services;

namespace Licence.Helpers.Implementations
{
    internal class GuidGeneratorService : IGuidKeyService
    {
        public Guid Current => Guid.NewGuid();

        public string CurrentString => Guid.NewGuid().ToString();
    }
}