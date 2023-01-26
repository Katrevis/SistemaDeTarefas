using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> BuscarUsuarios();

        Task<UsuarioModel> BuscarId(int id);

        Task<UsuarioModel> Adicionar(UsuarioModel usuarioModel);

        Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id);

        Task<bool> Deletar(int id);
        
    }
}
