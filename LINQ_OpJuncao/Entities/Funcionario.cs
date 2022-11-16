using System.ComponentModel.DataAnnotations;

namespace LINQ_OpJuncao.Entities;

//Entidade principal (pai)
public class Funcionario
{
    public int FuncionarioId { get; set; }
    [MaxLength(80)]
    public string FuncionarioNome { get; set; } = null!;
    [MaxLength(80)]
    public string FuncionarioCargo { get; set; } = null!;

    public int? SetorId { get; set; }
}
