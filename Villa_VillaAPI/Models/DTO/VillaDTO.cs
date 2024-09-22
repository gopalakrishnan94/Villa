using System;
using System.ComponentModel.DataAnnotations;

namespace Villa_VillaAPI.Models.DTO;

public class VillaDTO
{
    public int Id { get; set;}
    [Required]
    public string Name { get; set;}
}
