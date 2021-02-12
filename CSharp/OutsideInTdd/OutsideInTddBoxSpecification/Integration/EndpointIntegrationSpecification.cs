using System.Threading.Tasks;
using NUnit.Framework;

namespace OutsideInTddBoxSpecification.Integration
{
    //TODO IDisposable

    public class EndpointIntegrationSpecification
    {
        [Test]
        public async Task ShouldReturnOkWhenAddingTodoNoteFinishesWithoutException()
        {
            //GIVEN
            using var driver = new EndpointIntegrationDriver();
            await driver.InitializeAsync();
            driver.SetupSuccessfulAddTodoCommand();

            //WHEN
            await driver.InvokeAddTodoEndpoint();

            //THEN
            driver.ResponseShouldBeOk();
        }

        [Test]
        public async Task ShouldReturnBadRequestErrorWithMessageWhenAddingTodoNoteFinishesWithError()
        {
            //GIVEN
            using var driver = new EndpointIntegrationDriver();
            await driver.InitializeAsync();
            driver.SetupAddTodoCommandFailedBecauseOfInappropriateWord();

            //WHEN
            await driver.InvokeAddTodoEndpoint();

            //THEN
            driver.ResponseShouldBeInternalServerErrorWithBadWordMessage();
        }
    }
}
