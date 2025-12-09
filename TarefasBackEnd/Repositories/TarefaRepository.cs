using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories
{
    public interface ITarefaRepository
    {
        List<Tarefa> Read();
        void Create(Tarefa tarefa);
        Task Delete(Guid id);
        void Update(Guid id, Tarefa tarefa);
    }

    public class TarefaRepository : ITarefaRepository
    {
        private readonly DataContext _context;
        public TarefaRepository(DataContext context)
        {
            _context = context;
        }
        public void Create(Tarefa tarefa)
        {
            tarefa.Id = Guid.NewGuid();
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
        
        public List<Tarefa> Read()
        {
            return _context.Tarefas.ToList();
        }

        public void Update(Guid id, Tarefa tarefa)
        {
            var _tarefa = _context.Tarefas.Find(id);

            if (_tarefa == null)
                return;

            _tarefa.Nome = tarefa.Nome;
            _tarefa.Concluida = tarefa.Concluida;
            
            _context.Tarefas.Update(_tarefa);
            _context.SaveChanges();
        }
    }
}