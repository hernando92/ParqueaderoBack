using AutoMapper;
using Parqueadero.AccesoDatos.Repository.Data;
using Parqueadero.AccesoDatos.Repository.IRepository;
using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace Parqueadero.AccesoDatos.Repository
{
    public class RepositoryRegistroVehiculo : IRepositoryRegistroVehiculo
    {

        private readonly IContextDatabase _context;
        private readonly IMapper _mapper;

        #region Constructor

        public RepositoryRegistroVehiculo(IContextDatabase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public RegistroVehiculo Adicionar(RegistroVehiculoDto registroVehiculo)
        {

            RegistroVehiculo objRegistroVehiculo = new RegistroVehiculo();
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(10), TransactionScopeAsyncFlowOption.Enabled))
            {

                //Consultar Vehiculo, si no existe se crea
                Vehiculo vehiculo = _context.Vehiculo.Where(x => x.VehiculoPlaca == registroVehiculo.Placa).FirstOrDefault();
                if (vehiculo is null)
                {
                    vehiculo = new Vehiculo
                    {
                        TipoVehiculoId = registroVehiculo.TipoVehiculo.GetValueOrDefault(),
                        VehiculoPlaca = registroVehiculo.Placa
                    };

                    _context.Vehiculo.Add(vehiculo);
                    _context.SaveChanges();
                }

                objRegistroVehiculo = _context.RegistroVehiculo.Where(x => x.VehiculoId == vehiculo.VehiculoId && x.RegistroVehiculoHoraSalida == null).FirstOrDefault();
                if (objRegistroVehiculo == null)
                {
                    objRegistroVehiculo = new RegistroVehiculo();
                    objRegistroVehiculo = _mapper.Map<RegistroVehiculo>(registroVehiculo);

                    objRegistroVehiculo.VehiculoId = vehiculo.VehiculoId;
                    objRegistroVehiculo.RegistroVehiculoHoraIngreso = DateTime.Now;
                    _context.RegistroVehiculo.Add(objRegistroVehiculo);
                    _context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("El vehiculo esta dentro del parqueadero");
                }

                //Confirmar cambios
                scope.Complete();
            }
            return objRegistroVehiculo;

        }

        public RegistroVehiculo Modificar(RegistroVehiculo registroVehiculo)
        {
            RegistroVehiculo objRegistroVehiculo = _context.RegistroVehiculo.Where(x => x.RegistroVehiculoId == registroVehiculo.RegistroVehiculoId && x.RegistroVehiculoHoraSalida == null).FirstOrDefault();
            if (objRegistroVehiculo == null)
            {
                throw new InvalidOperationException("Movimiento no realizado");
            }
            else
            {
                objRegistroVehiculo.RegistroVehiculoHoraSalida = registroVehiculo.RegistroVehiculoHoraSalida;
                objRegistroVehiculo.RegistroVehiculoAplicaDescuento = registroVehiculo.RegistroVehiculoAplicaDescuento;
                objRegistroVehiculo.RegistroVehiculoValorDescuento = registroVehiculo.RegistroVehiculoValorDescuento;
                objRegistroVehiculo.RegistroVehiculoFacturaRelacionada = registroVehiculo.RegistroVehiculoFacturaRelacionada;
                objRegistroVehiculo.RegistroVehiculoValorTotal = registroVehiculo.RegistroVehiculoValorTotal;

                _context.SaveChanges();
            }

            return objRegistroVehiculo;
        }

        public IEnumerable<RegistroVehiculoDto> Consultar(Expression<Func<RegistroVehiculo, bool>> objBusqueda)
        {
            return _mapper.Map<IEnumerable<RegistroVehiculoDto>>(_context.RegistroVehiculo.Where(objBusqueda).ToList());
        }

        public IEnumerable<RegistroVehiculoDto> ConsultarTotal(Expression<Func<RegistroVehiculo, bool>> objBusqueda)
        {
            IEnumerable<RegistroVehiculoDto> res = _context.RegistroVehiculo.Where(objBusqueda).Join(
                                                     _context.Vehiculo,
                                                     regVehiculo => regVehiculo.VehiculoId,
                                                     vehiculo => vehiculo.VehiculoId,
                                                     (regVehiculo, vehiculo) => new RegistroVehiculoDto
                                                     {
                                                         Placa = vehiculo.VehiculoPlaca,
                                                         VehiculoId = vehiculo.VehiculoId
                                                     }
                                                     ).ToList();

            return res;

        }

    }
}
