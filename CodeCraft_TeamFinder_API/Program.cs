
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using CodeCraft_TeamFinder_Services;
using CodeCraft_TeamFinder_Services.Interfaces;
using Hangfire;
using Quartz;

namespace CodeCraft_TeamFinder_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<MongoDbService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IAssignmentProposalService, AssignmentProposalService>();
            builder.Services.AddScoped<IDeallocationProposalService, DeallocationProposalService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectTeamService, ProjectTeamService>();
            builder.Services.AddScoped<ISkillCategoryService, SkillCategoryService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<ISystemRoleService, SystemRoleService>();
            builder.Services.AddScoped<ITeamRoleService, TeamRoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddTransient(typeof(Lazy<>), typeof(LazilyResolved<>));

            builder.Services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(SkillUpgradeProposal));

                configure
                    .AddJob<SkillUpgradeProposal>(jobKey)
                    .AddTrigger(
                        trigger => trigger.ForJob(jobKey).WithSimpleSchedule(
                            schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));
            });

            builder.Services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("cors-policy",
                                  builder =>
                                  {
                                      builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();

                                  });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseCors("cors-policy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
