using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PutSpecialityExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["id"] = new OpenApiInteger(1),
            ["name"] = new OpenApiString("Cardiology")
        };
    }
}