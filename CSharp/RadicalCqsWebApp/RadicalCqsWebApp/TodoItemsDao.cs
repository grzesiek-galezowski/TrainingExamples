using Core.Either;

namespace RadicalCqsWebApp;

public class TodoItemsDao
{
  public void SaveTodoItem(ITodoItemsDaoOperationCallback callback, TodoItemDto todoItemDto)
  {
    if (todoItemDto.DueDate != null)
    {
      callback.Success(Guid.NewGuid());
    }
    else
    {
      callback.Error(123);
    }
  }
}