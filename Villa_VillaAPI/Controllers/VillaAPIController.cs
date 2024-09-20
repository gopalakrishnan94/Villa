using System;
using Microsoft.AspNetCore.Mvc;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas() 
    {
        return VillaStore.villaList;
    }

    [HttpGet("{id:int}")]
    public VillaDTO GetVilla(int id) 
    {
        return VillaStore.villaList.FirstOrDefault(x => x.Id == id);
    }
}
