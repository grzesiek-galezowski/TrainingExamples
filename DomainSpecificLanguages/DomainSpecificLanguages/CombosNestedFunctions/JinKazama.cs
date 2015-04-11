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
      var moves = 
      MoveList(
        Combo("White Heron", LP + RK, RP, RK),
        Combo("Heihachi Smash", f + LP, b + RP, RK),
        Combo("Mishima Palm", f, f + RP),
        Combo("Twin Thrusts->Inner Axe", LP, RP, LK),
        Combo("Twin Thrusts->Roundhouse", LP, RP, RK),
        Combo("Thrust Godfist", f, N, d, d/f + LP),
        Combo("Electric Thrust Godfist", f, N, d/f + LP),
        Combo("Savage Sword", d/b + RP, RP, LK),
        Combo("Spinning Blade", d/b + RK),
        Combo("Axe Kick->Kazama Fury", f, f + LK, LP, LK, RP, LP, RK)
      );

    }
  }

}
