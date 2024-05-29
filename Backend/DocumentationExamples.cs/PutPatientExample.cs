using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PutPatientExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["id"] = new OpenApiInteger(1),
            ["firstName"] = new OpenApiString("John"),
            ["lastName"] = new OpenApiString("Doe"),
            ["birthdate"] = new OpenApiString("1995-12-10"),
            ["email"] = new OpenApiString("john.doe@example.com")
        };
    }
}