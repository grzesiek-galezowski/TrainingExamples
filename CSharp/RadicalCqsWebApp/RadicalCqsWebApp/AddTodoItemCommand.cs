using Core.Maybe;

namespace RadicalCqsWebApp;

public class AddTodoItemCommand(
  ResponseInProgress responseInProgress,
  AddTodoItemDto addTodoItemDto,
  TranslationService translationService,
  TodoItemsDao database,
  ReminderApi reminderApi,
  ILogger<AddTodoItemCommand> logger) 
  : ITodoItemsDaoOperationCallback, ISetReminderCallback
{
  public void Execute()
  {
    var maybeTranslatedDto = translationService.Translate(addTodoItemDto);
    database.SaveTodoItem(
      this,
      new TodoItemDto(
        addTodoItemDto.Title,
        addTodoItemDto.DueDate,
        maybeTranslatedDto
          .Select(dto => dto.TranslatedContent)
          .OrElse(addTodoItemDto.Content)));

  }

  void ITodoItemsDaoOperationCallback.Success(Guid assignedId)
  {
    if (addTodoItemDto.DueDate != null)
    {
      reminderApi.RemindAbout(this, addTodoItemDto.Title, addTodoItemDto.DueDate.Value);
    }
  }

  void ITodoItemsDaoOperationCallback.Error(int errorCode)
  {
    logger.LogError("Error saving todo item: {ErrorCode}", errorCode);
  }

  void ISetReminderCallback.Success(DateTime dueDate)
  {
    responseInProgress.Success(new AddTodoResponseDto(new DateOnly(), 1, false));
  }

  void ISetReminderCallback.Error(string message)
  {
    logger.LogError(message);
  }
}

//public void Execute()
//{
//  analyticsApi.TrackEvent(nameof(AddTodoItemCommand), addTodoItemDto.Author, this);
//  var translatedDto = translationService.Translate(addTodoItemDto);
//  database.SaveTodoItem(
//    new TodoItemDto(
//      addTodoItemDto.Title,
//      addTodoItemDto.DueDate,
//      translatedDto.TranslatedContent));
//  if (addTodoItemDto.DueDate != null)
//  {
//    reminderApi.RemindAbout(addTodoItemDto.Title, addTodoItemDto.DueDate.Value);
//  }
//
//  if (addTodoItemDto.Title.Contains("crime"))
//  {
//    police.SubmitForAudit(addTodoItemDto.Title, addTodoItemDto.Content, addTodoItemDto.Author);
//  }
//  responseInProgress.Success(new AddTodoResponseDto(new DateOnly(), 1, false));
//}
