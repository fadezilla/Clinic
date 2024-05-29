using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PostSpecialityExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["name"] = new OpenApiString("Cardiology")
        };
    }
}
