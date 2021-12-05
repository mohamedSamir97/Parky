using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<NationalParkDto>))]

        public IActionResult GetNationalParks()
        {
            var objList = _nationalParkRepository.GetNationalParks();
            var objDto = new List<NationalParkDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual national park
        /// </summary>
        /// <param name="nationalParkId"> The id of the national park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}",Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _nationalParkRepository.GetNationalPark(nationalParkId);
            if (obj == null)
            {

                return NotFound();
            }

            var objDto = _mapper.Map<NationalParkDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [ProducesDefaultResponseType]
        public IActionResult CreateNAtionalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);

            }

            

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_nationalParkRepository.CreateNationlPark(nationalParkObj))
            {
                ModelState.AddModelError("",$"Something went wrong when saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark",new {version=HttpContext.GetRequestedApiVersion().ToString(),
                                                                            nationalParkId=nationalParkObj.Id},nationalParkObj);
        }
        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.UpdateNationlPark(nationalParkObj))
            {
                ModelState.AddModelError("",$"Something went wrong when updating the record{nationalParkObj.Name}");
                return StatusCode(50, ModelState);

            }

            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepository.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }

            var nationalParkObj =_nationalParkRepository.GetNationalPark(nationalParkId) ;
            if (!_nationalParkRepository.DeleteNationlPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record{nationalParkObj.Name}");
                return StatusCode(50, ModelState);

            }

            return NoContent();
        }


    }
    
}
