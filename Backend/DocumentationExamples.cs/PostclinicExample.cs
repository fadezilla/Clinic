using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;

public class PostClinicExample : IExamplesProvider<OpenApiObject>
{
    public OpenApiObject GetExamples()
    {
        return new OpenApiObject
        {
            ["name"] = new OpenApiString("Clinic Name"),
            ["address"] = new OpenApiString("123 Street, City"),
            ["phoneNumber"] = new OpenApiInteger(123456789)
        };
    }
}
