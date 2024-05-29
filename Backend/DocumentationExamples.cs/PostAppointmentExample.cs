using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PostAppointmentExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["category"] = new OpenApiString("checkup"),
            ["date"] = new OpenApiString("29.05.2024 09:00"),
            ["socialSecurityNumber"] = new OpenApiLong(14109541525),
            ["clinicId"] = new OpenApiInteger(1),
            ["duration"] = new OpenApiInteger(60),
            ["patient"] = new OpenApiObject
            {
                ["firstName"] = new OpenApiString("John"),
                ["lastName"] = new OpenApiString("Doe"),
                ["birthdate"] = new OpenApiString("1995-12-10"),
                ["email"] = new OpenApiString("john.doe@example.com")
            }
        };
    }
}
