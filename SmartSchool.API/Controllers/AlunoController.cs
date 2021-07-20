using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Igor",
                Sobrenome = "Pinheiro",
                Telefone = "33987302643"
            },
            new Aluno(){
                Id = 2,
                Nome = "João",
                Sobrenome = "Guedes",
                Telefone = "33987302644"
            },
            new Aluno(){
                Id = 3,
                Nome = "Laura",
                Sobrenome = "Zanchin",
                Telefone = "33987302645"
            }
        };
        public AlunoController(){}

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }
        
        [HttpGet("{id:int}")] //byId -> ?id=1
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("O Aluno não foi encontrado!");

            return Ok(aluno);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByNome(string nome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome));
            if(aluno == null) return BadRequest("O Aluno não foi encontrado!");

            return Ok(aluno);
        }

        [HttpGet("ByName")] // api/aluno/ByName?nome=Igor&sobrenome=Pinheiro
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => 
                a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );
            if(aluno == null) return BadRequest("O Aluno não foi encontrado!");

            return Ok(aluno);
        }

        [HttpPost] 
        public IActionResult Post(Aluno aluno)
        {
           
            return Ok(aluno);
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, Aluno aluno)
        {
           
            return Ok(aluno);
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
           
            return Ok();
        }

        [HttpPatch("{id}")] 
        public IActionResult Patch(int id, Aluno aluno)
        {
           
            return Ok();
        }
    }
}