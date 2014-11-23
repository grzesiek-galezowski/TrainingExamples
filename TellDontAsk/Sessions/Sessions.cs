using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessions
{
/*
“zapaszki” do wyeliminowania:
1.   W przypadku synchronizacji wielowątkowej (zarówno do sesji jak i do kolekcji sesji) każdy kod kliencki musi zawierać locka
2.   Jeśli dojdzie nowa dana do sesji, to
a.   Wszyscy klienci kodu muszą zostać zmuszeni (np. przez błąd kompilacji) do świadomego podjęcia decyzji czy chcą wspierać nowe własnośc czy nie, tzn. nie powinno być możliwości zmiany jednego klienta nie wiedząc, że inni muszą się zmienić
b.   [Opcjonalne] kolejność wpisywania danych do pliku/konsoli/ramki ma być modyfikowana w jednym miejscu
*/

  public class Sessions
  {
    readonly List<Session> _sessions = new List<Session>();
    public void Add(Session session)
    {
      _sessions.Add(session);
    }
    public IEnumerable<Session> GetAll()
    {
      return _sessions;
    }
  }
}

