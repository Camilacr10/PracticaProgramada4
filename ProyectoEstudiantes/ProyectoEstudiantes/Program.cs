using ProyectoEstudiantesBLL.Mapeos;
using ProyectoEstudiantesBLL.Servicios;
using ProyectoEstudiantesDAL.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IEstudiantesRepositorio, EstudiantesRepositorio>();
builder.Services.AddSingleton<IEstudiantesServicio, EstudiantesServicio>();

builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

builder.Services.AddHttpClient<IEstudiantesRepositorio, EstudiantesRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Estudiantes",
    pattern: "{controller=Estudiante}/{action=Index}/{id?}");

app.Run();
