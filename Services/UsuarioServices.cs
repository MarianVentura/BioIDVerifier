using Microsoft.EntityFrameworkCore;
using BioIDVerifier.DAL;  // Asegúrate de que este espacio de nombres sea el adecuado
using BioIDVerifier.Models;
using System.Linq.Expressions;
using System.Linq;

namespace BioIDVerifier.Services
{
    public class UsuarioServices
    {
        private readonly Contexto _contexto;

        public UsuarioServices(Contexto contexto)
        {
            _contexto = contexto;
        }

        // Método Existe: Verifica si el usuario ya existe
        public async Task<bool> Existe(int usuarioId)
        {
            return await _contexto.Usuario
                .AnyAsync(u => u.UsuarioId == usuarioId);
        }

        // Método Insertar: Inserta un nuevo registro de usuario (con foto y huella)
        private async Task<bool> Insertar(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
            return await _contexto.SaveChangesAsync() > 0;
        }

        // Método Modificar: Modifica un registro de usuario existente
        public async Task<bool> Modificar(Usuario usuario)
        {
            _contexto.Usuario.Update(usuario);
            var modificado = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(usuario).State = EntityState.Detached;
            return modificado;
        }


        // Método Guardar: Determina si es un nuevo registro o una actualización
        public async Task<bool> Guardar(Usuario usuario)
        {
            if (!await Existe(usuario.UsuarioId))
                return await Insertar(usuario);
            else
                return await Modificar(usuario);
        }

        // Método Eliminar: Elimina un registro de usuario por su ID
        public async Task<bool> Eliminar(int usuarioId)
        {
            var usuario = await _contexto.Usuario
                .Where(u => u.UsuarioId == usuarioId)
                .ExecuteDeleteAsync();
            return usuario > 0;
        }

        // Método Buscar: Busca un usuario por su ID
        public async Task<Usuario?> Buscar(int usuarioId)
        {
            return await _contexto.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);
        }

        // Método Listar: Obtiene una lista de usuarios con un filtro específico
        public async Task<List<Usuario>> Listar(Expression<Func<Usuario, bool>> criterio)
        {
            return await _contexto.Usuario
                .Where(criterio)
                .ToListAsync();
        }
    }
}
