

using System.ComponentModel.DataAnnotations;

namespace DataAcceseLayer.Entities;

public class BaseEntity
{
    [Key, Required]
    public int Id { get; set; }
}
