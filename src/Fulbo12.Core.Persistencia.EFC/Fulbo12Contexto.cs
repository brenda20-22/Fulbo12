using Fulbo12.Core.Futbol;
using Fulbo12.Core.Persistencia.EFC.Mapeos;
using Microsoft.Extensions.Configuration;

namespace Fulbo12.Core.Persistencia.EFC;
public sealed class Fulbo12Contexto : DbContext
{
    public DbSet<Pais> Paises => Set<Pais>();
    public DbSet<Persona> Personas => Set<Persona>();
    public DbSet<Liga> Ligas => Set<Liga>();
    public DbSet<Equipo> Equipo => Set<Equipo>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfiguration<Pais>(new MapPais());
        mb.ApplyConfiguration<Persona>(new MapPersona());
        mb.ApplyConfiguration<Liga>(new MapLiga());
        mb.ApplyConfiguration<Equipo>(new MapEquipo());
        mb.ApplyConfiguration<Posicion>(new MapPosicion());
        mb.ApplyConfiguration<Futbolista>(new MapFutbolista());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder ob)
    {
        if (!ob.IsConfigured)
        {
            IConfiguration myConfig = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appSettings.json")
                .Build();

            string strConexion = myConfig.GetConnectionString("dev");
            var serverVersion = new MySqlServerVersion(versionString: myConfig["SerVersion"]);
            ob.UseMySql(strConexion, serverVersion);
        }
    }
}