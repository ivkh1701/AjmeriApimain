using System.ComponentModel.DataAnnotations.Schema;

namespace Ajmera_Core.Domain
{
    public partial class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
