using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using System.Collections.Generic;

namespace Parqueadero.Negocio.Servicios.IServicios
{
    public interface IServiceRegistroVehiculo
    {

        RegistroVehiculo RegistrarIngreso(RegistroVehiculoDto registroVehiculo);

        RegistroVehiculo RegistrarSalida(RegistroVehiculo registroVehiculo);

        IEnumerable<RegistroVehiculoDto> ConsultarPorFechas(RegistroVehiculoDto registroVehiculoDto);

        RegistroVehiculoDto ConsultarPorPlaca(RegistroVehiculoDto registroVehiculoDto);

    }
}
