using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public static class HelloWorldHandler
{
  public static void MapHelloWorldEndpoint(this WebApplication app)
  {
    app.MapGet("/", () => "Hello from .NET Minimal API!");
  }
}
