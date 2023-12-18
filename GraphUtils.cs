using TrabalhoGrafos.Graph.Algorithms;
using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph;

public class GraphUtils
{
    public int[,] AdjacencyMatrix { get; set; }
    public int[,] IncidenceMatrix { get; set; }
    public int[,] DistanceMatrix { get; set; }
    public List<List<int>> AdjacencyList { get; set; }
    private const int ArcoInexistente = 999;

    public GraphUtils(int numVertices)
    {
        IncidenceMatrix = new int[numVertices, numVertices];
        AdjacencyMatrix = new int[numVertices, numVertices];
        DistanceMatrix = new int[numVertices, numVertices];
        AdjacencyList = new List<List<int>>(numVertices);

        for (var i = 0; i < numVertices; i++)
        {
            AdjacencyList.Add(new List<int>());
        }
    }


    public static void DisplayAdjacencyList(List<List<int>> adjacencyList)
    {
        Console.WriteLine("Lista Adjacencia:");

        for (var i = 0; i < adjacencyList.Count; i++)
        {
            Console.Write($"Vertex {i} -> ");

            if (adjacencyList[i].Count == 0)
            {
                Console.Write("Sem vizinhos");
            }
            else
            {
                foreach (var neighbor in adjacencyList[i])
                {
                    Console.Write($"{neighbor} ");
                }
            }

            Console.WriteLine();
        }
    }

