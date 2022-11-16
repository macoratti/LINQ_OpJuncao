using LINQ_OpJuncao.Entities;

namespace LINQ_OpJuncao.Context;

public class SeedDatabase
{
    public static void PopulaDB(AppDbContext contexto)
    {
        contexto.Database.EnsureCreated();

        if (!contexto.Funcionarios.Any())
        {
            var setor1 = new Setor
            {
                SetorNome = "Recursos Humanos",
            };
            contexto.Setores.Add(setor1);

            var funcionarios1 = new List<Funcionario>
            {
               new Funcionario { FuncionarioNome="Marisa Monte", 
                                 FuncionarioCargo="Gerente", SetorId= 1},
               new Funcionario { FuncionarioNome="Janice Ribeiro", 
                                 FuncionarioCargo="Administrativo", SetorId= 1}
            };

            contexto.Funcionarios.AddRange(funcionarios1);

            var setor2 = new Setor
            {
                SetorNome = "Contabilidade",
            };
            contexto.Setores.Add(setor2);
            var funcionarios2 = new List<Funcionario>
            {
                 new Funcionario { FuncionarioNome="Pedro Toledo", 
                                   FuncionarioCargo="Gerente", SetorId=2},
                 new Funcionario { FuncionarioNome="Andre Sanches", 
                                   FuncionarioCargo="Contador", SetorId=2},
                 new Funcionario { FuncionarioNome="Hilda Hinst", 
                                   FuncionarioCargo="Diretora"}
            };
            contexto.Funcionarios.AddRange(funcionarios2);


            var setor3 = new Setor
            {
                SetorNome = "Marketing",
            };
            contexto.Setores.Add(setor3);
            var funcionarios3 = new List<Funcionario>
            {
                    new Funcionario { FuncionarioNome="Ana Maria Lima",
                                      FuncionarioCargo="Gerente", SetorId=3},
                    new Funcionario { FuncionarioNome="Carlos Ribeiro", 
                                      FuncionarioCargo="Designer", SetorId=3},
                    new Funcionario { FuncionarioNome="Jaime Lacuste", 
                                      FuncionarioCargo="CEO"},
            };

            contexto.Funcionarios.AddRange(funcionarios3);

            var setor4 = new Setor
            {
                SetorNome = "Tecnologia",
            };
            contexto.Setores.Add(setor4);

            contexto.SaveChanges();
        }
    }
}
