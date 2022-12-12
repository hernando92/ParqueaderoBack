using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Parqueadero.AccesoDatos.Repository.IRepository
{
    public interface IRepositoryRegistroVehiculo
    {

        RegistroVehiculo Adicionar(RegistroVehiculoDto registroVehiculo);

        RegistroVehiculo Modificar(RegistroVehiculo registroVehiculo);

        IEnumerable<RegistroVehiculoDto> Consultar(Expression<Func<RegistroVehiculo, bool>> objBusqueda);

        IEnumerable<RegistroVehiculoDto> ConsultarTotal(Expression<Func<RegistroVehiculo, bool>> objBusqueda);

    }
}
