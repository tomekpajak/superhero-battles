using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shb.Api.Wrappers;
using Shb.Application.DTOs;
using Shb.Application.Services.Abstraction;
using Shb.Application.Validators;
using Shb.Domain.Abstractions;

namespace Shb.Api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Route("api/v{version:apiVersion}/superheroes")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class SuperheroController : ControllerBase
    {
        private readonly ISuperheroService superheroService;
        private readonly IAppLogger<SuperheroController> logger;

        public SuperheroController(ISuperheroService superheroService, IAppLogger<SuperheroController> logger)
        {
            this.superheroService = superheroService;
            this.logger = logger;
        }

        // GET api/v1.0/superheroes/1
        /// <summary>
        /// Get superheroes
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [ProducesResponseType(typeof(ResponsePaginated<IReadOnlyList<SuperheroDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]  
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetListAsync([FromQuery] RequestPagination request) 
        {
            var dtos = await this.superheroService.ListPaginatedAsync(request.PageNumber, request.PageSize);
            if (dtos == null)
                return NotFound();

            var count = await this.superheroService.CountAllAsync();

            return Ok(CreateResponsePaginated(dtos, count, request.PageSize, request.PageNumber));
        }     

        // GET api/v1.0/superheroes/1
        /// <summary>
        /// Get superheroes by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        [ProducesResponseType(typeof(Response<SuperheroDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]  
        [ProducesDefaultResponseType]      
        public async Task<IActionResult> GetByIdAsync(int id) 
        {
            SuperheroDTO dto = await this.superheroService.SingleByIdAsync(id);
            if (dto == null) 
                return NotFound();

            return Ok(new Response<SuperheroDTO>(dto));
        }
        
        // POST api/v1.0/superheroes
        /// <summary>
        /// Add superhero
        /// </summary>
        /// <param name="superheroDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<SuperheroDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        [ProducesDefaultResponseType]          
        public async Task<IActionResult> PostAsync([FromBody] SuperheroDTO superheroDTO) 
        {
            if (superheroDTO == null)
                return BadRequest(new ArgumentNullException());

            var dto = await this.superheroService.CreateAsync(superheroDTO);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = dto.Id }, new Response<SuperheroDTO>(dto));
        }

        // PUT api/v1.0/superheroes/1
        /// <summary>
        /// Edit superhero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="superheroDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]      
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]      
        public async Task<IActionResult> PutAsync(int id, [FromBody] SuperheroDTO superheroDTO) 
        {
            if (id != superheroDTO.Id)
                return BadRequest();

            if (!await this.superheroService.ExistsAsync(id))
                return NotFound();
            
            await this.superheroService.UpdateAsync(superheroDTO);

            return NoContent();
        }

        // DELETE api/v1.0/superheroes/1
        /// <summary>
        /// Deletes superhero
        /// </summary>
        /// <returns></returns>        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            if (!await this.superheroService.ExistsAsync(id))
                return NotFound();

            await this.superheroService.DeleteAsync(id);

            return NoContent();
        }

        // GET api/v1.0/superheroes/random
        /// <summary>
        /// Get random uperhero
        /// </summary>
        /// <returns>
        /// SuperheroDTO
        /// </returns>        
        [ProducesResponseType(typeof(Response<SuperheroDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("random")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetRandomAsync() 
        {
            var dto = await this.superheroService.SingleRandomAsync();
            if (dto == null) 
                return NotFound();

            return Ok(new Response<SuperheroDTO>(dto));
        }

        private ResponsePaginated<IReadOnlyList<SuperheroDTO>> CreateResponsePaginated (IReadOnlyList<SuperheroDTO> data, int count, int pageSize, int pageNumber) 
        {
            var totalPages = (double)count / (double)pageSize;
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            return new ResponsePaginated<IReadOnlyList<SuperheroDTO>>(data, pageNumber, roundedTotalPages);
        }  
    }
}
