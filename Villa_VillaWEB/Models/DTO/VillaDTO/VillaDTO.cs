using System;
using System.ComponentModel.DataAnnotations;

namespace Villa_VillaWEB.Models.DTO.VillaDTO;

public class VillaDTO
{
    public int Id { get; set;}
    [Required]
    public string Name { get; set;}
    public int Occupancy {get; set;}
    public int Sqft { get; set; }
    public string Details { get; set; }
    [Required]
    public double Rate { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}
