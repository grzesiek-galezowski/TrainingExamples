namespace CombosExtensionMethods
{
  public class JinKazama : ComboSyntax
  {
    public void Combos()
    {
      "White Heron"              .  Sequence  (   LP.Plus(RK), RP, RK                );
      "Heihachi Smash"           .  Sequence  (   f.Plus(LP), b.Plus(RP), RK         );
      "Mishima Palm"             .  Sequence  (   f, f.Plus(RP)                      );
      "Twin Thrusts->Inner Axe"  .  Sequence  (   LP, RP, LK                         );
      "Twin Thrusts->Roundhouse" .  Sequence  (   LP, RP, RK                         );
      "Thrust Godfist"           .  Sequence  (   f, N, d, d.Plus(f).Plus(LP)        );
      "Electric Thrust Godfist"  .  Sequence  (   f, N, d.Plus(f, LP)                );
      "Savage Sword"             .  Sequence  (   d.Plus(b).Plus(RP), RP, LK         ); //or d.Plus(b.Plus(RP))!!
      "Spinning Blade"           .  Sequence  (   d.Plus(b).Plus(RK)                 );
      "Axe Kick->Kazama Fury"    .  Sequence  (   f, f.Plus(LK), LP, LK, RP, LP, RK  );
    }
  }


  //TODO alarms both nested functions and extension methods, and method chaining

  
}
