using Asp.Versioning.ApiExplorer;

namespace ReSale.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

            foreach (ApiVersionDescription description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();

                options.SwaggerEndpoint(url, name);
            }
        });
        
        app.UseReDoc(options =>
        {
            options.RoutePrefix = "redoc";
            options.SpecUrl = "/swagger/v1/swagger.json";
            options.DocumentTitle = "ReSale API Documentation";
        });

        return app;
    }
}
