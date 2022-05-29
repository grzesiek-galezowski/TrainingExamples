using Lib;

namespace _06_Dialog;

public class LicensePlateQueryForm
{
  private LicensePlateQueryData _queryData = new(null, null);

  public void UpdateWith(LicensePlateQueryData queryData)
  {
    _queryData = new LicensePlateQueryData(
      queryData.Number ?? queryData.Number,
      queryData.State ?? queryData.State);
  }

  public bool IsComplete()
  {
    return _queryData.State is not null && _queryData.Number is not null;
  }

  public async Task Submit()
  {
    var completeQueryData = _queryData;
    var carMake = FederalDatabase.FindCar(completeQueryData);
    var response = "The car with " +
                   $"license plate: {completeQueryData.Number} " +
                   $"in state {completeQueryData.State} " +
                   $"is {carMake}";

    await User.RespondWith(response);
  }

  public async Task PromptForMissingFields()
  {
    await User.RespondWith(MissingFieldsMessage());
  }

  private string MissingFieldsMessage()
  {
    var missingFields = new[] { ("state", _queryData.State), ("number", _queryData.Number) }
      .Where(t => t.Item2 is null)
      .Select(t => t.Item1);

    return $"Missing fields: {string.Join(" and ", missingFields)}.";
  }

  public void Clear()
  {
    _queryData = new LicensePlateQueryData(null, null);
  }
}