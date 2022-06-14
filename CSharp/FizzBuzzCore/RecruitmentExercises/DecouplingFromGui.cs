using NUnit.Framework;

namespace RecruitmentExercises;

/***************** GUI CODE **********************************/

public class Gui
{
  private readonly Logic _logic = new Logic();

  public void GetAgeButton_OnClick()
  {
    _logic.HandleGettingAge();
  }
}

public class MessageBox
{
  public static void Show(string text)
  {
    LastMessage = text;
  }

  public static string LastMessage { get; private set; }
}

/***************** APP LOGIC **********************************/

public class Logic
{
  private readonly Database _database = new Database();

  public void HandleGettingAge()
  {
    var age = _database.RetrieveAge();
    
    //What SOLID principles does this line potentially violate?
    MessageBox.Show("The age is " + age);
  }
}

/********************* DATABASE ******************************/

public class Database
{
  public uint RetrieveAge()
  {
    return 12;
  }
}

public class DecouplingFromGuiExamples
{
  [Test]
  public void Example()
  {
    //GIVEN
    var gui = new Gui();
    
    //WHEN
    gui.GetAgeButton_OnClick();

    //THEN
    Assert.AreEqual("The age is 12", MessageBox.LastMessage);
  }
}