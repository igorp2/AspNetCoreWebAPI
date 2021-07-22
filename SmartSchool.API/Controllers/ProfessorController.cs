using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }
        
        [HttpGet("{id}")] //byId -> ?id=1
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null) return BadRequest("O Professor não foi encontrado!");

            return Ok(professor);
        }

        /*[HttpGet("{nome}")]
        public IActionResult GetByNome(string nome)
        {
            var aluno = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if(aluno == null) return BadRequest("O Professor não foi encontrado!");

            return Ok(aluno);
        }

        [HttpGet("ByName")] // api/aluno/ByName?nome=Igor
        public IActionResult GetByName(string nome)
        {
            var aluno = _context.Professores.FirstOrDefault(a => 
                a.Nome.Contains(nome)
            );
            if(aluno == null) return BadRequest("O Professor não foi encontrado!");

            return Ok(aluno);
        }*/

        [HttpPost] 
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if(_repo.SaveChanges())
                return Ok(professor);
            return BadRequest("Aluno não cadastrado!");
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id, false);
           if(prof == null) return BadRequest("Professor não encontrado");
           _repo.Update(professor);
           if(_repo.SaveChanges())
                return Ok(professor);
            return BadRequest("Professor não atualizado!");
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
           if(professor == null) return BadRequest("Professor não encontrado");
           _repo.Delete(professor);
            if(_repo.SaveChanges())
                return Ok(professor);
            return BadRequest("Professor não deletado!");
        }

        [HttpPatch("{id}")] 
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id, false);
           if(prof == null) return BadRequest("Professor não encontrado");
           _repo.Update(professor);
           if(_repo.SaveChanges())
                return Ok(professor);
            return BadRequest("Professor não atualizado!");
        }
    }
}