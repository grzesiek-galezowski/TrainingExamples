// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using BotLogic;
using BotLogic.StateValues;
using Microsoft.Bot.Builder;

namespace BotBuilderEchoBotV4
{
  public class BotAccessors
  {
    public BotAccessors(ConversationState conversationState)
    {
      ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
    }

    public static string StatesName { get; } = $"{nameof(BotAccessors)}.State";

    public IStatePropertyAccessor<States> CurrentState { get; set; }

    public ConversationState ConversationState { get; }
  }
}
