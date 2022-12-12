using System.ComponentModel.DataAnnotations;

namespace Parqueadero.Entidades.Data
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }

        public string VehiculoPlaca { get; set; }

        public int TipoVehiculoId { get; set; }

    }
}
