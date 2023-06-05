using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _context.Categorias.ToList();
            if(categorias is null)
            {
                return NotFound("Lista de categorias não encontrada!");
            }

            return categorias;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            if(id  == 0) return BadRequest("Identificador Inválido!");
         
            var cat = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (cat is null) return NotFound("Categoria não encontrada!");

            return cat;
        }

        [HttpPost]
        public ActionResult Post(Categoria c)
        {
            if (c is null) return BadRequest("Erro ao inserir categoria");

            _context.Categorias.Add(c);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = c.Id }, c);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria c)
        {
            if (id != c.Id) return BadRequest();

            _context.Entry(c).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(c);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria is null) return NotFound("Categoria não encontrada!");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
