using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.AssignmentProposalDAL;
using TeamFinderDataAccess.OrganizationDAL;
using TeamFinderDataAccess.DeallocationProposalDAL;
using TeamFinderDataAccess.DepartmentDAL;
using TeamFinderDataAccess.ProjectDAL;
using TeamFinderDataAccess.ProjectTeamDAL;
using TeamFinderDataAccess.SkillDAL;
using TeamFinderDataAccess.SkillCategoryDAL;
using TeamFinderDataAccess.SystemRoleDAL;
using TeamFinderDataAccess.TeamRoleDAL;
using TeamFinderDataAccess.UserDAL;

namespace TeamFinderAPI
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

            builder.Services.AddScoped<IAssignmentProposalAccessor, AssignmentProposalAccessor>();
            builder.Services.AddScoped<IDeallocationProposalAccessor, DeallocationProposalAccessor>();
            builder.Services.AddScoped<IDepartmentAccessor, DepartmentAccessor>();
            builder.Services.AddScoped<IOrganizationAccessor, OrganizationAccessor>();
            builder.Services.AddScoped<IProjectAccessor, ProjectAccessor>();
            builder.Services.AddScoped<IProjectTeamAccessor, ProjectTeamAccessor>();
            builder.Services.AddScoped<ISkillAccessor, SkillAccessor>();
            builder.Services.AddScoped<ISkillCategoryAccessor, SkillCategoryAccessor>();
            builder.Services.AddScoped<ISystemRoleAccessor, SystemRoleAccessor>();
            builder.Services.AddScoped<ITeamRoleAccessor, TeamRoleAccessor>();
            builder.Services.AddScoped<IUserAccessor, UserAccessor>();

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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           

            app.UseCors("cors-policy");

           

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
