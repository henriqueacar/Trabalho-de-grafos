namespace TrabalhoGrafos.Graph.Models;

public class Grafo
{
    public List<Vertice> Vertices = new();
    public List<Aresta> Arestas = new();

    public void CriarGrafo(int[,] matriz)
    {
        // Pega o tamanho da matriz com base no numero de elementos da primeira linha
        var numeroVertices = matriz.GetLength(0);

        for (var i = 0; i < numeroVertices; i++)
        {
            // Cria todos os vertices
            // Cria um vertice com nome i + 1 para facilitar a leitura
            var vertice = new Vertice($"{i}")
            {
                // E adiciona uma lista de arestas que incidem nele
                Arestas = new List<Aresta>()
            };
            // Adiciona esse vertice no grafo
            Vertices.Add(vertice);
        }

        // Percorre a matriz
        for (var i = 0; i < numeroVertices; i++)
        {
            for (var j = i; j < numeroVertices; j++)
            {
                // Verifica se é um valor maior que 0 e menor que 999 para grafos valorados
                if (matriz[i, j] > 0 && matriz[i, j] < 999)
                {
                    // Cria uma aresta com o vertice de "origem" e "destino" 
                    var aresta = new Aresta(Vertices[i], Vertices[j], matriz[i,j]);

                    // Verifica se a aresta já existe
                    if (!Arestas.Contains(aresta))
                    {
                        Arestas.Add(aresta);
                        Vertices[i].Arestas.Add(aresta);
                        Vertices[j].Arestas.Add(aresta);
                    }
                }
            }
        }
    }

    public void ExibirGrafo()
    {
        Console.WriteLine("Vértices:");
        foreach (var vertice in Vertices)
        {
            Console.Write($"{vertice.Name}: ");

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

            Console.WriteLine();
        }

        Console.WriteLine("\nArestas:");
        foreach (var aresta in Arestas)
        {
            Console.WriteLine($"{aresta.LeftVertice} - {aresta.RightVertice}");
        }

        Console.WriteLine($"Número de vertices: {Vertices.Count}");
    }
}