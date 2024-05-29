using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PutDoctorExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["id"] = new OpenApiInteger(1),
            ["firstName"] = new OpenApiString("Jane"),
            ["lastName"] = new OpenApiString("Doe"),
            ["specialityId"] = new OpenApiInteger(1),
            ["clinicId"] = new OpenApiInteger(1)
        };
    }
}
