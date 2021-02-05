using FluentAssertions;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using OutsideInTdd.App;
using TddXt.AnyRoot;
using static TddXt.AnyRoot.Root;

namespace OutsideInTddSpecification.App
{
    public class AddNoteCommandSpecification
    {
        [Test]
        public void ShouldSaveTodoNoteWhenExecuted()
        {
            //GIVEN
            var request = Substitute.For<INewNoteRequest>();
            var storage = Any.Instance<ITodoNoteDao>();
            var addNoteCommand = 
                new AddNoteCommand(
                    request, 
                    storage, 
                    Any.Instance<IAddTodoResponse>());
            
            //WHEN
            addNoteCommand.Execute();
        
            //THEN
            request.Received(1).CreateNoteIn(storage);
        }
        
        [Test]
        public void ShouldReportFailureWhenInappropriateWordDetected()
        {
            //GIVEN
            var request = Substitute.For<INewNoteRequest>();
            var storage = Any.Instance<ITodoNoteDao>();
            var exception = Any.Instance<InappropriateWordException>();
            var response = Substitute.For<IAddTodoResponse>();
            var addNoteCommand = 
                new AddNoteCommand(
                    request, 
                    storage, 
                    response);

            request.When(m => m.AssertContainsOnlyAppropriateWords())
                .Throw(exception);

            //WHEN
            addNoteCommand.Execute();
        
            //THEN
            response.Received(1).ReportFailureBecauseOfInappropriateWord(exception.Word);
            request.DidNotReceive().CreateNoteIn(Arg.Any<ITodoNoteDao>());
        }
    }
}