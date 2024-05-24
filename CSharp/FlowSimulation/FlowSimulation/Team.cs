using System.Collections.Immutable;

namespace FlowSimulation;

public class Team
{
  private readonly List<TeamMember> developers = [];

  public bool HasNoMembers()
  {
    return developers.Count == 0;
  }

  public void AssignWork(WorkItemsList workItems)
  {
    foreach (var backlogPart in workItems.AllItems())
    {
      backlogPart.UpdateAssignmentTo(developers.Where(d => !d.HasWork).ToImmutableList());
    }
  }

  public void WorkOnAssignedItems()
  {
    foreach (var dev in developers)
    {
      dev.WorkOnAssignedItem();
    }
    foreach (var dev in developers)
    {
      dev.CompleteDoneItems();
    }
  }


  public void AssertDoesNotAlreadyHaveMemberWith(string developerId)
  {
    if (HasDeveloperWith(developerId))
    {
      throw new Exception("Duplicate developer");
    }
  }

  public void Add(TeamMember teamMember)
  {
    developers.Add(teamMember);
  }

  private bool HasDeveloperWith(string developerId)
  {
    return developers.Exists(d => d.Has(developerId));
  }

  public void AssertHasSomeoneWithRole(string role)
  {
    if (!developers.Exists(d => d.HasRole(role)))
    {
      throw new Exception($"No developer with role {role} found");
    }
  }
}