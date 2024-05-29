using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PostDoctorExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["firstName"] = new OpenApiString("Jane"),
            ["lastName"] = new OpenApiString("Doe"),
            ["specialityId"] = new OpenApiInteger(1),
            ["clinicId"] = new OpenApiInteger(1)
        };
    }
}
