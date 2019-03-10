// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using BotBuilderEchoBotV4.Logic;
using Microsoft.Bot.Builder;

namespace BotBuilderEchoBotV4
{
  /// <summary>
  /// This class is created as a Singleton and passed into the IBot-derived constructor.
  ///  - See <see cref="EchoWithCounterBot"/> constructor for how that is injected.
  ///  - See the Startup.cs file for more details on creating the Singleton that gets
  ///    injected into the constructor.
  /// </summary>
  public class EchoBotAccessors
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="EchoBotAccessors"/> class.
    /// Contains the <see cref="ConversationState"/> and associated <see cref="IStatePropertyAccessor{T}"/>.
    /// </summary>
    /// <param name="conversationState">The state object that stores the counter.</param>
    public EchoBotAccessors(ConversationState conversationState)
    {
      ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
    }

    /// <summary>
    /// Gets the <see cref="IStatePropertyAccessor{T}"/> name used for the <see cref="CurrentState"/> accessor.
    /// </summary>
    /// <remarks>Accessors require a unique name.</remarks>
    /// <value>The accessor name for the counter accessor.</value>
    public static string StatesName { get; } = $"{nameof(EchoBotAccessors)}.State";

    /// <summary>
    /// Gets or sets the <see cref="IStatePropertyAccessor{T}"/> for CounterState.
    /// </summary>
    /// <value>
    /// The accessor stores the turn count for the conversation.
    /// </value>
    public IStatePropertyAccessor<States> CurrentState { get; set; }

    /// <summary>
    /// Gets the <see cref="ConversationState"/> object for the conversation.
    /// </summary>
    /// <value>The <see cref="ConversationState"/> object.</value>
    public ConversationState ConversationState { get; }
  }
}
