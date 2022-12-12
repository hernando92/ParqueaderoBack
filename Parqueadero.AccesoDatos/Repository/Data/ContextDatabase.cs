using Microsoft.EntityFrameworkCore;
using Parqueadero.Entidades.Data;

namespace Parqueadero.AccesoDatos.Repository.Data
{
    public class ContextDatabase : DbContext, IContextDatabase
    {
        //Constructor sin parametros
        public ContextDatabase()
        {
        }

        //Constructor con parametros para la configuracion
        public ContextDatabase(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        //Sobreescribimos el metodo OnConfiguring para hacer los ajustes que queramos en caso de
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //En caso de que el contexto no este configurado, lo configuramos mediante la cadena de conexion
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-MR929M0;Initial Catalog=Parqueadero;Integrated Security=False;User ID=dev;Password=dev+.;");
            }
        }

        //Tablas de datos
        public DbContext Instance => this;

        public DbSet<Tarifa> Tarifa { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        public DbSet<RegistroVehiculo> RegistroVehiculo { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }

    }
}
