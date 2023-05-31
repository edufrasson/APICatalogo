using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
    public int Id { get; set; }
    public string? Nome { get; set; } // ? -> significa que a prop é nullable
    public string? ImageUrl { get; set; }

    public ICollection<Produto>? Produtos { get; set; }
}
