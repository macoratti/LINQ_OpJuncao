using LINQ_OpJuncao.Context;
//PopulaDatabase();
//ExemploInnerJoin();
//ExemploLeftJoin();
//ExemploRightJoin();
//ExemploFullJoin();
//ExemploCrossJoin();
ExemploGroupJoin();

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

static void ExemploRightJoin()
{
    using (var contexto = new AppDbContext())
    {
        var rightJoin = (from s in contexto.Setores
                         join f in contexto.Funcionarios
                         on s.SetorId equals f.SetorId
                         into SetorFunciGrupo
                         from funcionario in SetorFunciGrupo.DefaultIfEmpty()
                         select new
                         {
                             NomeFuncionario = funcionario.FuncionarioNome,
                             CargoFuncionario = funcionario.FuncionarioCargo,
                             NomeSetor = s.SetorNome
                         }).ToList();

        Console.WriteLine("Funcionario\t\tCargo\t\t\tSetor");

        foreach (var funcionario in rightJoin)
        {
            Console.WriteLine($"{funcionario.NomeFuncionario}" +
                              $"\t\t {funcionario.CargoFuncionario}" +
                              $"\t\t {funcionario.NomeSetor}");
        }
        Console.ReadLine();
    }
}

static void ExemploFullJoin()
{
    using (var contexto = new AppDbContext())
    {
        var leftJoin = from f in contexto.Funcionarios
                       join s in contexto.Setores on f.SetorId equals s.SetorId
                       into set
                       from setor in set.DefaultIfEmpty()
                       select new
                       {
                           Nome = f.FuncionarioNome,
                           Cargo = f.FuncionarioCargo,
                           Setor = setor.SetorNome
                       };

        var rightJoin = from s in contexto.Setores
                        join f in contexto.Funcionarios on s.SetorId equals f.SetorId
                        into funci
                        from funcionario in funci.DefaultIfEmpty()
                        select new
                        {
                            Nome = funcionario.FuncionarioNome,
                            Cargo = funcionario.FuncionarioCargo,
                            Setor = s.SetorNome
                        };

        var fullJoin = leftJoin.Union(rightJoin);

        Console.WriteLine("Funcionario\t\tCargo\t\tSetor");

        foreach (var resultado in fullJoin)
        {
            Console.WriteLine(resultado.Nome + "\t\t" +
                              resultado.Cargo + "\t\t" +
                              resultado.Setor);
        }

    }
}

static void ExemploCrossJoin()
{
    using (var contexto = new AppDbContext())
    {
        var crossJoin = from f in contexto.Funcionarios
                        from s in contexto.Setores
                        select new
                        {
                            Nome = f.FuncionarioNome,
                            Cargo = f.FuncionarioCargo,
                            Setor = s.SetorNome
                        };

        Console.WriteLine("Funcionario\t\tCargo\t\t\tSetor");

        foreach (var resultado in crossJoin)
        {
            Console.WriteLine(resultado.Nome + "\t\t" + resultado.Cargo + "\t\t\t" + resultado.Setor);
        }
    }
}

static void ExemploGroupJoin()
{
    using (var contexto = new AppDbContext())
    {
        var groupJoin = contexto.Setores
                        .GroupJoin(contexto.Funcionarios,
                        s => s.SetorId, f => f.SetorId,
                        (f, funcionariosGrupo) => new
                        {
                            Funcionarios = funcionariosGrupo,
                            NomeSetor = f.SetorNome
                        }).ToList();

        foreach (var item in groupJoin)
        {
            Console.WriteLine(item.NomeSetor);
            foreach (var func in item.Funcionarios)
            {
                Console.WriteLine($"\t {func.FuncionarioNome}");
            }
        }
        Console.ReadLine();
    }
}