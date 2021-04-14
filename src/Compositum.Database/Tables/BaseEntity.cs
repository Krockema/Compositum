using System.ComponentModel.DataAnnotations.Schema;

namespace Compositum.Database.Tables
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}