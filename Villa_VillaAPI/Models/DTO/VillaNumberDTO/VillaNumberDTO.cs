using System;
using System.ComponentModel.DataAnnotations;

namespace Villa_VillaAPI.Models.DTO.VillaNumberDTO;

public class VillaNumberDTO
{
    [Required]
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; }
}
