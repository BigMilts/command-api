using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;

        public IMapper _mapper { get; }

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            IEnumerable<Command> commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
            
        }
         //GET api/commands/{id}
        [HttpGet("{id}", Name="getCommandByID")]
        public ActionResult <CommandReadDTO> getCommandByID(int id )
        {
            
            Command commandItem = _repository.GetCommandByID(id);
            if(commandItem != null) {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));
            }
            return NotFound();
            
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDTO> createCommand(CommandCreateDTO commandCreateDTO)
        {
            Command commandModel = _mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(commandModel);
            _repository.saveChanges();
            CommandReadDTO commandReadDto = _mapper.Map<CommandReadDTO>(commandModel);

            return CreatedAtRoute(nameof(getCommandByID), new{Id = commandReadDto.Id}, commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult updateCommand(int id, CommandUpdateDTO commandUpdate)
        {
            Command commandFromRepo = _repository.GetCommandByID(id);
            if(commandFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdate, commandFromRepo);

            _repository.UpdateCommand(commandFromRepo);

            _repository.saveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch]
        [Route("{id}")]
        public ActionResult partialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDTO> patchDocument)
        {
            Command commandFromRepo = _repository.GetCommandByID(id);
            if(commandFromRepo == null)
            {
                return NotFound();
            }

            CommandUpdateDTO commandToPatch = _mapper.Map<CommandUpdateDTO>(commandFromRepo);
            patchDocument.ApplyTo(commandToPatch, ModelState);
            
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            
            _mapper.Map(commandToPatch, commandFromRepo);
             
            _repository.UpdateCommand(commandFromRepo);
              
            _repository.saveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete]
        [Route("{id}")]
        public ActionResult deleteCommand(int id)
        {
            Command commandFromRepo = _repository.GetCommandByID(id);
            if(commandFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandFromRepo);
            
            _repository.saveChanges();

            return NoContent();
        }
    }
}