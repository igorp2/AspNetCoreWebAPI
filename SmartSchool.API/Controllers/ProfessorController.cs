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
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }
        
        [HttpGet("{id:int}")] //byId -> ?id=1
        public IActionResult GetById(int id)
        {
            var aluno = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("O Professor não foi encontrado!");

            return Ok(aluno);
        }

        [HttpGet("{nome}")]
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
        }

        [HttpPost] 
        public IActionResult Post(Professor aluno)
        {
           _context.Add(aluno);
           _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, Professor aluno)
        {
            var al = _context.Professores.FirstOrDefault(a => a.Id == id);
           if(al == null) return BadRequest("Professor não encontrado");
           _context.Update(aluno);
           _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            var aluno = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
           if(aluno == null) return BadRequest("Professor não encontrado");
           _context.Remove(aluno);
           _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{id}")] 
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
           if(alu == null) return BadRequest("Professor não encontrado");
           _context.Update(aluno);
           _context.SaveChanges();
            return Ok();
        }
    }
}