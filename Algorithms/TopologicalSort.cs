using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class TopologicalSort
{
    List<Vertice> vertices;
    Stack<Vertice> ordem;

    public void Execute(Grafo grafo)
    {
        vertices = grafo.Vertices;
        var numVertices = vertices.Count;
        ordem = new Stack<Vertice>();

        for (var i = 0; i < numVertices; i++)
        {
            if (vertices[i].Cor.Equals("branco"))
            {
                if (!BuscaEmProfudidade(i))
                {
                    Console.WriteLine("Graph contains a cycle!");
                    return;
                }

                BuscaEmProfudidade(i);
            }
        }
        
        Console.WriteLine("Ordenação topologica: ");
        foreach (var v in ordem)
        {
            Console.Write($"{v.Name} ");
        }

        Console.WriteLine();
    }

    private bool BuscaEmProfudidade(int index)
    {
        var verticeAtual = vertices[index];

        switch (verticeAtual.Cor)
        {
            case "preto": // Já visitado
                return true;

            case "cinza": // Ciclo
                return false;
        }

        verticeAtual.Cor = "cinza";

        foreach (var vizinho in verticeAtual.Vizinhos)
        {
            // Procura se tem um ciclo
            if (!BuscaEmProfudidade(vertices.IndexOf(vizinho)))
            {
                return false;
            }
        }

        verticeAtual.Cor = "preto";
        
        ordem.Push(verticeAtual);

        return true;
    }
}