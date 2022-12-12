using System;
using System.ComponentModel.DataAnnotations;

namespace Parqueadero.Entidades.Data
{
    public class RegistroVehiculo
    {
        [Key]
        public int RegistroVehiculoId { get; set; }
        public DateTime RegistroVehiculoHoraIngreso { get; set; }
        public Nullable<DateTime> RegistroVehiculoHoraSalida { get; set; }
        public Nullable<Boolean> RegistroVehiculoAplicaDescuento { get; set; }
        public Nullable<Double> RegistroVehiculoValorDescuento { get; set; }
        public string RegistroVehiculoFacturaRelacionada { get; set; }
        public Nullable<Double> RegistroVehiculoValorTotal { get; set; }
        public int VehiculoId { get; set; }
    }
}
