using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Service;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController(ITarefaService service) : ControllerBase
    {
        private readonly ITarefaService _service = service;

        [HttpGet("{id}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            var tarefa = _service.GetId(id);
            if (tarefa != null)
            {
                return Ok(tarefa);
            }
            else
            {
                return NotFound();
            }

            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
        }

        [HttpGet("ObterTodos")]
        public ActionResult<IEnumerable<Tarefa>> ObterTodos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) return BadRequest("O número de página deve ser maior que 0.");

            if (pageSize < 1) return BadRequest("O tamanho da página deve ser maior que 0.");

            var tarefas = _service.GetAll(pageNumber, pageSize);
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo/{titulo}")]
        public IActionResult ObterPorTitulo([FromRoute] string titulo)
        {
            if (string.IsNullOrEmpty(titulo)) return BadRequest("O titulo da tarefa é obrigatorio.");

            var tarefa = _service.GetTitle(titulo);

            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData/{data}")]
        public IActionResult ObterPorData([FromRoute] DateTime data)
        {
            var tarefa = _service.GetDate(data);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus/{status}")]
        public IActionResult ObterPorStatus([FromRoute] EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _service.GetStatus(status);
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Tarefa tarefa)
        {

            Console.WriteLine($"Recebido: {tarefa.Titulo}, {tarefa.Data}");
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _service.Add(tarefa);
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromRoute] int id, [FromBody] Tarefa tarefa)
        {
            var tarefaBanco = _service.GetId(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _service.Update(tarefaBanco);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            var tarefaBanco = _service.GetId(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _service.Delete(tarefaBanco);
            return NoContent();
        }
    }
}
