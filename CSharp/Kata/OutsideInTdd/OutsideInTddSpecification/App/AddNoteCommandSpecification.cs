using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using OutsideInTdd.App;
using static TddXt.AnyRoot.Root;

namespace OutsideInTddSpecification.App
{
    public class AddNoteCommandSpecification
    {
        [Test]
        public async Task ShouldSaveTodoNoteWhenExecuted()
        {
            //GIVEN
            var request = Substitute.For<INewNoteRequest>();
            var storage = Any.Instance<ITodoNoteDao>();
            var addNoteCommand = 
                new AddNoteCommand(
                    request, 
                    storage, 
                    Any.Instance<IAddTodoResponseInProgress>());
            
            //WHEN
            await addNoteCommand.Execute();
        
            //THEN
            request.Received(1).CreateNoteIn(storage);
        }
        
        [Test]
        public async Task ShouldReportFailureWhenInappropriateWordDetected()
        {
            //GIVEN
            var request = Substitute.For<INewNoteRequest>();
            var storage = Any.Instance<ITodoNoteDao>();
            var exception = Any.Instance<InappropriateWordException>();
            var response = Substitute.For<IAddTodoResponseInProgress>();
            var addNoteCommand = 
                new AddNoteCommand(
                    request, 
                    storage, 
                    response);

            request.When(m => m.AssertContainsOnlyAppropriateWords())
                .Throw(exception);

            //WHEN
            await addNoteCommand.Execute();
        
            //THEN
            await response.Received(1).ReportFailureBecauseOfInappropriateWord(exception.Word);
            request.DidNotReceive().CreateNoteIn(Arg.Any<ITodoNoteDao>());
        }
    }
}