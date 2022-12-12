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
    public class RepositoryVehiculo : IRepositoryVehiculo
    {

        private readonly IContextDatabase _context;
        private readonly IMapper _mapper;

        #region Constructor

        public RepositoryVehiculo(IContextDatabase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        #endregion

        public Vehiculo Adicionar(Vehiculo vehiculo)
        {

            Vehiculo objVehiculo = _context.Vehiculo.Where(x => x.VehiculoPlaca == vehiculo.VehiculoPlaca).FirstOrDefault();
            if (objVehiculo == null)
            {
                _context.Vehiculo.Add(vehiculo);
                _context.SaveChanges();

                objVehiculo = _mapper.Map<Vehiculo>(vehiculo);
            }

            return objVehiculo;
        }

        public IEnumerable<VehiculoDto> Consultar(Expression<Func<Vehiculo, bool>> objBusqueda)
        {
            return _mapper.Map<IEnumerable<VehiculoDto>>(_context.Vehiculo.Where(objBusqueda).ToList());
        }

        public Vehiculo Modificar(Vehiculo vehiculo)
        {
            throw new NotImplementedException();
        }
    }
}
