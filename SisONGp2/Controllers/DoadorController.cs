using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisONGp2.Context;
using SisONGp2.Model;
using System.Linq;
using System.Threading.Tasks;

namespace SisONGp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDoadores()
        {
            var doadores = _context.Doadores.ToList();
            return Ok(doadores);
        }

        [HttpGet("{id}")]
        public IActionResult GetDoador(int id)
        {
            var doador = _context.Doadores.Find(id);
            if (doador == null) return NotFound();
            return Ok(doador);
        }

        [HttpPost]
        public IActionResult PostDoador(Doador doador)
        {
            _context.Doadores.Add(doador);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDoador), new { id = doador.DoadorId }, doador);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Busca o doador pelo username e senha
            var usuario = _context.Doadores
                .FirstOrDefault(u => u.Username == loginRequest.Username && u.Senha == loginRequest.Senha);

            if (usuario == null)
            {
                return Unauthorized(new { mensagem = "Usuário ou senha inválidos" });
            }

            // Retorna os dados do doador logado (se necessário)
            return Ok(new
            {
                mensagem = "Login realizado com sucesso",
                doador = new
                {
                    usuario.DoadorId,
                    usuario.NomeDoador,
                    usuario.Username,
                    usuario.EmailDoador
                }
            });
        }


        [HttpPut("{id}")]
        public IActionResult PutDoador(int id, [FromBody] Doador doador)
        {
            doador.DoadorId = id;

            _context.Entry(doador).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoador(int id)
        {
            var doador = _context.Doadores.Find(id);
            if (doador == null) return NotFound();

            _context.Doadores.Remove(doador);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
