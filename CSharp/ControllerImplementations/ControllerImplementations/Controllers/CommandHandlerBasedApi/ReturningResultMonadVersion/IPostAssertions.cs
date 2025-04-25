﻿using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;

public interface IPostAssertions
{
  void AssertContentIsOfRequiredLength(AddPostCommand command);
  void AssertContentContainsNoInappropriateWords(AddPostCommand command);
}