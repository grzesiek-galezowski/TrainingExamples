﻿namespace DotNetJunkieKata
{
  public interface ICommandHandler<TCommand>
  {
    void Handle(TCommand command);
  }
}