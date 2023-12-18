namespace TrabalhoGrafos.Graph.Models;

public class Vertice
{
    public string Name { get; set; }
    public List<Aresta> Arestas { get; set; }
    public Vertice Pai { get; set; }
    public string Cor { get; set; } = "branco";
    public List<Vertice> Vizinhos { get; set; }

    public Vertice(string nome)
    {
        Name = nome;
        Vizinhos = new List<Vertice>();
    }
    
    public override string ToString()
    {
        return Name;
    }
}