using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Parqueadero.AccesoDatos.Repository.IRepository
{
    public interface IRepositoryTipoVehiculo
    {

        TipoVehiculo Adicionar(TipoVehiculo tipoVehiculo);

        TipoVehiculo Modificar(TipoVehiculo tipoVehiculo);

        IEnumerable<TipoVehiculoDto> Consultar(Expression<Func<TipoVehiculo, bool>> objBusqueda);
    }
}
