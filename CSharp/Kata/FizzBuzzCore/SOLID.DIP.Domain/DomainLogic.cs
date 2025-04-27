using SOLID.DIP.Persistence;

namespace SOLID.DIP.Domain
{
    public class DomainLogic
    {
        private readonly Database _database;

        public DomainLogic()
        {
            _database = new Database();
        }

        public void PerformCalculation(int a, int b)
        {
            _database.Save(a + b);
        }
    }
}
