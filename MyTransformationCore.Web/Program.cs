using MyTransformationCore.Repository.Managers;
using MyTransformationCore.Repository.Repositories;
using MyTransformationCore.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddMongoDbClient();
builder.Services.AddSingleton<IExerciseRepository, ExerciseRepository>();
builder.Services.AddSingleton<IExerciseManager, ExerciseManager>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserManager, UserManager>();
builder.Services.AddSingleton<IUserPhotoRepository, UserPhotoRepository>();
builder.Services.AddSingleton<IUserPhotoManager, UserPhotoManager>();
builder.Services.AddSingleton<IUserProgressRepository, UserProgressRepository>();
builder.Services.AddSingleton<IUserProgressManager, UserProgressManager>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();
