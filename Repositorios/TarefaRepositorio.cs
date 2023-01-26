using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private  readonly SistemaTarefasDBContext _dbContext;

        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }

        //Buscar por ID
        public async Task<TarefaModel> BuscarId(int id)
        {
            return await _dbContext.Tarefa
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        //BuscarTarefas
        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefa
                 .Include(x => x.Usuario)
                .ToListAsync();
        }

        //Adicionar
        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefa.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        //Atualizar
        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o  Id: {id} não foi encontrado no banco de dados");
            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefa.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }

        //Deletar
        public async Task<bool> Deletar(int id)
        {

            TarefaModel tarefaPorId = await BuscarId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa por Id: {id} não foi encontrado no banco de dados");
            }

            _dbContext.Tarefa.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}