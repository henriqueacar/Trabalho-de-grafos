using System.Collections;
using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class BFS
{
    public void Execute(Grafo grafo, int startVertex)
    {
        // Cria uma lista com a ordem de vertices visitados
        var ordemDeVisita = new List<Vertice>();

        // Cria uma fila 
        var fila = new Queue<Vertice>();

        // Pega o vertice do grafo referente ao vertice inicial escolhido
        var inicio = grafo.Vertices[startVertex];

        // Adiciona o inicio a fila
        fila.Enqueue(inicio);
        
        while (fila.Count > 0)
        {
            // Pega o próximo vertice da fila
            var verticeAtual = fila.Dequeue();
            switch (verticeAtual.Cor)
            {
                case "preto": // Já visitado
                    continue;
            }

            // Atribui a cor cinza pro vertice. Cinza quer dizer que passou
            verticeAtual.Cor = "cinza";

            foreach (var vizinho in verticeAtual.Vizinhos)
            {
                if (vizinho.Cor.Equals("branco"))
                {
                    // Marca como visitado
                    vizinho.Cor = "cinza";
                    // Adiciona o vizinho como próximo vertice da fila
                    fila.Enqueue(vizinho);
                }
            }

            // Pinta o vértice atual de preto após visitar todos os vizinhos
            verticeAtual.Cor = "preto";

            ordemDeVisita.Add(verticeAtual);
        }

        Console.Write($"Ordem de vertices: ");
        foreach (var vertice in ordemDeVisita)
        {
            Console.Write($"{vertice} ");
        }

        Console.WriteLine();
    }
}