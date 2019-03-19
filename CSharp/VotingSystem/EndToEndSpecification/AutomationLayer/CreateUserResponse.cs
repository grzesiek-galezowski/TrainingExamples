﻿using System;
using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;
using FluentAssertions.Execution;
using Lib;

namespace EndToEndSpecification.AutomationLayer
{
  public class CreateUserResponse
  {
    private const string UsersApiRelativeUriRoot = "api/users/";
    private readonly string _content;
    private readonly HttpStatusCode _statusCode;
    private readonly HttpResponseHeaders _headers;

    public CreateUserResponse(string content, HttpStatusCode statusCode, HttpResponseHeaders headers)
    {
      _content = content;
      _statusCode = statusCode;
      _headers = headers;
    }

    public Uri Location => _headers.Location;

    private CreateUserResponse ShouldBe201Created()
    {
      _statusCode.Should().Be(HttpStatusCode.Created);
      return this;
    }

    private CreateUserResponse ShouldHaveLocationStartingWith(string s)
    {
      _headers.Location.ToString().Should().StartWith(s);
      return this;
    }


    public CreateUserResponse ShouldIndicateSuccessfulCreationOf(UserDtoBuilder johnny)
    {
      using (new AssertionScope())
      {
        return ShouldBe201Created()
          .ShouldHaveLocationStartingWith(UsersApiRelativeUriRoot)
          .ShouldHaveBodyEqualTo(johnny.Build().Login)
          .ShouldHaveLocationWith(_content);
      }
    }

    private CreateUserResponse ShouldHaveLocationWith(string content)
    {
      _headers.Location.Should().Be("api/users/" + content);
      return this;
    }

    private CreateUserResponse ShouldHaveBodyEqualTo(string id)
    {
      _content.Should().Be(id);
      return this;
    }
  }
}