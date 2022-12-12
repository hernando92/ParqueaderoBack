using System;

namespace Parqueadero.Entidades.Data.Dto
{
    public class RegistroVehiculoDto
    {
        public Nullable<int> RegistroVehiculoId { get; set; }
        public Nullable<DateTime> RegistroVehiculoHoraIngreso { get; set; }
        public Nullable<DateTime> RegistroVehiculoHoraSalida { get; set; }
        public Nullable<Boolean> RegistroVehiculoAplicaDescuento { get; set; }
        public Nullable<Double> RegistroVehiculoValorDescuento { get; set; }
        public string RegistroVehiculoFacturaRelacionada { get; set; }
        public Nullable<Double> RegistroVehiculoValorTotal { get; set; }
        public Nullable<int> VehiculoId { get; set; }

        //Consulta
        public Nullable<DateTime> FechaInicio { get; set; }
        public Nullable<DateTime> FechaFin { get; set; }
        public string Placa { get; set; }
        public Nullable<int> TipoVehiculo { get; set; }
        public Nullable<Double> TotalMinutos { get; set; }
    }
}
