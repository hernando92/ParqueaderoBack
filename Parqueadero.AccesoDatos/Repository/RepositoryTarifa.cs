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
    public class RepositoryTarifa : IRepositoryTarifa
    {

        private readonly IContextDatabase _context;
        private readonly IMapper _mapper;

        #region Constructor

        public RepositoryTarifa(IContextDatabase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public Tarifa Adicionar(Tarifa tarifa)
        {
            Tarifa objTarifa = _context.Tarifa.Where(x => x.TipoVehiculoId == tarifa.TipoVehiculoId).FirstOrDefault();
            if (objTarifa == null)
            {
                _context.Tarifa.Add(tarifa);
                _context.SaveChanges();

                objTarifa = _mapper.Map<Tarifa>(tarifa);
            }

            return objTarifa;
        }

        public Tarifa Modificar(Tarifa tarifa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TarifaDto> Consultar(Expression<Func<Tarifa, bool>> objBusqueda)
        {
            return _mapper.Map<IEnumerable<TarifaDto>>(_context.Tarifa.Where(objBusqueda).ToList());
        }
    }
}
