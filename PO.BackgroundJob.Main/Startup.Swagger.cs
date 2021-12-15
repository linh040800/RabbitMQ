﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PO.BackgroundJob.Main
{
    public partial  class Startup 
    {/// <summary>
     /// Register the Swagger generator, defining one or more Swagger documents
     /// </summary>
     /// <param name="services">The services.</param>
        public void ConfigureSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                //set API Name
                foreach (var item in ApiExplorerGroupNameCustom.groupName)
                {
                    c.SwaggerDoc(item, new OpenApiInfo
                    {
                        Title = "API PO version 1.0",
                        Version = "1.0",
                        Description = $"Group {item}"
                    });
                }

                //c.DocumentFilter<SwaggerEnumDocumentFilter>();

                //Add description JWT Bearer on API documents
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter JWT with Bearer into field.\n Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                //Add bearer security on header when API check each one request of client
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                //c.EnableAnnotations();
                c.CustomOperationIds((obj) => obj.GetHashCode().ToString());

                #region Hide parameter on functions when user unauthenticated
                //c.DocInclusionPredicate((docName, apiDesc) =>
                //{
                //    if (apiDesc.GroupName == docName)
                //    {
                //        return true;
                //    }
                //    return false;s
                //});
                #endregion

            });
        }
        /// <summary>
        /// Uses the swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for UseSwagger
        public void UseSwaggerUI(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (!env.IsProduction())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    foreach (var item in ApiExplorerGroupNameCustom.groupName)
                    {
                        c.SwaggerEndpoint($"../swagger/{item}/swagger.json", item);
                    }
                    c.DocExpansion(DocExpansion.None);
                    c.DisplayRequestDuration();
                    c.EnableDeepLinking();
                });

            }
        }
    }
}
