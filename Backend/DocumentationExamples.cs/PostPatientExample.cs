using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PostPatientExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["firstName"] = new OpenApiString("John"),
            ["lastName"] = new OpenApiString("Doe"),
            ["birthdate"] = new OpenApiString("1995-12-10"),
            ["email"] = new OpenApiString("john.doe@example.com")
        };
    }
}
