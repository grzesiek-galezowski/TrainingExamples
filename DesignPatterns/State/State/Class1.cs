namespace State
{
    public class Class1
    {
      //states are stateless - all fields read only
      //state machine should be protected from threaded access
      //state machine should be thin - delegate all functionality
      //if multithreaded, SM should be tell don't ask
      //different ways of passing context
      // - through setter
      // - through getInitialState(this) and then each state passes context to another's factory method
      // - through parameters
      //two interfaces - signals and context
    }
}
