using System;
using System.Collections.Generic;

namespace AvoidAutoMockingContainers
{
    public class RestartSequences
    {
        private readonly List<IService> _localServices;
        private readonly List<IService> _remoteServices;
        private readonly TimeSpan _timeout;

        public RestartSequences(
            List<IService> localServices, 
            List<IService> remoteServices, 
            TimeSpan timeout)
        {
            _localServices = localServices;
            _remoteServices = remoteServices;
            _timeout = timeout;
        }

        public void RestartAll()
        {
            for (int i = 0; i < _localServices.Count; ++i)
            {
                _localServices[i].Restart(_timeout);
                _remoteServices[i].Restart(_timeout);
            }
        }
    }
}