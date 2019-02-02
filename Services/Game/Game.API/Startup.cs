using FluentValidation;
using FluentValidation.AspNetCore;
using Game.API.Application.Commands;
using Game.API.Application.OutcomeStrategy;
using Game.API.Application.OutcomeStrategy.Handlers;
using Game.API.Application.Queries;
using Game.API.Models;
using Game.API.Models.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace Game.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // TODO: Cors policy from config.
                options.AddPolicy("TempDevCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddFluentValidation();

            //TODO: Push this out to middleware/extensions.
            services.AddTransient<IGameQueries, GameQueries>();
            services.AddTransient<IGameCommands, GameCommands>();
            services.AddTransient<IOutcomeStrategy, OutcomeStrategy>();
            services.AddTransient<IOutcomeHandler, StraightFlushHandler>();
            services.AddTransient<IOutcomeHandler, FlushHandler>();
            services.AddTransient<IOutcomeHandler, StraightHandler>();
            services.AddTransient<IOutcomeHandler, PairHandler>();
            services.AddTransient<IOutcomeHandler, HighCardHandler>();

            services.AddTransient<IValidator<AppendRoundRequest>, AppendRoundRequestValidator>();
            services.AddTransient<IValidator<RankRoundRequest>, RankRoundRequestValidator>();
            services.AddTransient<IValidator<DealCardsRequest>, DealCardsRequestValidator>();
            services.AddTransient<IValidator<ShuffleDeckRequest>, ShuffleDeckRequestValidator>();
            services.AddTransient<IValidator<NewGameRequest>, NewGameRequestValidator>();
            services.AddTransient<IValidator<OverallResultRequest>, OverallResultRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("TempDevCorsPolicy");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
