using System;
using System.ComponentModel.DataAnnotations;

namespace Villa_VillaWEB.Models.DTO.VillaNumberDTO;

public class VillaNumberDTO
{
    [Required]
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; }
    [Required]
    public int VillaID { get; set; }
}
