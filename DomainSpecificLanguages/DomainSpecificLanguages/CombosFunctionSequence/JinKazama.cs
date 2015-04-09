using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombosNestedFunctions
{
  public class JinKazama// : ComboSyntax
    {
    private const bool Hold = true;

    public void Combos()
    {
      Combo("White Heron")               ; LP(Hold); RK(); RP(); RK();
      Combo("Heihachi Smash")            ; f(Hold); LP(); b(Hold); RP(); RK();
      Combo("Mishima Palm")              ; f(); f(Hold); RP();
      Combo("Twin Thrusts->Inner Axe")   ; LP(); RP(); LK();
      Combo("Twin Thrusts->Roundhouse")  ; LP(); RP(); RK();
      Combo("Thrust Godfist")            ; f(); N(); d(); d(Hold); f(Hold); LP();
      Combo("Electric Thrust Godfist")   ; f(); N(); d(Hold); f(Hold); LP();
      Combo("Savage Sword")              ; d(Hold); b(Hold); RP(); RP(); LK();
      Combo("Spinning Blade")            ; d(Hold); b(Hold); RK();
      Combo("Axe Kick->Kazama Fury")     ; f(); f(Hold); LK(); LP(); LK(); RP(); LP(); RK();
    }

    private void d(bool hold = false)
    {
      throw new System.NotImplementedException();
    }

    private void N()
    {
      throw new System.NotImplementedException();
    }

    private void LK()
    {
      throw new System.NotImplementedException();
    }

    private void b(bool hold = false)
    {
      throw new System.NotImplementedException();
    }

    private void f(bool hold = false)
    {
      throw new System.NotImplementedException();
    }

    private void RP()
    {
      throw new System.NotImplementedException();
    }

    private void RK()
    {
      throw new System.NotImplementedException();
    }

    private void LP(bool hold = false)
    {
      throw new System.NotImplementedException();
    }

    private void Combo(string name)
      {
        throw new System.NotImplementedException();
      }
    }

}
