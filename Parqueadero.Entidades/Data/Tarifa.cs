using System.ComponentModel.DataAnnotations;

namespace Parqueadero.Entidades.Data
{
    public class Tarifa
    {
        [Key]
        public int TarifaId { get; set; }
        public decimal TarifaValor { get; set; }
        public int TipoVehiculoId { get; set; }
    }
}