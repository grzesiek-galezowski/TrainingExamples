using CompositionMaintenanceExample.Controllers;
using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;

namespace CompositionMaintenanceExample;

public class ServiceLogicRoot(ILoggerFactory loggerFactory)
{
  private readonly List<WeatherForecastDto1> _controller1State = new();
  private readonly List<WeatherForecastDto2> _controller2State = new();
  private readonly List<WeatherForecastDto3> _controller3State = new();
  private readonly List<WeatherForecastDto4> _controller4State = new();
  private readonly List<WeatherForecastDto5> _controller5State = new();
  private readonly List<WeatherForecastDto6> _controller6State = new();
  private readonly List<WeatherForecastDto7> _controller7State = new();
  private readonly List<WeatherForecastDto8> _controller8State = new();
  private readonly List<WeatherForecastDto9> _controller9State = new();
  private readonly List<WeatherForecastDto10> _controller10State = new();
  private readonly List<WeatherForecastDto11> _controller11State = new();
  private readonly List<WeatherForecastDto12> _controller12State = new();
  private readonly List<WeatherForecastDto13> _controller13State = new();
  private readonly List<WeatherForecastDto14> _controller14State = new();
  private readonly List<WeatherForecastDto15> _controller15State = new();
  private readonly List<WeatherForecastDto16> _controller16State = new();
  private readonly List<WeatherForecastDto17> _controller17State = new();
  private readonly List<WeatherForecastDto18> _controller18State = new();
  private readonly List<WeatherForecastDto19> _controller19State = new();
  private readonly List<WeatherForecastDto20> _controller20State = new();

  public WeatherForecast1Controller CreateWeatherForecastController1() =>
    new(loggerFactory.CreateLogger<WeatherForecast1Controller>(),
      _controller1State, new A11(), new A12(), new A13(), new A14());

  public WeatherForecast2Controller CreateWeatherForecastController2() =>
    new(loggerFactory.CreateLogger<WeatherForecast2Controller>(),
      _controller2State, new A21(), new A22(), new A23(), new A24());

  public WeatherForecast3Controller CreateWeatherForecastController3() =>
    new(loggerFactory.CreateLogger<WeatherForecast3Controller>(),
      _controller3State, new A31(), new A32(), new A33(), new A34());

  public WeatherForecast4Controller CreateWeatherForecastController4() =>
    new(loggerFactory.CreateLogger<WeatherForecast4Controller>(),
      _controller4State, new A41(), new A42(), new A43(), new A44());

  public WeatherForecast5Controller CreateWeatherForecastController5() =>
    new(loggerFactory.CreateLogger<WeatherForecast5Controller>(),
      _controller5State, new A51(), new A52(), new A53(), new A54());

  public WeatherForecast6Controller CreateWeatherForecastController6() =>
    new(loggerFactory.CreateLogger<WeatherForecast6Controller>(),
      _controller6State, new A61(), new A62(), new A63(), new A64());

  public WeatherForecast7Controller CreateWeatherForecastController7() =>
    new(loggerFactory.CreateLogger<WeatherForecast7Controller>(),
      _controller7State, new A71(), new A72(), new A73(), new A74());

  public WeatherForecast8Controller CreateWeatherForecastController8() =>
    new(loggerFactory.CreateLogger<WeatherForecast8Controller>(),
      _controller8State, new A81(), new A82(), new A83(), new A84());

  public WeatherForecast9Controller CreateWeatherForecastController9() =>
    new(loggerFactory.CreateLogger<WeatherForecast9Controller>(),
      _controller9State, new A91(), new A92(), new A93(), new A94());

  public WeatherForecast10Controller CreateWeatherForecastController10() =>
    new(loggerFactory.CreateLogger<WeatherForecast10Controller>(),
      _controller10State, new A101(), new A102(), new A103(), new A104());

  public WeatherForecast11Controller CreateWeatherForecastController11() =>
    new(loggerFactory.CreateLogger<WeatherForecast11Controller>(),
      _controller11State, new A111(), new A112(), new A113(), new A114());

  public WeatherForecast12Controller CreateWeatherForecastController12() =>
    new(loggerFactory.CreateLogger<WeatherForecast12Controller>(),
      _controller12State, new A121(), new A122(), new A123(), new A124());

  public WeatherForecast13Controller CreateWeatherForecastController13() =>
    new(loggerFactory.CreateLogger<WeatherForecast13Controller>(),
      _controller13State, new A131(), new A132(), new A133(), new A134());

  public WeatherForecast14Controller CreateWeatherForecastController14() =>
    new(loggerFactory.CreateLogger<WeatherForecast14Controller>(),
      _controller14State, new A141(), new A142(), new A143(), new A144());

  public WeatherForecast15Controller CreateWeatherForecastController15() =>
    new(loggerFactory.CreateLogger<WeatherForecast15Controller>(),
      _controller15State, new A151(), new A152(), new A153(), new A154());

  public WeatherForecast16Controller CreateWeatherForecastController16() =>
    new(loggerFactory.CreateLogger<WeatherForecast16Controller>(),
      _controller16State, new A161(), new A162(), new A163(), new A164());

  public WeatherForecast17Controller CreateWeatherForecastController17() =>
    new(loggerFactory.CreateLogger<WeatherForecast17Controller>(),
      _controller17State, new A171(), new A172(), new A173(), new A174());

  public WeatherForecast18Controller CreateWeatherForecastController18() =>
    new(loggerFactory.CreateLogger<WeatherForecast18Controller>(),
      _controller18State, new A181(), new A182(), new A183(), new A184());

  public WeatherForecast19Controller CreateWeatherForecastController19() =>
    new(loggerFactory.CreateLogger<WeatherForecast19Controller>(),
      _controller19State, new A191(), new A192(), new A193(), new A194());

  public WeatherForecast20Controller CreateWeatherForecastController20() =>
    new(loggerFactory.CreateLogger<WeatherForecast20Controller>(),
      _controller20State, new A201(), new A202(), new A203(), new A204());
}