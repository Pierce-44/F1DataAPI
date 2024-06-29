public class CalendarRace
{
  public int Id { get; set; }
  public string raceName { get; set; }
  public string date { get; set; }
  public string time { get; set; }

  public CalendarCircuit Circuit { get; set; }
  public Qualifying Qualifying { get; set; }
}

public class Qualifying
{
  public int Id { get; set; }
  public string date { get; set; }
  public string time { get; set; }
}

public class CalendarCircuit
{
  public int Id { get; set; }
  public string circuitId { get; set; }
  public string circuitName { get; set; }
  public CalendarLocation Location { get; set; }
}

public class CalendarLocation
{
  public int Id { get; set; }
  public string locality { get; set; }
  public string country { get; set; }
}