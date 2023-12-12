using TrabalhoGrafos.Graph.Algorithms;
using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph;

public class GraphUtils
{
    private int[,] AdjacencyMatrix { get; set; }
    private int[,] IncidenceMatrix { get; set; }
    private int[,] DistanceMatrix { get; set; }
    private List<List<int>> AdjacencyList { get; set; }

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


    // Imprime a lista de adjacência do grafo
    public static void DisplayAdjacencyList(List<List<int>> adjacencyList)
    {
        Console.WriteLine("Adjacency List:");

        for (var i = 0; i < adjacencyList.Count; i++)
        {
            Console.Write($"Vertex {i}: ");
            foreach (var neighbor in adjacencyList[i])
            {
                Console.Write($"{neighbor} ");
            }

            Console.WriteLine();
        }
    }

    // Imprime a matriz de adjacência do grafo
    public static void DisplayAdjacencyMatrix(int[,] adjacencyMatrix)
    {
        Console.WriteLine("Incidence Matrix:");

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

    // Imprime a tabela de incidência do grafo
    public static void DisplayIncidenceTable(int[,] incidenceMatrix)
    {
        Console.WriteLine("Incidence Table:");

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
                Console.WriteLine("Invalid number of vertices in the file.");
                return;
            }

            AdjacencyMatrix = new int[numVertices, numVertices];
            IncidenceMatrix = new int[numVertices, numVertices];
            DistanceMatrix = new int[numVertices, numVertices];
            AdjacencyList = new List<List<int>>(numVertices);

            for (var i = 0; i < numVertices; i++)
            {
                var lineValues = sr.ReadLine()?.Split(' ');

                AdjacencyList.Add(new List<int>());

                for (var j = 0; j < numVertices && j < lineValues.Length; j++)
                {
                    if (!int.TryParse(lineValues[j], out var value)) 
                        continue;
                    
                    AdjacencyMatrix[i, j] = value;
                    IncidenceMatrix[i, j] = value;
                    DistanceMatrix[i, j] = value;

                    if (value != 0)
                    {
                        AdjacencyList[i].Add(j);
                    }
                }
            }
            Console.WriteLine("Graph read successfully from the file.");
            
            TransformToIncidenceMatrix();
            TransformToAdjacencyMatrix();
            TransformToAdjacencyList(choice);
            Console.WriteLine("Graph transformado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    // Função para transformar o grafo lido em uma Matriz de Incidência
    public int[,] TransformToIncidenceMatrix()
    {
        var numVertices = IncidenceMatrix.GetLength(0);
        var numEdges = IncidenceMatrix.GetLength(1);

        for (var j = 0; j < numEdges; j++)
        {
            var edgeCount = 0;
            for (var i = 0; i < numVertices; i++)
            {
                if (AdjacencyMatrix[i, j] != 0)
                {
                    IncidenceMatrix[i, j] = 1;
                    edgeCount++;
                }
                else if (AdjacencyMatrix[j, i] != 0)
                {
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
    public List<List<int>> TransformToAdjacencyList(int choice)
    {
        var numVertices = AdjacencyMatrix.GetLength(0);
        AdjacencyList = new List<List<int>>(numVertices);

        for (var i = 0; i < numVertices; i++)
        {
            AdjacencyList.Add(new List<int>());
        }

        if(choice == 1){ //Choice 1 = grafo simples
            for(var i = 0; i < numVertices; i++){
                for (var j = 0; j < numVertices; j++)
                {
                    if (AdjacencyMatrix[i, j] != 0)
                    {
                        AdjacencyList[i].Add(j);
                    if(AdjacencyMatrix[j, i] == 0)
                            AdjacencyList[j].Add(i);
                    }

                }
            }
        }
        else {
            for(var i = 0; i < numVertices; i++){
                for (var j = 0; j < numVertices; j++)
                {
                    if (AdjacencyMatrix[i, j] != 0)
                    {
                        AdjacencyList[i].Add(j);
                    }

                }
            }
        }
        return AdjacencyList;
    }
    
    // Função para transformar o grafo lido em uma Matriz de Adjacência
    public int[,] TransformToAdjacencyMatrix()
    {
        var numVertices = AdjacencyList.Count;
        AdjacencyMatrix = new int[numVertices, numVertices];

        for (var i = 0; i < numVertices; i++)
        {
            foreach (var neighbor in AdjacencyList[i])
            {
                AdjacencyMatrix[i, neighbor] = 1;
            }
        }

        return AdjacencyMatrix;
    }

    // Salva o grafo em um txt
    public void SaveGraph(string filePath)
    {
        try
        {
            using var sw = new StreamWriter(filePath);

            sw.WriteLine(AdjacencyList.Count);

            for (var i = 0; i < AdjacencyMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < AdjacencyMatrix.GetLength(1); j++)
                {
                    sw.Write($"{AdjacencyMatrix[i, j]} ");
                }

                sw.WriteLine();
            }

            Console.WriteLine("Graph saved successfully to the file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the graph: {ex.Message}");
        }
    }
    
    // Busca em Largura (Breadth-First Search - BFS)
    public void ExecuteBFS(int startVertex, Grafo grafo)
    {
        var bfs = new BFS();
        bfs.Execute(grafo, this, startVertex);
    }

    // Busca em Profundidade (Depth-First Search - DFS)
    public void ExecuteDFS(int startVertex, Grafo grafo)
    {
        var dfs = new DFS();
        dfs.Execute(grafo, this, startVertex);
    }

    // Prim
    public void ExecutePrim(Grafo grafo)
    {
        var prim = new Prim();
        prim.Execute(grafo, this);
    }

    // Dijkstra
    public void ExecuteDijkstra(int startVertex, Grafo grafo)
    {
        var dijkstra = new Dijkstra();
        dijkstra.Execute(grafo, this, startVertex);
    }

    // Ordenação Topologica
    public void ExecuteTopologicalSort(Grafo grafo)
    {
        var topologicalSort = new TopologicalSort();
        topologicalSort.Execute(grafo, this);
    }

    // Ciclo Euleriano
    public void ExecuteEulerianCycle(Grafo grafo)
    {
        var eulerianCycle = new EulerianCycle();
        eulerianCycle.Execute(grafo, this); 
    }
}