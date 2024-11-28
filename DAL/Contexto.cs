using Microsoft.EntityFrameworkCore;
using BioIDVerifier.Models;

namespace BioIDVerifier.DAL
{
    public class Contexto : DbContext 
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
