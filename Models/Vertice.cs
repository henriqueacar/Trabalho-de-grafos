namespace TrabalhoGrafos.Graph.Models;

public class Vertice
{
    public string Name { get; set; }
    public List<Aresta> Arestas { get; set; }
    public Vertice Pai { get; set; }

    public Vertice(string nome)
    {
        Name = nome;
    }

    public Vertice(string nome, List<Aresta> arestas)
    {
        Arestas = arestas;
        Name = nome;
    }

    public override string ToString()
    {
        return Name;
    }
}