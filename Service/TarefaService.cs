using System;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Service;

public class TarefaService : ITarefaService
{
    private readonly OrganizadorContext _context;

    public TarefaService(OrganizadorContext context)
    {
        _context = context;
    }
    public void Add(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();
    }

    public void Delete(Tarefa tarefa)
    {
        _context.Tarefas.Remove(tarefa);
        _context.SaveChanges();
    }

    public IEnumerable<Tarefa> GetAll(int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;

        return _context.Tarefas
            .OrderBy(t => t.Id) // Ordena por ID
            .Skip(skip) // Pula itens anteriores
            .Take(pageSize) // Pega o número de itens da página atual
            .ToList();
    }

    public IEnumerable<Tarefa> GetDate(DateTime date)
    {
        return _context.Tarefas
            .Where(t => t.Data.Date == date.Date)
            .OrderBy(t => t.Data)
            .ToList();
            
    }

    public Tarefa GetId(int id)
    {
        return _context.Tarefas.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Tarefa> GetStatus(EnumStatusTarefa status)
    {
        return _context.Tarefas
            .Where(t => t.Status == status)
            .OrderBy(t => t.Id)
            .ToList();
    }

    public IEnumerable<Tarefa> GetTitle(string title)
    {
        return _context.Tarefas
            .Where(t => t.Titulo.Contains(title))
            .OrderBy(t => t.Id)
            .ToList();
    }

    public void Update(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
        _context.SaveChanges();
    }
}
