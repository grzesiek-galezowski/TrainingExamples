using System;

namespace SessionsRefactored.Destinations
{
  // Demonstrates that clients are still free to decide which fields to take into account
  public class PopulationOfOwnersListOnGui : DumpDestination
  {
    private readonly GuiOwnersList _ownersList;

    public PopulationOfOwnersListOnGui(GuiOwnersList ownersList)
    {
      _ownersList = ownersList;
    }

    public void BeginNewSessionDump()
    {
      //not interested
    }

    public void AddId(int id)
    {
      //not interested
    }

    public void AddOwner(string owner)
    {
      _ownersList.AddVisible(owner);
    }

    public void AddTarget(string target)
    {
      //not interested
    }

    public void EndCurrentSessionDump()
    {
      //not interested
    }
  }
}