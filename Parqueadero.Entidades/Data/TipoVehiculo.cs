using System.ComponentModel.DataAnnotations;

namespace Parqueadero.Entidades.Data
{
    public class TipoVehiculo
    {
        [Key]
        public int TipoVehiculoId { get; set; }
        public string TipoVehiculoDescripcion { get; set; }

    }
}
