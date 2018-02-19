package Commands;

import other.Log;

public class ExceptionLoggedCommand implements Command {
    private final Log _log;
    private final Command _wrappedCommand;

    public ExceptionLoggedCommand(Log log, Command wrappedCommand) {
        _log = log;
        _wrappedCommand = wrappedCommand;
    }

    public void Invoke() {
        try {
            _wrappedCommand.Invoke();
        } catch (Exception e) {
            _log.Error(e);
        }
    }
}
