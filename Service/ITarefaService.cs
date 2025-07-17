using System;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Service;

public interface ITarefaService
{
    IEnumerable<Tarefa> GetAll(int pageNumber, int pageSize);
    Tarefa GetId(int id);
    IEnumerable<Tarefa> GetTitle(string title);
    IEnumerable<Tarefa> GetDate(DateTime date);
    IEnumerable<Tarefa> GetStatus(EnumStatusTarefa status);
    void Add(Tarefa tarefa);
    void Update(Tarefa tarefa);
    void Delete(Tarefa tarefa);

}
