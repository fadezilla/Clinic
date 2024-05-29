using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerExamplesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody != null && operation.RequestBody.Content.Any())
        {
            var contentType = operation.RequestBody.Content.First().Key;
            var requestBody = operation.RequestBody.Content[contentType];

            if (context.ApiDescription.HttpMethod == "POST")
            {
                if (context.ApiDescription.RelativePath.Contains("Appointments"))
                {
                    requestBody.Example = new PostAppointmentExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Clinic"))
                {
                    requestBody.Example = new PostClinicExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Doctor"))
                {
                    requestBody.Example = new PostDoctorExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Speciality"))
                {
                    requestBody.Example = new PostSpecialityExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Patient"))
                {
                    requestBody.Example = new PostPatientExample().GetExamples();
                
                }
            }
            else if (context.ApiDescription.HttpMethod == "PUT")
            {
                if (context.ApiDescription.RelativePath.Contains("Appointments"))
                {
                    requestBody.Example = new PutAppointmentExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Clinic"))
                {
                    requestBody.Example = new PutClinicExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Doctor"))
                {
                    requestBody.Example = new PutDoctorExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Speciality"))
                {
                    requestBody.Example = new PutSpecialityExample().GetExamples();
                }
                else if (context.ApiDescription.RelativePath.Contains("Patient"))
                {
                    requestBody.Example = new PutPatientExample().GetExamples();
                }
            }
        }
    }
}
