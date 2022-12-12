using AutoMapper;
using Parqueadero.AccesoDatos.Repository.Data;
using Parqueadero.AccesoDatos.Repository.IRepository;
using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Parqueadero.AccesoDatos.Repository
{
    public class RepositoryTipoVehiculo : IRepositoryTipoVehiculo
    {
        private readonly IContextDatabase _context;
        private readonly IMapper _mapper;

        #region Constructor

        public RepositoryTipoVehiculo(IContextDatabase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public TipoVehiculo Adicionar(TipoVehiculo tipoVehiculo)
        {
            TipoVehiculo objTipoVehiculo = _context.TipoVehiculo.Where(x => x.TipoVehiculoDescripcion == tipoVehiculo.TipoVehiculoDescripcion).FirstOrDefault();
            if (objTipoVehiculo == null)
            {
                _context.TipoVehiculo.Add(tipoVehiculo);
                _context.SaveChanges();

                objTipoVehiculo = _mapper.Map<TipoVehiculo>(tipoVehiculo);
            }

            return objTipoVehiculo;
        }

        public TipoVehiculo Modificar(TipoVehiculo tipoVehiculo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoVehiculoDto> Consultar(Expression<Func<TipoVehiculo, bool>> objBusqueda)
        {
            return _mapper.Map<IEnumerable<TipoVehiculoDto>>(_context.TipoVehiculo.Where(objBusqueda).ToList());
        }
    }
}
