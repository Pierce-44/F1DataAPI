using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1DataAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AverageSpeed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    speed = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AverageSpeed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    locality = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CircuitDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    circuitId = table.Column<string>(type: "TEXT", nullable: false),
                    circuitName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircuitDriver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constructor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    constructorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nationality = table.Column<string>(type: "TEXT", nullable: false),
                    dateOfBirth = table.Column<string>(type: "TEXT", nullable: false),
                    permanentNumber = table.Column<string>(type: "TEXT", nullable: false),
                    givenName = table.Column<string>(type: "TEXT", nullable: false),
                    familyName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverQualyResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    driverId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverQualyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    driverId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    driverId = table.Column<string>(type: "TEXT", nullable: false),
                    givenName = table.Column<string>(type: "TEXT", nullable: false),
                    familyName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualifying",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    date = table.Column<string>(type: "TEXT", nullable: false),
                    time = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifying", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualyCircuit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    circuitId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualyCircuit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualyDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    givenName = table.Column<string>(type: "TEXT", nullable: false),
                    familyName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualyDriver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    constructorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    time = table.Column<string>(type: "TEXT", nullable: false),
                    millis = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FastestLap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AverageSpeedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastestLap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FastestLap_AverageSpeed_AverageSpeedId",
                        column: x => x.AverageSpeedId,
                        principalTable: "AverageSpeed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarCircuit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    circuitId = table.Column<string>(type: "TEXT", nullable: false),
                    circuitName = table.Column<string>(type: "TEXT", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarCircuit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarCircuit_CalendarLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "CalendarLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    raceName = table.Column<string>(type: "TEXT", nullable: false),
                    CircuitId = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverResultId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Races_CircuitDriver_CircuitId",
                        column: x => x.CircuitId,
                        principalTable: "CircuitDriver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Races_DriverResults_DriverResultId",
                        column: x => x.DriverResultId,
                        principalTable: "DriverResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RacesQualy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    raceName = table.Column<string>(type: "TEXT", nullable: false),
                    CircuitId = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverQualyResultsId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacesQualy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacesQualy_DriverQualyResults_DriverQualyResultsId",
                        column: x => x.DriverQualyResultsId,
                        principalTable: "DriverQualyResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RacesQualy_QualyCircuit_CircuitId",
                        column: x => x.CircuitId,
                        principalTable: "QualyCircuit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    givenName = table.Column<string>(type: "TEXT", nullable: false),
                    familyName = table.Column<string>(type: "TEXT", nullable: false),
                    driverId = table.Column<string>(type: "TEXT", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamDrivers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    raceName = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<string>(type: "TEXT", nullable: false),
                    time = table.Column<string>(type: "TEXT", nullable: false),
                    CircuitId = table.Column<int>(type: "INTEGER", nullable: false),
                    QualifyingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendar_CalendarCircuit_CircuitId",
                        column: x => x.CircuitId,
                        principalTable: "CalendarCircuit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calendar_Qualifying_QualifyingId",
                        column: x => x.QualifyingId,
                        principalTable: "Qualifying",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    position = table.Column<string>(type: "TEXT", nullable: false),
                    positionText = table.Column<string>(type: "TEXT", nullable: false),
                    points = table.Column<string>(type: "TEXT", nullable: false),
                    grid = table.Column<string>(type: "TEXT", nullable: false),
                    TimeId = table.Column<int>(type: "INTEGER", nullable: false),
                    FastestLapId = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConstructorId = table.Column<int>(type: "INTEGER", nullable: false),
                    RaceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Constructor_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_FastestLap_FastestLapId",
                        column: x => x.FastestLapId,
                        principalTable: "FastestLap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Results_Time_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Time",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QualifyingResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    position = table.Column<string>(type: "TEXT", nullable: false),
                    DriverId = table.Column<int>(type: "INTEGER", nullable: false),
                    RacesQualyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifyingResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualifyingResults_QualyDriver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "QualyDriver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualifyingResults_RacesQualy_RacesQualyId",
                        column: x => x.RacesQualyId,
                        principalTable: "RacesQualy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_CircuitId",
                table: "Calendar",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_QualifyingId",
                table: "Calendar",
                column: "QualifyingId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarCircuit_LocationId",
                table: "CalendarCircuit",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FastestLap_AverageSpeedId",
                table: "FastestLap",
                column: "AverageSpeedId");

            migrationBuilder.CreateIndex(
                name: "IX_QualifyingResults_DriverId",
                table: "QualifyingResults",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_QualifyingResults_RacesQualyId",
                table: "QualifyingResults",
                column: "RacesQualyId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_CircuitId",
                table: "Races",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_DriverResultId",
                table: "Races",
                column: "DriverResultId");

            migrationBuilder.CreateIndex(
                name: "IX_RacesQualy_CircuitId",
                table: "RacesQualy",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_RacesQualy_DriverQualyResultsId",
                table: "RacesQualy",
                column: "DriverQualyResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ConstructorId",
                table: "Results",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_DriverId",
                table: "Results",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_FastestLapId",
                table: "Results",
                column: "FastestLapId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_RaceId",
                table: "Results",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TimeId",
                table: "Results",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamDrivers_TeamId",
                table: "TeamDrivers",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "QualifyingResults");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "TeamDrivers");

            migrationBuilder.DropTable(
                name: "CalendarCircuit");

            migrationBuilder.DropTable(
                name: "Qualifying");

            migrationBuilder.DropTable(
                name: "QualyDriver");

            migrationBuilder.DropTable(
                name: "RacesQualy");

            migrationBuilder.DropTable(
                name: "Constructor");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "FastestLap");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "CalendarLocation");

            migrationBuilder.DropTable(
                name: "DriverQualyResults");

            migrationBuilder.DropTable(
                name: "QualyCircuit");

            migrationBuilder.DropTable(
                name: "AverageSpeed");

            migrationBuilder.DropTable(
                name: "CircuitDriver");

            migrationBuilder.DropTable(
                name: "DriverResults");
        }
    }
}
