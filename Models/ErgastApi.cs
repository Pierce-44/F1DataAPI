
public class ErgastApiDriverResponse
{
  public DriverMRData MRData { get; set; }
}

public class DriverMRData
{
  public DriverTable DriverTable { get; set; }
}

public class DriverTable
{
  public List<Driver> Drivers { get; set; }
}

public class ErgastApiDriverResultsResponse
{
  public RaceMRData MRData { get; set; }
}

public class RaceMRData
{
  public RaceTable RaceTable { get; set; }
}

public class RaceTable
{
  public List<Race> Races { get; set; }
}
public class ErgastApiCalendarResponse
{
  public CalendarMRData MRData { get; set; }
}
public class CalendarMRData
{
  public RaceTableCalendar RaceTable { get; set; }
}
public class RaceTableCalendar
{
  public List<RaceInfo> Races { get; set; }
}
public class RaceInfo
{
  public string raceName { get; set; }
  public string date { get; set; }
  public string time { get; set; }
  public QualifyingTime Qualifying { get; set; }
  public CalendarCircuit Circuit { get; set; }
}
public class QualifyingTime
{
  public string date { get; set; }
  public string time { get; set; }
}

public class ErgastApiConstructorsResponse
{
  public ConstructorMRData MRData { get; set; }
}

public class ConstructorMRData
{
  public ConstructorTable ConstructorTable { get; set; }
}

public class ConstructorTable
{
  public List<ErgastConstructor> Constructors { get; set; }
}

public class ErgastConstructor
{
  public string constructorId { get; set; }
  public string name { get; set; }
}

public class ErgastApiTeamResponse
{
  public TeamMRData MRData { get; set; }
}

public class TeamMRData
{
  public TeamDriverTable DriverTable { get; set; }
}

public class TeamDriverTable
{
  public List<ErgastTeamDrivers> Drivers { get; set; }
}

public class ErgastTeamDrivers
{
  public string familyName { get; set; }
  public string givenName { get; set; }
  public string driverId { get; set; }
}
public class ErgastApiQualiResponse
{
  public QualyMRData MRData { get; set; }
}

public class QualyMRData
{
  public ErgastQualyRaceTable RaceTable { get; set; }
}

public class ErgastQualyRaceTable
{
  public List<ErgastQualyRaces> Races { get; set; }
}

public class ErgastQualyRaces
{
  public string raceName { get; set; }
  public ErgastCircuit Circuit { get; set; }
  public List<ErgastQualifyingResults> QualifyingResults { get; set; }
}

public class ErgastCircuit
{
  public string circuitId { get; set; }
}

public class ErgastQualifyingResults
{
  public string position { get; set; }
  public ErgastQualyDriver Driver { get; set; }
}

public class ErgastQualyDriver
{
  public string familyName { get; set; }
  public string givenName { get; set; }

}