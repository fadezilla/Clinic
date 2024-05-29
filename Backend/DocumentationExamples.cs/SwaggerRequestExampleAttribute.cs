[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SwaggerRequestExampleAttribute : Attribute
{
    public Type ExampleType { get; }

    public SwaggerRequestExampleAttribute(Type exampleType)
    {
        ExampleType = exampleType;
    }
}
