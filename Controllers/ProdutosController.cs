using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null) return BadRequest();
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            /* Retorna o objeto criado como uma rota de um Get by Id, com um novo id inserido.*/
            return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.Id }, produto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        { 
            var produtos = _context.Produtos.ToList();
            if(produtos is null)
            {
                return NotFound("Produtos não encontrados!");
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            // FirstOrDefault -> Procura o primeiro registro que obedece a condição
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if(produto is null)
            {
                return NotFound("Produto não encontrado!");
            }

            return produto;
        }
        
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto p)
        {
            if (id != p.Id) return BadRequest();

            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(p);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        { 
            //var produto = _context.Produtos.Find(id); 
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
           
            if (produto is null) 
            {
                return NotFound("Produto não localizado!");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);
        }
    }
}
