using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data.Common;
using TRAINING.API.GraphQL;
using TRAINING.API.Schema.Queries;
using TRAINING.API.Schema.Mutation;

namespace TRAINING.API
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
            services.AddControllers()
            .AddJsonOptions(o =>
            {
                // ignore null field when serialized
                // o.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
            });
            services.AddCors();
            services.AddPooledDbContextFactory<AMGContext>(x => x.UseSqlServer(Configuration.GetConnectionString("AMGConnection")).LogTo(Console.WriteLine));
            services.AddDbContext<ECSContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ECSConnection")));
            services.AddDbContext<AMGContext>(x => x.UseSqlServer(Configuration.GetConnectionString("AMGConnection")).LogTo(Console.WriteLine));

            services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<PartNumberType>()
                .AddTypeExtension<PartNumberQuery>()
                .AddFiltering();

            // services.AddDbContext<APRISEContext>(x => x.UseOracle(Configuration.GetConnectionString("APRISEConnection")));            
            // services.AddDbContext<ORDSContext>(x => x.UseDb2(Configuration.GetConnectionString("ORDSConnection"), 
            //     action => {
            //         action.CommandTimeout(10);
            //     }));
            // DbProviderFactories.RegisterFactory("IBM.data.DB2", IBM.Data.Db2.DB2Factory.Instance);
            // DbConnection db = DbProviderFactories.GetFactory("IBM.data.DB2").CreateConnection();
            // db.ConnectionString = Configuration.GetConnectionString("ORDSConnection");
            // services.AddDbContext<ORDSContext>(x => x.UseDb2(db, action => {
            //     action.CommandTimeout(10);
            // }));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<ICheckSheetRepository, CheckSheetRepository>();
            services.AddScoped<IPalletTypeRepository, PalletTypeRepository>();

            // services.AddScoped<IORDSRepository, ORDSRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MySuperSecurePassword")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            var mapConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TRAINING.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TRAINING.API v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200"));

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL()
                    .WithOptions(new HotChocolate.AspNetCore.GraphQLServerOptions()
                    {
                        EnableSchemaRequests = env.IsDevelopment(),
                        Tool = { Enable = env.IsDevelopment() }
                    });
                // endpoints.MapFallbackToController("Index", "Fallback");
            });

            // app.UseEndpoints(endpoints => {
            //     endpoints.MapControllers();

            // });
        }
    }
}
