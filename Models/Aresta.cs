namespace TrabalhoGrafos.Graph.Models;

public class Aresta
{
    public Vertice LeftVertice { get; set; }
    public Vertice RightVertice { get; set; }
    public int Peso { get; set; }

    public Aresta(Vertice leftVertice, Vertice rightVertice)
    {
        LeftVertice = leftVertice;
        RightVertice = rightVertice;
    }
    public Aresta(Vertice leftVertice, Vertice rightVertice, int peso)
    {
        LeftVertice = leftVertice;
        RightVertice = rightVertice;
        Peso = peso;
    }
    
}