using LINQ_OpJuncao.Entities;
using Microsoft.EntityFrameworkCore;

namespace LINQ_OpJuncao.Context;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //define o provedor do BD e a string de conexão
        optionsBuilder
          .UseSqlServer("Data Source=DESKTOP-DK57UNP\\SQLEXPRESS;Initial Catalog=LinqDB;" +
                        "Integrated Security=True;TrustServerCertificate=True;");
        //exibe as consultas SQL no console
        optionsBuilder
          .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    }

    //mapeia as entidades para as tabelas do BD
    public DbSet<Setor> Setores { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
}
