using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Backend.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clinic Booking", Version = "v1" });

                // Register custom example filters
                c.ExampleFilters();

                c.OperationFilter<SwaggerExamplesOperationFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<PostAppointmentExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PutAppointmentExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PostClinicExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PutClinicExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PostDoctorExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PutDoctorExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PostPatientExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PutPatientExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PostSpecialityExample>();
            services.AddSwaggerExamplesFromAssemblyOf<PutSpecialityExample>();
        }
    }
}
