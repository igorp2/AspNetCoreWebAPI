using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {

        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }
        
        
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }
        

        [HttpGet("{id}")] //byId -> ?id=1
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("O Aluno não foi encontrado!");
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }


        /*[HttpGet("ByName")] // api/aluno/ByName?nome=Igor&sobrenome=Pinheiro
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => 
                a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );
            if(aluno == null) return BadRequest("O Aluno não foi encontrado!");

            return Ok(aluno);
        }*/

        [HttpPost] 
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);
            if(_repo.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            return BadRequest("Aluno não cadastrado!");
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, true);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            _mapper.Map(model,aluno);
            _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            return BadRequest("Aluno não atualizado!");
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, true);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            _repo.Delete(aluno);
            if(_repo.SaveChanges())
                return Ok(aluno);
            return BadRequest("Aluno não deletado!");
        }

        [HttpPatch("{id}")] 
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, true);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            _mapper.Map(model,aluno);
            _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Ok(aluno);
            return BadRequest("Aluno não atualizado!");
        }
    }
}