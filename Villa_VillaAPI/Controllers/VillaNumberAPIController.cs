using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO.VillaNumberDTO;
using Villa_VillaAPI.Repository.IRepository;

namespace Villa_VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaNumberAPIController : ControllerBase
{
    private readonly IVillaNumberRepository _dbVillaNumber;
    private readonly IMapper _mapper;
    protected APIResponse _response;
    public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper)
    {
        _dbVillaNumber = dbVillaNumber;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetVillaNumbers() 
    {
        try {
            IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();
            _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

    [HttpGet("{id:int}", Name = "GetVillaNumber")]
    public async Task<ActionResult<APIResponse>> GetVillaNumber(int id) 
    {
        try {
            if (id == 0) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
            if (villaNumber == null) {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody]CreateVillaNumberDTO createDTO) 
    {
        try {
            // if (!ModelState.IsValid) {
            //     return BadRequest(ModelState);
            // }
            
            // Custom Validation Error

            if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null) 
            {
                ModelState.AddModelError("CustomError", "Villa Number Already Exist!");
                _response.Result = ModelState;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            if (createDTO == null) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            VillaNumber villaNumberModel = _mapper.Map<VillaNumber>(createDTO);

            await _dbVillaNumber.CreateAsync(villaNumberModel);

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumberModel);
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetVillaNumber", new { id = villaNumberModel.VillaNo }, _response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
    public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id) 
    {
        try {
            if (id == 0) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
            if (villaNumber == null) {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            await _dbVillaNumber.RemoveAsync(villaNumber);

            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

    [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
    public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody]UpdateVillaNumberDTO updateDTO) 
    {
        try {
            if (updateDTO == null || id != updateDTO.VillaNo) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id, tracked: false);
            if (villaNumber == null) {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            VillaNumber villaNumberModel = _mapper.Map<VillaNumber>(updateDTO);

            await _dbVillaNumber.UpdateAsync(villaNumberModel);

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumberModel);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

}
