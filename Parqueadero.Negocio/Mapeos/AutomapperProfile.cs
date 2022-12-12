using AutoMapper;
using Parqueadero.Entidades.Data;
using Parqueadero.Entidades.Data.Dto;

namespace Parqueadero.Negocio.Mapeos
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<RegistroVehiculo, RegistroVehiculoDto>().ReverseMap();
            CreateMap<Vehiculo, VehiculoDto>().ReverseMap();
            CreateMap<Tarifa, TarifaDto>().ReverseMap();
        }
    }
}
