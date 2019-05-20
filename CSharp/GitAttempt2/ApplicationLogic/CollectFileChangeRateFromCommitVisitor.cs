using System;
using System.Collections.Generic;

namespace ApplicationLogic
{
  public interface ITreeVisitor
  {
    void OnBlob(string filePath, string fileContent, DateTimeOffset changeDate);
  }

  public class CollectFileChangeRateFromCommitVisitor : ITreeVisitor
  {
    private readonly Dictionary<string, ChangeLog> _commitsPerPath;

    public CollectFileChangeRateFromCommitVisitor(Dictionary<string, ChangeLog> commitsPerPath)
    {
      _commitsPerPath = commitsPerPath;
    }

    public void OnBlob(string filePath, string fileContent, DateTimeOffset changeDate)
    {
      if (!_commitsPerPath.ContainsKey(filePath))
      {
        _commitsPerPath[filePath] = new ChangeLog();
      }
      AddChange(filePath, fileContent, changeDate);
    }

    public void OnModified(string filePath, string fileContent, DateTimeOffset changeDate)
    {
      AddChange(filePath, fileContent, changeDate);
    }

    private void AddChange(string filePath, string fileContent, DateTimeOffset changeDate)
    {
      _commitsPerPath[filePath].AddDataFrom(
        ChangeFactory.CreateChange(filePath, fileContent, changeDate));
    }

    public void OnRenamed(string oldPath, string newPath)
    {
      _commitsPerPath[oldPath] = _commitsPerPath[newPath];
    }

    public void OnCopied(string filePath, string fileContent, DateTimeOffset changeDate)
    {
      _commitsPerPath[filePath] = new ChangeLog();
      AddChange(filePath, fileContent, changeDate);
    }

    public void OnAdded(string filePath, string fileContent, DateTimeOffset changeDate)
    {
      _commitsPerPath[filePath] = new ChangeLog();
      AddChange(filePath, fileContent, changeDate);
    }
  }
}