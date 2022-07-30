using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreWebApiProject.Models;
using DotnetCoreWebApiProject.Services;
using DotnetCoreWebApiProject.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DotnetCoreWebApiProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        
        private ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [HttpGet("GetAll")]       
        public async Task<IActionResult> Get()
        {

            return Ok(await characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddCharacterDto addCharacterDto)
        {
            return Ok(await characterService.AddCharacter(addCharacterDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterDto updateCharacterDto)
        {
            var response = await characterService.UpdateCharacter(updateCharacterDto);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await characterService.DeleteCharacter(id);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
    }
}
