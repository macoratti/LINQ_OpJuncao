using LINQ_OpJuncao.Context;
using System.Net;

//PopulaDatabase();
//ExemploInnerJoin();
ExemploLeftJoin();

static void PopulaDatabase()
{
    using (var contexto = new AppDbContext())
    {
        try
        {
            SeedDatabase.PopulaDB(contexto);
            Console.WriteLine("Concluído com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro : " + ex.Message);
        }
    }
}

static void ExemploInnerJoin()
{
    using (var contexto = new AppDbContext())
    {
        //var innerJoin = contexto.Funcionarios              //Outer Data Source
        //.Join(
        //      contexto.Setores,                            //Inner Data Source
        //               funcionario => funcionario.SetorId, //Inner Key Selector
        //               setor => setor.SetorId,             //Outer Key selector
        //               (funcionario, setor) => new         //Projetando os dados em um conjunto
        //               {
        //                   NomeFuncionario = funcionario.FuncionarioNome,
        //                   NomeSetor = setor.SetorNome,
        //                   CargoFuncionario = funcionario.FuncionarioCargo
        //               }).ToList();

        var innerJoin2 = (from f in contexto.Funcionarios
                          join s in contexto.Setores on f.SetorId equals s.SetorId
                          select new
                          {
                              NomeFuncionario = f.FuncionarioNome,
                              CargoFuncionario = f.FuncionarioCargo,
                              NomeSetor = s.SetorNome
                          }).ToList();

        Console.WriteLine("Funcionario\t\tCargo\t\t\tSetor");

        foreach (var funcionario in innerJoin2)
        {
            Console.WriteLine($"{funcionario.NomeFuncionario}" +
                              $"\t\t{funcionario.CargoFuncionario}" +
                              $"\t\t{funcionario.NomeSetor}");
        }
        Console.ReadLine();
    }
}

static void ExemploLeftJoin()
{
    using (var contexto = new AppDbContext())
    {
        var leftJoin = (from f in contexto.Funcionarios
                        join s in contexto.Setores
                        on f.SetorId equals s.SetorId
                        into funciSetorGrupo
                        from setor in funciSetorGrupo.DefaultIfEmpty()
                        select new
                        {
                            NomeFuncionario = f.FuncionarioNome,
                            CargoFuncionario = f.FuncionarioCargo,
                            NomeSetor = setor.SetorNome
                        }).ToList();

        Console.WriteLine("Funcionario\t\tCargo\t\t\tSetor");
        foreach (var funcionario in leftJoin)
        {
            Console.WriteLine($"{funcionario.NomeFuncionario}" +
                              $"\t\t{funcionario.CargoFuncionario}" +
                              $"\t\t{funcionario.NomeSetor}");
        }
        Console.ReadLine();
    }
}