using ApiWeb;
string Cadena = "";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Cadena = builder.Configuration.GetConnectionString("BaseDeDatos");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();



app.MapGet("/VueloMasVisitado", async () =>
{
    SQLServerDatabaseHelper helper = new SQLServerDatabaseHelper(Cadena);
    try
    {
        return Results.Ok(await helper.ExtraeVueloMayor());
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
})
.WithName("VueloMasVisitado")
.WithOpenApi();

app.MapGet("/VueloMenosVisitado", async () =>
{
    SQLServerDatabaseHelper helper = new SQLServerDatabaseHelper(Cadena);
    try
    {
        return Results.Ok(await helper.ExtraeVueloMenor());
    }catch(Exception ex)
    {
        throw new Exception(ex.Message);
    }

})
.WithName("VueloMenosVisitado")
.WithOpenApi();

app.MapPost("/HorasAvion", (Avion avion) =>
{
    SQLServerDatabaseHelper helper = new SQLServerDatabaseHelper(Cadena);
    try
    {
        return Results.Ok( helper.ExtraeHoras(avion.ID));
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
})
.WithName("HorasAvion")
.WithOpenApi();


app.Run();

internal record Avion()
{
    public long ID { get; set; }
}
