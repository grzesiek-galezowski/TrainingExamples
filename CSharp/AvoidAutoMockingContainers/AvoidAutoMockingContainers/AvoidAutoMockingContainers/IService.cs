using System;

namespace AvoidAutoMockingContainers
{
    public interface IService
    {
        void Restart(TimeSpan timeout);
    }
}