using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using OutsideInTdd.Adapters;
using OutsideInTdd.App;
using static TddXt.AnyRoot.Root;

namespace OutsideInTddBoxSpecification.Integration
{
    public class DataAccessIntegrationSpecification
    {
        [Test]
        public void ShouldAllowRetrievingAddedTodoItem()
        {
            //GIVEN
            var dao = new TodoNoteDao();
            var dto1 = Any.Instance<TodoNoteDto>();
            dao.Save(dto1);

            //WHEN
            var allTodos = dao.LoadAllItems();

            //THEN
            allTodos.Single().Should().BeEquivalentTo(dto1);
        }
    }
}
