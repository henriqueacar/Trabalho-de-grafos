using System.Collections;
using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class BFS : IGraphAlgorithm
{
    public void Execute(Grafo grafo, int startVertex)
    {
        // Cria uma lista com a ordem de vertices visitados
        var ordemDeVisita = new List<Vertice>();
        
        // Cria uma fila 
        var fila = new Queue<Vertice>();

        // Pra ajudar na hora de inserir o vertice inicial no console
        if (startVertex > 0)
        {
            startVertex -= 1;
        }
        // Pega o vertice do grafo referente ao vertice inicial escolhido
        var inicio = grafo.Vertices[startVertex];
        // Atribui a cor cinza pro vertice. Cinza quer dizer que passou
        inicio.Cor = "cinza";
        // Adiciona o inicio a fila
        fila.Enqueue(inicio);

        // Enqanto a fila tiver mais de 0 elementos
        while (fila.Count > 0)
        {
            // Remove o primeiro elemento da fila e atruibui a variavel do vertice atual
            Vertice atual = fila.Dequeue();
            // Adiciona na ordem de visita dos vertices
            ordemDeVisita.Add(atual);

            // Pra cada aresta do vertice atual
            foreach (var aresta in atual.Arestas)
            {
                // Pega o vertice vizinho a esquerda do atual
                Vertice vizinho = aresta.LeftVertice;

                // Se o vizinho da esquerda foi igual o atual, pega o da direita
                if (aresta.LeftVertice == atual)
                {
                    vizinho = aresta.RightVertice;
                }

                // Verifica se o vizinho não foi visitado/pintado, ou seja, a cor ainda é "branco"
                if (vizinho.Cor.Equals("branco"))
                {
                    vizinho.Cor = "cinza";
                    // Se for branco, pinta ele de cinza e adiciona ele na fila
                    fila.Enqueue(vizinho);
                }
            }
            
            // Pinta ele de preto
            atual.Cor = "preto";
        }

        Console.Write($"Ordem de vertices: ");
        foreach (var vertice in ordemDeVisita)
        {
            Console.Write($"{vertice} ");
        }

        Console.WriteLine();
    }

    // Not Used
    public void Execute(Grafo grafo, GraphUtils graphUtils)
    {
        throw new NotImplementedException();
    }
}