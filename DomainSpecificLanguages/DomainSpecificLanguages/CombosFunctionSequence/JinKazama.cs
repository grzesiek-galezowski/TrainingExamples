namespace CombosFunctionSequence
{


  public class JinKazama : ComboSyntax
  {
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
  }

}
