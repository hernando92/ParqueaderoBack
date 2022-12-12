using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;
using Parqueadero.Negocio.Servicios.IServicios;
using System;

namespace Parqueadero.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroVehiculoController : ControllerBase
    {

        private readonly IServiceRegistroVehiculo _services;
        private readonly IMapper _mapper;

        public RegistroVehiculoController(IServiceRegistroVehiculo serviceRegistroVehiculo, IMapper mapper)
        {
            _services = serviceRegistroVehiculo;
            _mapper = mapper;
        }

        [HttpGet("ConsultarPorPlaca")]
        public IActionResult ConsultarPorPlaca([FromQuery] RegistroVehiculoDto registroVehiculoDto)
        {
            try
            {
                return Ok(_services.ConsultarPorPlaca(registroVehiculoDto));
            }
            catch (InvalidOperationException ex)
            {
                return Problem(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("ConsultarPorFechas")]
        public IActionResult ConsultarPorFechas([FromQuery] RegistroVehiculoDto registroVehiculoDto)
        {
            return Ok(_services.ConsultarPorFechas(registroVehiculoDto));
        }

        [HttpPost("RegistrarIngreso")]
        public IActionResult RegistrarIngreso([FromBody] RegistroVehiculoDto registroVehiculoDto)
        {
            try
            {
                return Ok(_services.RegistrarIngreso(registroVehiculoDto));
            }
            catch (InvalidOperationException ex)
            {
                return Problem(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("RegistrarSalida")]
        public IActionResult RegistrarSalida([FromBody] RegistroVehiculoDto registroVehiculoDto)
        {
            try
            {
                return Ok(_services.RegistrarSalida(_mapper.Map<RegistroVehiculo>(registroVehiculoDto)));
            }
            catch (InvalidOperationException ex)
            {
                return Problem(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


    }
}
