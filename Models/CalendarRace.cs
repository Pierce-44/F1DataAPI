public class CalendarRace
{
  public int Id { get; set; }
  public string raceName { get; set; }
  public string date { get; set; }
  public string time { get; set; }
  public Qualifying Qualifying { get; set; }
}

public class Qualifying
{
  public int Id { get; set; }
  public string date { get; set; }
  public string time { get; set; }
}