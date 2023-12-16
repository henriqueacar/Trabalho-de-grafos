using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class Dijkstra
{
    public void Execute(int[,] grafo, int src)
    {
        // Pega o numero de vertices do grafo
        var numVertices = grafo.GetLength(0);
        
        // Cria arrays para guardar as distancia, menor caminho e vertices visitados 
        var distancias = new int[numVertices];
        var verticesVisitados = new bool[numVertices];
        var caminhoMaisCurto = new int[numVertices];

        // Initialize distance, visited, and path arrays
        // Inicializa os arrays
        for (var i = 0; i < numVertices; ++i)
        {
            distancias[i] = int.MaxValue; // Inicializa todas as distancia com maior valor para simbolizar não existencia de arco ainda
            verticesVisitados[i] = false; // Todos os valores sendo falso pois nenhum vertice foi visitado até então
            caminhoMaisCurto[i] = -1; // Não possui nenhum vertice no caminho
        }

        // Distancia do vertice inicial para ele mesmo é 0
        distancias[src] = 0;

        // Encontrar o caminho mais curto de todos os vértices
        for (var i = 0; i < numVertices - 1; i++)
        {
            // Encontra o vertice com menor distancia
            var m = MenorDistancia(distancias, verticesVisitados, numVertices);

            // Marca o vértice selecionado como true para indicar que foi visitado
            verticesVisitados[m] = true;

            // Atualiza o valor da distância dos vértices adjacentes
            for (var n = 0; n < numVertices; n++)
            {
                // Verifica se o vértice não foi visitado,
                // há uma aresta de m para n,
                // e se o peso total do caminho de origem para n passando por m é menor do que
                // o valor atual de distancia[n]
                if (!verticesVisitados[n] && Convert.ToBoolean(grafo[m, n]) && 
                    distancias[m] != int.MaxValue &&
                    distancias[m] + grafo[m, n] < distancias[n])
                {
                    // Atualiza a distância e registra o caminho
                    distancias[n] = distancias[m] + grafo[m, n];
                    caminhoMaisCurto[n] = m;
                }
            }
        }
        // Imprime o caminho mai curto
        Print(distancias, caminhoMaisCurto, src, numVertices);
    }
    private static void Print(int[] distancias, int[] caminhoMaisCurto, int src, int numVertices)
    {
        Console.WriteLine("Caminhos mínimos a partir do vértice inicial:");
    
        for (var i = 0; i < numVertices; ++i)
        {
            if (i != src)
            {
                Console.Write($"{src}-{i}: ");
                PegarCaminho(caminhoMaisCurto, i);
                Console.WriteLine($" ({distancias[i]})");
            }
        }
    }

    private static void PegarCaminho(int[] caminhoMaisCurto, int vertice)
    {
        if (vertice == -1)
            return;

        PegarCaminho(caminhoMaisCurto, caminhoMaisCurto[vertice]);

        if (caminhoMaisCurto[vertice] != -1)
            Console.Write($" -> {vertice}");
        else
            Console.Write($"{vertice}");
    }
    private static int MenorDistancia(int[] distancia, bool[] verticesVisitados, int verticeCount)
    {
        var menorDistancia = int.MaxValue;
        var verticeComMenorDistancia = 0;

        for (var v = 0; v < verticeCount; ++v)
        {
            // Verifica se o vértice já foi incluido no caminho
            // e se a distância é menor ou igual à menor distância atual
            if (verticesVisitados[v] == false && distancia[v] <= menorDistancia)
            {
                menorDistancia = distancia[v];
                verticeComMenorDistancia = v;
            }
        }

        return verticeComMenorDistancia;
    }
}