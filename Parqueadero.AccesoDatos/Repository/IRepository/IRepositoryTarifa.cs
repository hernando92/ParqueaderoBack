using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Parqueadero.AccesoDatos.Repository.IRepository
{
    public interface IRepositoryTarifa
    {

        Tarifa Adicionar(Tarifa tarifa);

        Tarifa Modificar(Tarifa tarifa);

        IEnumerable<TarifaDto> Consultar(Expression<Func<Tarifa, bool>> objBusqueda);
    }
}
