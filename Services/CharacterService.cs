using AutoMapper;
using DotnetCoreWebApiProject.Data;
using DotnetCoreWebApiProject.Dtos;
using DotnetCoreWebApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreWebApiProject.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context; 
        }

        private static List<Character> _characterList = new List<Character>()
        {
            new Character(),
            new Character{ Id  =1, Name = "Paul"  },
        };

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            //var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            //Character character = _mapper.Map<Character>(newCharacter);
            //character.Id = _characterList.Max(c => c.Id) + 1;
            //_characterList.Add(character);
            //serviceResponse.Data = _characterList.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            //return serviceResponse;

            var response = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            response.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            response.Message = "New character added successfully";
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            //return new ServiceResponse<List<GetCharacterDto>>
            //{
            //    Data = _characterList.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            //};

            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacter = await  _context.Characters.ToListAsync();
            response.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
            
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            //var serviceResponse = new ServiceResponse<GetCharacterDto>();
            //var character = _characterList.FirstOrDefault(c => c.Id == id);
            //serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            //return serviceResponse;

            var response = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            //ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            //try
            //{
            //    Character character = _characterList.FirstOrDefault(c => c.Id == updateCharacter.Id);

            //    _mapper.Map(updateCharacter, character);
            //    //character.Name = updateCharacter.Name;
            //    //character.HitPoints = updateCharacter.HitPoints;
            //    //character.Strength = updateCharacter.Strength;
            //    //character.Defense = updateCharacter.Defense;
            //    //character.Intelligence = updateCharacter.Intelligence;
            //    //character.Class = updateCharacter.Class;

            //    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            //}
            //catch(Exception ex)
            //{
            //    serviceResponse.Success = false;
            //    serviceResponse.Message = ex.Message;
            //}

            //return serviceResponse;

            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                _mapper.Map(updateCharacter, character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            //ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            //try
            //{
            //    Character character = _characterList.First(c => c.Id == id);
            //    _characterList.Remove(character);
            //    response.Data = _characterList.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            //}
            //catch(Exception ex)
            //{
            //    response.Success = false;
            //    response.Message = ex.Message;
            //}
            //return response;

            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                _characterList.Remove(character);
                await _context.SaveChangesAsync();
                response.Data = _characterList.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