    public static void DisplayAdjacencyMatrix(int[,] adjacencyMatrix)
    {
        Console.WriteLine("Matriz de Adjacencia:");

        var rows = adjacencyMatrix.GetLength(0);
        var cols = adjacencyMatrix.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                Console.Write(adjacencyMatrix[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    public static void DisplayIncidenceTable(int[,] incidenceMatrix)
    {
        Console.WriteLine("Tabela de Incidencia:");

        Console.Write("\t");
        for (var j = 0; j < incidenceMatrix.GetLength(1); j++)
        {
            Console.Write($"({(char)('A' + j)})\t");
        }

        Console.WriteLine();

        for (var i = 0; i < incidenceMatrix.GetLength(0); i++)
        {
            Console.Write($"{(char)('A' + i)}\t");
            for (var j = 0; j < incidenceMatrix.GetLength(1); j++)
            {
                Console.Write($" {incidenceMatrix[i, j]}\t");
            }

            Console.WriteLine();
        }
    }

    // Lê o grafo e inicializa a representação
    public void ReadGraph(string filePath, int choice)
    {
        try
        {
            using var sr = new StreamReader(filePath);

            var numVertices = int.Parse(sr.ReadLine());

            if (numVertices <= 0)
            {
                Console.WriteLine("Número inválido de vertices nesse arquivo.");
                return;
            }

            // Inicializa a estrutura de dados com tamanho igual ao numero de vertices
            AdjacencyMatrix = new int[numVertices, numVertices];
            IncidenceMatrix = new int[numVertices, numVertices];
            DistanceMatrix = new int[numVertices, numVertices];
            AdjacencyList = new List<List<int>>(numVertices);

            for (var i = 0; i < numVertices; i++)
            {
                // Le as linhas, remove os espaços em branco e adiciona em um array cada elemento
                var lineValues = sr.ReadLine()?.Split(' ');

                // Adiciona a estrutura de Lista de Adjacencia uma nova lista vazia pra cada vertice
                AdjacencyList.Add(new List<int>());

                for (var j = 0; j < numVertices && j < lineValues.Length; j++)
                {
                    if (!int.TryParse(lineValues[j], out var value))
                    {
                        Console.WriteLine($"Valor inválido na linha {i}, coluna {j}: {lineValues[j]}");
                        value = ArcoInexistente;
                    }

                    // Lida com os diferentes tipos de grafo de entrada
                    switch (choice)
                    {
                        case 1: // Grafo Simples Não-Valorado
                            AdjacencyMatrix[i, j] = (value != 0) ? 1 : 0;
                            IncidenceMatrix[i, j] = (value != 0) ? 1 : 0;
                            DistanceMatrix[i, j] = value;
                            break;

                        case 2: // Grafo Simples Valorado
                            AdjacencyMatrix[i, j] = value;
                            IncidenceMatrix[i, j] = value;
                            DistanceMatrix[i, j] = value;
                            break;

                        case 3: // Grafo Direcionado Valorado
                            AdjacencyMatrix[i, j] = value;
                            IncidenceMatrix[i, j] = value;
                            DistanceMatrix[i, j]  = (i == j) ? 0 : (value == 0) ? 999 : value;
                            break;

                        default:
                            Console.WriteLine("Grafo invalido.");
                            return;
                    }

                    if (value != 0)
                    {
                        AdjacencyList[i].Add(j);
                    }
                }
            }

            // Transforma o grafo lido (matriz de adjacencia ou matriz de distancias) em

            // Uma matriz de incidencia 
            TransformToIncidenceMatrix();
            // Uma matriz de distancia
            TransformToDistanceMatrix();
            // Uma lista de adjacencia
            TransformToAdjacencyList(choice);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    // Função para transformar o grafo lido em uma Matriz de Incidência
    public int[,] TransformToIncidenceMatrix()
    {
        // Pega o numero de vertices e arestas
        var numVertices = IncidenceMatrix.GetLength(0);
        var numEdges = IncidenceMatrix.GetLength(1);

        // Pra cada aresta
        for (var j = 0; j < numEdges; j++)
        {
            var edgeCount = 0;
            // E pra cada vertice
            for (var i = 0; i < numVertices; i++)
            {
                if (AdjacencyMatrix[i, j] != 0) // Se o elemento i,j da matriz for diferente de 0
                {
                    // Adiciona 1 ao elemento i,j da matriz de incidencia 
                    IncidenceMatrix[i, j] = 1;
                    edgeCount++;
                }
                else if (AdjacencyMatrix[j, i] != 0) // Se o elemento j,i da matriz for diferente de 0
                {
                    // Adiciona -1 ao elemento i,j da matriz de incidencia 
                    IncidenceMatrix[i, j] = -1;
                    edgeCount++;
                }
            }

            if (edgeCount == 1)
            {
                for (var i = 0; i < numVertices; i++)
                {
                    if (IncidenceMatrix[i, j] == 1 || IncidenceMatrix[i, j] == -1)
                    {
                        IncidenceMatrix[i, j] = 1;
                    }
                }
            }
        }

        return IncidenceMatrix;
    }

    // Função para transformar grafo lido em um Lista de Adjacência
    public List<List<int>> TransformToAdjacencyList(int graphType)
    {
        // Pega o numero de vertices e arestas
        var numVertices = AdjacencyMatrix.GetLength(0);
        // Inicializa a lista com uma lista de lista de inteiros com o tamanho equivalente ao numero de vertices
        AdjacencyList = new List<List<int>>(numVertices);

        for (var i = 0; i < numVertices; i++)
        {
            AdjacencyList.Add(new List<int>());
        }

        switch (graphType)
        {
            case 1: // Grafo Simples Não-Valorado
                for (var i = 0; i < numVertices; i++)
                {
                    for (var j = 0; j < numVertices; j++)
                    {
                        if (AdjacencyMatrix[i, j] != 0)
                        {
                            AdjacencyList[i].Add(j);

                            // Adiciona a aresta de volta
                            if (AdjacencyMatrix[j, i] != 0 && !AdjacencyList[j].Contains(i))
                            {
                                AdjacencyList[j].Add(i);
                            }
                        }
                    }
                }

                break;

            case 2: // Grafo Simples Valorado
                for (var i = 0; i < numVertices; i++)
                {
                    for (var j = 0; j < numVertices; j++)
                    {
                        if (AdjacencyMatrix[i, j] != 0)
                        {
                            // Checar se tem self-loops
                            if (i != j && !AdjacencyList[i].Contains(j))
                            {
                                AdjacencyList[i].Add(j);
                            }

                            // Adiciona a aresta de volta pra grafos não direcionados
                            if (graphType == 1 && i != j && !AdjacencyList[j].Contains(i))
                            {
                                AdjacencyList[j].Add(i);
                            }
                        }
                    }
                }

                break;

            case 3: // Grafo Direcionado Valorado
                for (var i = 0; i < numVertices; i++)
                {
                    for (var j = 0; j < numVertices; j++)
                    {
                        if (AdjacencyMatrix[i, j] != 0)
                        {
                            AdjacencyList[i].Add(j);
                        }
                    }
                }

                break;

            default:
                Console.WriteLine("Invalid graph type.");
                return null;
        }

        return AdjacencyList;
    }

    public int[,] TransformToDistanceMatrix()
    {
        var numVertices = AdjacencyList.Count;
        DistanceMatrix = new int[numVertices, numVertices];

        // Preenche a matriz com valores iniciais
        for (var i = 0; i < numVertices; i++)
        {
            for (var j = 0; j < numVertices; j++)
            {
                // Para indicar ausência de aresta foi usada a constante ArcoInexistente que é o número 999
                DistanceMatrix[i, j] = (i == j) ? 0 : ArcoInexistente;
            }
        }

        // Atualiza os valores da matriz com os pesos das arestas
        for (var i = 0; i < numVertices; i++)
        {
            foreach (var neighbor in AdjacencyList[i])
            {
                var weight = GetEdgeWeight(i, neighbor);
                DistanceMatrix[i, neighbor] = weight;
            }
        }

        return DistanceMatrix;
    }

    // Método para pegar o peso da aresta entre dois vértices
    private int GetEdgeWeight(int vertex1, int vertex2)
    {
        return AdjacencyMatrix[vertex1, vertex2];
    }

    public static void SalvarMatriz(string fileName, int[,] matrix)
    {
        const string path = @"C:\Users\iwest\RiderProjects\Trabalho-de-grafos\Outputs";
        var filePath = Path.Combine(path, fileName);
        
        using var sw = new StreamWriter(filePath);

        try
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    sw.Write(matrix[i, j] + "\t");
                }

                sw.WriteLine();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Console.WriteLine("foi");
    }    
    public static void SalvarTabelaIncidencia(string fileName, int[,] matrix)
    {
        const string path = @"C:\Users\iwest\RiderProjects\Trabalho-de-grafos\Outputs";
        var filePath = Path.Combine(path, fileName);

        using var sw = new StreamWriter(filePath);

        sw.Write("\t");
        for (var j = 0; j < matrix.GetLength(1); j++)
        {
            sw.Write($"({(char)('A' + j)})\t");
        }

        sw.WriteLine();

        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            sw.Write($"{(char)('A' + i)}\t");
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                sw.Write($" {matrix[i, j]}\t");
            }

            sw.WriteLine();
        }
    }

    public static void SalvarLista(string fileName, List<List<int>> list)
    {
        const string path = @"C:\Users\iwest\RiderProjects\Trabalho-de-grafos\Outputs";
        var filePath = Path.Combine(path, fileName);
        
        using var sw = new StreamWriter(filePath);
        
        for (var i = 0; i < list.Count; i++)
        {
            sw.Write($"Vertice {i}: ");
            foreach (var item in list[i])
            {
                sw.Write(item + " ");
            }

            sw.WriteLine();
        }

        Console.WriteLine("foi");
    }

    // Salva o grafo em um txt
    // public void SaveGraph(string filePath)
    // {
    //     try
    //     {
    //         using var sw = new StreamWriter(filePath);
    //
    //         sw.WriteLine(AdjacencyList.Count);
    //
    //         for (var i = 0; i < AdjacencyMatrix.GetLength(0); i++)
    //         {
    //             for (var j = 0; j < AdjacencyMatrix.GetLength(1); j++)
    //             {
    //                 sw.Write($"{AdjacencyMatrix[i, j]} ");
    //             }
    //
    //             sw.WriteLine();
    //         }
    //
    //         Console.WriteLine("Graph saved successfully to the file.");
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"An error occurred while saving the graph: {ex.Message}");
    //     }
    // }

    // Busca em Largura (Breadth-First Search - BFS)
    public void ExecuteBFS(int startVertex, Grafo grafo)
    {
        var bfs = new BFS();
        bfs.Execute(grafo, startVertex);
    }

    // Busca em Profundidade (Depth-First Search - DFS)
    public void ExecuteDFS(int startVertex, Grafo grafo)
    {
        var dfs = new DFS();
        dfs.Execute(grafo, startVertex);
    }

    // Prim
    public void ExecutePrim(Grafo grafo)
    {
        var prim = new Prim();
        prim.Execute(grafo);
    }

    // Dijkstra
    public void ExecuteDijkstra(int[,] grafo, int src)
    {
        var dijkstra = new Dijkstra();
        dijkstra.Execute(grafo, src);
    }

    // Ordenação Topologica
    public void ExecuteTopologicalSort(Grafo grafo)
    {
        var topologicalSort = new TopologicalSort();
        topologicalSort.Execute(grafo);
    }

    // Ciclo Euleriano
    public void ExecuteEulerianCycle(Grafo grafo)
    {
        var eulerianCycle = new EulerianCycle();
        eulerianCycle.Execute(grafo);
    }
}