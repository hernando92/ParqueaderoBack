using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Parqueadero.AccesoDatos.Repository.IRepository
{
    public interface IRepositoryVehiculo
    {
        Vehiculo Adicionar(Vehiculo vehiculo);

        Vehiculo Modificar(Vehiculo vehiculo);

        IEnumerable<VehiculoDto> Consultar(Expression<Func<Vehiculo, bool>> objBusqueda);
    }
}
