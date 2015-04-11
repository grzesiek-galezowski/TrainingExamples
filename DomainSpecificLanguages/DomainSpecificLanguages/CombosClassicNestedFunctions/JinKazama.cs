using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombosNestedFunctions
{
  public class JinKazama : ComboSyntax
  {
    public void Combos()
    {
      Combo("White Heron"                , Both(LP(), RK()), RP(), RK());
      Combo("Heihachi Smash"             , Both(f(), LP()), Both(b(), RP()), RK());
      Combo("Mishima Palm"               , f(), Both(f(), RP()));
      Combo("Twin Thrusts->Inner Axe"    , LP(), RP(), LK());
      Combo("Twin Thrusts->Roundhouse"   , LP(), RP(), RK());
      Combo("Thrust Godfist"             , f(), N(), d(), All(d(), f(), LP()));
      Combo("Electric Thrust Godfist"    , f(), N(), All(d(), f(), LP()));
      Combo("Savage Sword"               , All(d(), b(), RP()), RP(), LK());
      Combo("Spinning Blade"             , All(d(), b(), RK()));
      Combo("Axe Kick->Kazama Fury"      , f(), Both(f(), LK()), LP(), LK(), RP(), LP(), RK());
    }
  }


}
