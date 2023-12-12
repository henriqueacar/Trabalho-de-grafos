namespace TrabalhoGrafos.Graph.Models;

public class Grafo
{
    public List<Vertice> Vertices;
    public List<Aresta> Arestas;
    public string[] Nomes;

    public Grafo()
    {
        Vertices = new List<Vertice>();
        Arestas = new List<Aresta>();
    }
    
    public void CriarGrafo(int[,] matriz)
    {
        var numeroVertices = matriz.GetLength(0);
        Nomes = new string[numeroVertices];
        
        for (var i = 0; i < numeroVertices; i++)
        {
            var vertice = new Vertice("V" + i)
            {
                Arestas = new List<Aresta>()
            };
            Vertices.Add(vertice);
        }
        for (var i = 0; i < numeroVertices; i++)
        {
            for (var j = i; j < numeroVertices; j++)
            {
                if (matriz[i,j] == 1)
                {
                    var aresta = new Aresta(Vertices[i], Vertices[j]);
                    
                    Arestas.Add(aresta);
                    Vertices[i].Arestas.Add(aresta);
                    Vertices[j].Arestas.Add(aresta);
                }   
            }
        }
    }
    public void ExibirGrafo()
    {
        Console.WriteLine("Vértices:");
        foreach (var vertice in Vertices)
        {
            Console.Write($"{vertice.Name}: [");

            var arestaCount = vertice.Arestas.Count;
            for (var i = 0; i < arestaCount; i++)
            {
                var aresta = vertice.Arestas[i];
                Console.Write($"({aresta.LeftVertice},{aresta.RightVertice})");

                if (i < arestaCount - 1)
                {
                    Console.Write(", ");
                }
            }

            Console.WriteLine("]");
        }

        Console.WriteLine("\nArestas:");
        foreach (var aresta in Arestas)
        {
            Console.WriteLine($"{aresta.LeftVertice} - {aresta.RightVertice}");
        }

        Console.WriteLine($"Número de vertices: {Vertices.Count}");
    }
}