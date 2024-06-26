using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Gate.Api.Extensions
{
    public static class SwaggerSetup
    {
        public static void ConfigureSwagger(this IServiceCollection services) 
        { 
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gate.Api",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espaço] então coloque seu token.
                                Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void UseSwaggerUI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
                if (apiVersionProvider == null)
                    throw new ArgumentException("API Versioning not registered.");
                    
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName);
                }
                options.RoutePrefix = string.Empty;

                options.DocExpansion(DocExpansion.List);
            });
        }
    }
}