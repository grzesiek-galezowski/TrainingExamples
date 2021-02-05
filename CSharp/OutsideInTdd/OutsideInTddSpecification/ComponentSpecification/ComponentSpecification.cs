using NSubstitute;
using NUnit.Framework;
using OutsideInTdd.App;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace OutsideInTddSpecification.ComponentSpecification
{
    public class ComponentSpecification
    {
        [Test]
        public void ShouldReportErrorWhenInappropriateWordDetected()
        {
            //GIVEN
            var response = Substitute.For<IAddTodoResponse>();
            var appLogicRoot = new AppLogicRoot(Any.Instance<ITodoNoteDao>());
            var inappropriateWord = "Banan";
            var dto = new TodoNoteDto(
                Any.String(), 
                Any.StringContaining(inappropriateWord));
        
            //WHEN
            appLogicRoot.CommandFactory.CreateAddNoteCommand(dto, response).Execute();
        
            //THEN
            response.Received(1)
                .ReportFailureBecauseOfInappropriateWord(inappropriateWord);
        }
    }
}
