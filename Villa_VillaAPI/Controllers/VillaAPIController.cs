using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    private readonly ApplicationDBContext _dbContext;
    public VillaAPIController(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas() 
    {
        return Ok(_dbContext.Villas.ToList());
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    public ActionResult<VillaDTO> GetVilla(int id) 
    {
        if (id == 0) {
            return BadRequest();
        }
        var villa = _dbContext.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        return Ok(villa);
    }
    
    [HttpPost]
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO) 
    {
        // if (!ModelState.IsValid) {
        //     return BadRequest(ModelState);
        // }
        
        // Custom Validation Error

        if (_dbContext.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null) 
        {
            ModelState.AddModelError("CustomError", "Villa Already Exist!");
            return BadRequest(ModelState);
        }

        if (villaDTO == null) {
            return BadRequest();
        }

        if (villaDTO.Id > 0) {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        Villa villaModel = new() {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
            Details = villaDTO.Details,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft,
            Amenity = villaDTO.Amenity,
            ImageUrl = villaDTO.ImageUrl
        };

        _dbContext.Villas.Add(villaModel);
        _dbContext.SaveChanges();

        return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public ActionResult<VillaDTO> DeleteVilla(int id) 
    {
        if (id == 0) {
            return BadRequest();
        }
        var villa = _dbContext.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        _dbContext.Villas.Remove(villa);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public ActionResult<VillaDTO> UpdateVilla(int id, [FromBody]VillaDTO villaDTO) 
    {
        if (villaDTO == null || id != villaDTO.Id) {
            return BadRequest();
        }
        var villa = _dbContext.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        
        Villa villaModel = new() {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
            Details = villaDTO.Details,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft,
            Amenity = villaDTO.Amenity,
            ImageUrl = villaDTO.ImageUrl
        };

        _dbContext.Villas.Update(villaModel);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    public ActionResult<VillaDTO> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) 
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }
        var villa = _dbContext.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        VillaDTO villaDTO = new()
        {
            Id = villa.Id,
            Name = villa.Name,
            Details = villa.Details,
            Occupancy = villa.Occupancy,
            Rate = villa.Rate,
            Sqft = villa.Sqft,
            Amenity = villa.Amenity,
            ImageUrl = villa.ImageUrl
        };

        patchDTO.ApplyTo(villaDTO, ModelState);

        Villa villaModel = new() {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
            Details = villaDTO.Details,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft,
            Amenity = villaDTO.Amenity,
            ImageUrl = villaDTO.ImageUrl
        };

        _dbContext.Villas.Update(villaModel);
        _dbContext.SaveChanges();

        return NoContent();
    }
}
