using Asp.Versioning.ApiExplorer;

namespace ReSale.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DefaultModelsExpandDepth(-1);

            IReadOnlyList<ApiVersionDescription> descriptions =
                app.DescribeApiVersions();

            descriptions
                .Select(d => d.GroupName)
                .ToList()
                .ForEach(groupName =>
                {
                    string url = $"/swagger/{groupName}/swagger.json";
                    string name = groupName.ToUpperInvariant();

                    options.SwaggerEndpoint(url, name);
                });
        });

        return app;
    }
}
