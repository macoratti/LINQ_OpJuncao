using System.ComponentModel.DataAnnotations;

namespace LINQ_OpJuncao.Entities;

//Entidade dependente(filha)
public class Setor
{
    public int SetorId { get; set; }
    [MaxLength(80)]
    public string SetorNome { get; set; } = null!;
}
