

using System.ComponentModel.DataAnnotations;

namespace DataAcceseLayer.Entities;

public class BaseEntity
{
    [Required, Key]
    public int Id { get; set; }
}
