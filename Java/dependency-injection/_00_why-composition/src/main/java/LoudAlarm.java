public class LoudAlarm
  {
    private final Sirens sirens;

    public LoudAlarm()
    {
      //what if we want to use another sound?
      sirens = new Sirens();
    }

    public void trigger()
    {
      sirens.play();
    }

    public void disable()
    {
      sirens.stop();
    }
  }
