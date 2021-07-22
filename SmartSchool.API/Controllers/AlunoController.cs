using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {

        public readonly IRepository _repo;
        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }
        
        [HttpGet("{id}")] //byId -> ?id=1
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("O Aluno não foi encontrado!");

            return Ok(aluno);
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
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges())
                return Ok(aluno);
            return BadRequest("Aluno não cadastrado!");
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, Aluno aluno)
        {
            var al = _repo.GetAlunoById(id, true);
           if(al == null) return BadRequest("Aluno não encontrado");
           _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Ok(aluno);
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
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, true);
           if(alu == null) return BadRequest("Aluno não encontrado");
           _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Ok(aluno);
            return BadRequest("Aluno não atualizado!");
        }
    }
}