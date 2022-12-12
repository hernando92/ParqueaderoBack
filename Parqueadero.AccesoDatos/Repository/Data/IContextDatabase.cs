using Microsoft.EntityFrameworkCore;
using Parqueadero.Entidades.Data;

namespace Parqueadero.AccesoDatos.Repository.Data
{

    public interface IContextDatabase
    {
        DbSet<Tarifa> Tarifa { get; set; }
        DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        DbSet<RegistroVehiculo> RegistroVehiculo { get; set; }
        DbSet<Vehiculo> Vehiculo { get; set; }
        int SaveChanges();
    }

}
