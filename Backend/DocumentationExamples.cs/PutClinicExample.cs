using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PutClinicExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["id"] = new OpenApiInteger(1),
            ["name"] = new OpenApiString("Clinic Name"),
            ["address"] = new OpenApiString("123 Street, City"),
            ["phoneNumber"] = new OpenApiInteger(123456789)
        };
    }
}
