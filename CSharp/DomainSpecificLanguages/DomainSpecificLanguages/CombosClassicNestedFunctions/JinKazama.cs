namespace CombosClassicNestedFunctions
{
  public class JinKazama : ComboSyntax //context superclass
  {
    public void Combos()
    {
      Combo("White Heron"                , All(LP(), RK()), RP(), RK());
      Combo("Heihachi Smash"             , All(f(), LP()), All(b(), RP()), RK());
      Combo("Mishima Palm"               , f(), All(f(), RP()));
      Combo("Twin Thrusts->Inner Axe"    , LP(), RP(), LK());
      Combo("Twin Thrusts->Roundhouse"   , LP(), RP(), RK());
      Combo("Thrust Godfist"             , f(), N(), d(), All(d(), f(), LP()));
      Combo("Electric Thrust Godfist"    , f(), N(), All(d(), f(), LP()));
      Combo("Savage Sword"               , All(d(), b(), RP()), RP(), LK());
      Combo("Spinning Blade"             , All(d(), b(), RK()));
      Combo("Axe Kick->Kazama Fury"      , f(), All(f(), LK()), LP(), LK(), RP(), LP(), RK());
    }
  }


}
