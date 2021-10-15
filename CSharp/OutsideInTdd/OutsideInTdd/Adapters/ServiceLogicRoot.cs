using System;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class ServiceLogicRoot : IDisposable
    {
        private readonly TodoNoteDao _todoNoteDao;
        private readonly AppLogicRoot _appLogicRoot;
        public EndpointsRoot Endpoints { get; }

        public ServiceLogicRoot()
        {
            _todoNoteDao = new TodoNoteDao();
            _appLogicRoot = new AppLogicRoot(_todoNoteDao);
            Endpoints = new EndpointsRoot(_appLogicRoot.CommandFactory, _todoNoteDao);
        }

        public void Dispose()
        {
            _todoNoteDao.Dispose();
        }
    }
}