using System;
using System.ComponentModel.DataAnnotations;

namespace Villa_VillaAPI.Models.DTO;

public class UpdateVillaDTO
{
    [Required]
    public int Id { get; set;}
    [Required]
    public string Name { get; set;}
    [Required]
    public int Occupancy {get; set;}
    [Required]
    public int Sqft { get; set; }
    [Required]
    public string Details { get; set; }
    [Required]
    public double Rate { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}
