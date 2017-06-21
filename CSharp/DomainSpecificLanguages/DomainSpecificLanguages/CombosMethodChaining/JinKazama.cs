using System.Collections.Generic;

namespace CombosMethodChaining
{
  public class JinKazama
  {
    public List<ComboData> Combos()
    {
      return Combo("White Heron").               LP.plus.RK. RP. RK.
             Combo("Heihachi Smash").            f.plus.LP. b.plus.RP. RK.
             Combo("Mishima Palm").              f. f.plus.RP.
             Combo("Twin Thrusts->Inner Axe").   LP. RP. LK.
             Combo("Twin Thrusts->Roundhouse").  LP. RP. RK.
             Combo("Thrust Godfist").            f. N. d. d.plus.f.plus.LP.
             Combo("Electric Thrust Godfist").   f. N. d.plus.f.plus.LP.
             Combo("Savage Sword").              d.plus.b.plus.RP. RP. LK.
             Combo("Spinning Blade").            d.plus.b.plus.RK.
             Combo("Axe Kick->Kazama Fury").     f. f.plus.LK. LP. LK. RP. LP. RK.
             End; //!!! needed "build" method or implicit cast
    }

    private ComboBuilder Combo(string name) //!! helper method
    {
      return new ComboBuilder(name);
    }
  }

}
