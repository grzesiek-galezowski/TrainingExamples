using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class ServiceLogicRoot
    {
        private readonly ITodoNoteDao _todoNoteDao;
        private readonly AppLogicRoot _appLogicRoot;
        public EndpointsRoot Endpoints { get; }

        public ServiceLogicRoot()
        {
            _todoNoteDao = new TodoNoteDao();
            _appLogicRoot = new AppLogicRoot(_todoNoteDao);
            Endpoints = new EndpointsRoot(_appLogicRoot.CommandFactory, _todoNoteDao);
        }
    }
}