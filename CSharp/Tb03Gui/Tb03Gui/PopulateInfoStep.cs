﻿using AtmaFileSystem;

namespace Tb03Gui;

internal class PopulateInfoStep : ISelectedTb03BackupFolderProcessingStep
{
  private readonly ITb03FolderProcessingObserver _observer;

  public PopulateInfoStep(ITb03FolderProcessingObserver observer)
  {
    _observer = observer;
  }

  public void Handle(AbsoluteDirectoryPath folderPath)
  {
    _observer.PathIsOk(folderPath);
  }
}