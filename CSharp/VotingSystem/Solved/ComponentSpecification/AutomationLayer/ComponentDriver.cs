using ApplicationLogic.Ports;
using Bootstrap.CompositionRoot;
using FluentAssertions;
using Lib;
using NSubstitute;
using TddXt.XNSubstitute.Root;

namespace ComponentSpecification.AutomationLayer
{
  public class ComponentDriver
  {
    private IUsersRepository _repository;
    private IResultBuilder _resultBuilder;
    private ApplicationLogicRoot _root;

    public void Start()
    {
      _repository = new InMemoryUsersRepository();
      _resultBuilder = Substitute.For<IResultBuilder>();
      _root = new ApplicationLogicRoot(_repository);
    }

    public void Create(UserDtoBuilder johnny)
    {
      _root.CommandFactory()
        .CreateRegisterUserCommand(johnny.Build(), _resultBuilder)
        .Execute();
    }

    public void ShouldReportSuccessfulCreationOf(UserDtoBuilder johnny)
    {
      _resultBuilder.Received(1).UserAddedSuccessfully(Arg<UserDto>.That(dto => dto.Should().BeEquivalentTo(johnny.Build())));
    }

    public QueriedTestUser Get(UserDtoBuilder user)
    {
      return new QueriedTestUser(
        _root.CommandFactory().CreateGetUserByIdQuery(user.Build().Login)
          .Execute());
    }
  }
}