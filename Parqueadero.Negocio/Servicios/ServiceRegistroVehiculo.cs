using Parqueadero.AccesoDatos.Repository.IRepository;
using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using Parqueadero.Negocio.Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parqueadero.Negocio.Servicios
{
    public class ServiceRegistroVehiculo : IServiceRegistroVehiculo
    {

        private readonly IRepositoryRegistroVehiculo _repositoryRegistroVehiculo;
        private readonly IRepositoryVehiculo _repositoryVehiculo;
        private readonly IRepositoryTarifa _repositoryTarifa;

        public ServiceRegistroVehiculo(IRepositoryRegistroVehiculo repositoryRegistroVehiculo, IRepositoryVehiculo repositoryVehiculo, IRepositoryTarifa repositoryTarifa)
        {
            _repositoryRegistroVehiculo = repositoryRegistroVehiculo;
            _repositoryVehiculo = repositoryVehiculo;
            _repositoryTarifa = repositoryTarifa;
        }

        public RegistroVehiculo RegistrarIngreso(RegistroVehiculoDto registroVehiculo)
        {
            return _repositoryRegistroVehiculo.Adicionar(registroVehiculo);
        }
        public RegistroVehiculo RegistrarSalida(RegistroVehiculo registroVehiculo)
        {
            return _repositoryRegistroVehiculo.Modificar(registroVehiculo);
        }

        public IEnumerable<RegistroVehiculoDto> ConsultarPorFechas(RegistroVehiculoDto registroVehiculoDto)
        {

            return _repositoryRegistroVehiculo.ConsultarTotal(x => x.RegistroVehiculoHoraIngreso.Date >= registroVehiculoDto.FechaInicio &&
                                                              x.RegistroVehiculoHoraIngreso.Date <= registroVehiculoDto.FechaFin);



        }

        public RegistroVehiculoDto ConsultarPorPlaca(RegistroVehiculoDto registroVehiculoDto)
        {

            //consultar vehiculo
            VehiculoDto vehiculo = _repositoryVehiculo.Consultar(x => x.VehiculoPlaca == registroVehiculoDto.Placa).FirstOrDefault();

            if (vehiculo != null)
            {
                //consultar registro de ingreso
                RegistroVehiculoDto objRegistroVehiculoDto = new RegistroVehiculoDto();
                objRegistroVehiculoDto = _repositoryRegistroVehiculo.Consultar(x => x.VehiculoId == vehiculo.VehiculoId &&
                                             x.RegistroVehiculoHoraSalida == null).FirstOrDefault();

                if (objRegistroVehiculoDto != null)
                {
                    //consultar tarifa del tipo de vehiculo
                    TarifaDto tarifaDto = _repositoryTarifa.Consultar(x => x.TipoVehiculoId == vehiculo.TipoVehiculoId).FirstOrDefault();

                    //liquidar salida
                    bool registraDescuento = !string.IsNullOrEmpty(registroVehiculoDto.RegistroVehiculoFacturaRelacionada);

                    objRegistroVehiculoDto.RegistroVehiculoHoraSalida = DateTime.Now;
                    TimeSpan ts = objRegistroVehiculoDto.RegistroVehiculoHoraSalida.GetValueOrDefault().Subtract(objRegistroVehiculoDto.RegistroVehiculoHoraIngreso.GetValueOrDefault());

                    objRegistroVehiculoDto.RegistroVehiculoAplicaDescuento = registraDescuento;
                    objRegistroVehiculoDto.RegistroVehiculoFacturaRelacionada = registroVehiculoDto.RegistroVehiculoFacturaRelacionada;
                    objRegistroVehiculoDto.TotalMinutos = ts.TotalMinutes;
                    objRegistroVehiculoDto.RegistroVehiculoValorDescuento = (objRegistroVehiculoDto.TotalMinutos * Convert.ToDouble(tarifaDto.TarifaValor)) * (registraDescuento ? Convert.ToDouble(0.3) : 0);
                    objRegistroVehiculoDto.RegistroVehiculoValorTotal = (Convert.ToDouble(ts.TotalMinutes) * Convert.ToDouble(tarifaDto.TarifaValor)) - objRegistroVehiculoDto.RegistroVehiculoValorDescuento;

                    return objRegistroVehiculoDto;
                }
                else
                    throw new InvalidOperationException("vehiculo no encontrado en el parqueadero");
            }

            throw new InvalidOperationException("vehiculo no encontrado");
        }

    }
}
