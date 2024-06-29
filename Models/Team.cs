
public class Team
{
  public int Id { get; set; }
  public string constructorId { get; set; }
  public List<TeamDrivers> Drivers { get; set; }
}

public class TeamDrivers
{
  public int Id { get; set; }
  public string givenName { get; set; }
  public string familyName { get; set; }
  public string driverId { get; set; }
}