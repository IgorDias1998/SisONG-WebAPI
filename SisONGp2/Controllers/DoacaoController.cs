using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisONGp2.Context;
using SisONGp2.DTO;
using SisONGp2.Model;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SisONGp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoacaoController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para listar todas as doações
        [HttpGet]
        public IActionResult GetDoacoes()
        {
            var doacoes = _context.Doacoes
                .Join(_context.Doadores,
                      doacao => doacao.DoadorId,
                      doador => doador.DoadorId,
                      (doacao, doador) => new DoacaoDTO
                      {
                          DoacaoId = doacao.DoacaoId,
                          ValorDoacao = doacao.ValorDoacao,
                          NomeDoador = doador.NomeDoador,
                          DataDoacao = doacao.DataDoacao.ToString("dd/MM/yyyy")
                      })
                .ToList();

            return Ok(doacoes);
        }


        // Endpoint para listar todas as doações do doador logado
        [HttpGet("minhas-doacoes/{doadorId}")]
        public IActionResult GetMinhasDoacoes(int doadorId)
        {
            // Filtra doações pelo ID do doador fornecido
            var doacoes = _context.Doacoes
                .Where(d => d.DoadorId == doadorId)
                .ToList();

            if (!doacoes.Any())
            {
                return NotFound(new { mensagem = "Nenhuma doação encontrada para este doador." });
            }

            return Ok(doacoes);
        }

        // Endpoint para criar uma nova doação
        [HttpPost]
        public IActionResult PostDoacao([FromBody] Doacao doacao)
        {
            // Verifica se o DoadorId está correto
            var doador = _context.Doadores.Find(doacao.DoadorId);
            if (doador == null)
            {
                return NotFound(new { mensagem = "Doador não encontrado." });
            }

            doacao.DataDoacao = DateTime.Now;

            _context.Doacoes.Add(doacao);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMinhasDoacoes), new { doadorId = doacao.DoadorId }, doacao);
        }

        // Endpoint para obter os detalhes de uma doação específica
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoacao(int id)
        {
            var doacao = await _context.Doacoes.FindAsync(id);
            if (doacao == null) return NotFound();

            return Ok(doacao);
        }
    }
}
