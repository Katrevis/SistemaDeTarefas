using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;

        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext) 
        {
            _dbContext= sistemaTarefasDBContext;
        }

        //BuscarId
        public async Task<UsuarioModel> BuscarId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        //BuscarUsuario
        public async Task<List<UsuarioModel>> BuscarUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        //Adicionar
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await  _dbContext.SaveChangesAsync();

            return usuario;
        }

        //Atualizar
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioId = await BuscarId(id);

            if(usuarioId == null) 
            {
                throw new Exception($"Usuário para o  Id: {id} não foi encontrado no banco de dados");
            }

            usuarioId.Nome = usuario.Nome;
            usuarioId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioId);
            await  _dbContext.SaveChangesAsync();
            return usuarioId;
        }

        //Deletar
        public async Task<bool> Deletar(int id)
        {

            UsuarioModel usuarioId = await BuscarId(id);

            if (usuarioId == null)
            {
                throw new Exception($"Usuário para o  Id: {id} não foi encontrado no banco de dados");
            }

            _dbContext.Usuarios.Remove(usuarioId);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
