using TrabalhoGrafos.Graph.Algorithms;

namespace TrabalhoGrafos.Graph;

public class Graph
{
    private int[,] AdjacencyMatrix { get; set; }
    private int[,] IncidenceMatrix { get; set; }
    private int[,] DistanceMatrix { get; set; }
    private List<List<int>> AdjacencyList { get; set; }

    public Graph(int numVertices)
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
    public void DisplayAdjacencyList()
    {
        Console.WriteLine("Adjacency List:");

        for (var i = 0; i < AdjacencyList.Count; i++)
        {
            Console.Write($"Vertex {i}: ");
            foreach (var neighbor in AdjacencyList[i])
            {
                Console.Write($"{neighbor} ");
            }

            Console.WriteLine();
        }
    }

    // Imprime a matriz de adjacência do grafo
    public void DisplayAdjacencyMatrix()
    {
        Console.WriteLine("Incidence Matrix:");

        var rows = AdjacencyMatrix.GetLength(0);
        var cols = AdjacencyMatrix.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                Console.Write(AdjacencyMatrix[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    // Imprime a tabela de incidência do grafo
    public void DisplayIncidenceTable()
    {
        Console.WriteLine("Incidence Table:");

        Console.Write("\t");
        for (var j = 0; j < IncidenceMatrix.GetLength(1); j++)
        {
            Console.Write($"({(char)('A' + j)})\t");
        }

        Console.WriteLine();

        for (var i = 0; i < IncidenceMatrix.GetLength(0); i++)
        {
            Console.Write($"{(char)('A' + i)}\t");
            for (var j = 0; j < IncidenceMatrix.GetLength(1); j++)
            {
                Console.Write($" {IncidenceMatrix[i, j]}\t");
            }

            Console.WriteLine();
        }
    }
    
    // Lê o grafo e inicializa a representação
    public void ReadGraph(string filePath)
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

            for (int i = 0; i < numVertices; i++)
            {
                var lineValues = sr.ReadLine()?.Split(' ');

                AdjacencyList.Add(new List<int>());

                for (int j = 0; j < numVertices && j < lineValues.Length; j++)
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
            TransformToAdjacencyList();
            Console.WriteLine("Graph transformado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    // Função para transformar o grafo lido em uma Matriz de Incidência
    public void TransformToIncidenceMatrix()
    {
        int numVertices = IncidenceMatrix.GetLength(0);
        int numEdges = IncidenceMatrix.GetLength(1);

        for (int j = 0; j < numEdges; j++)
        {
            int edgeCount = 0;
            for (int i = 0; i < numVertices; i++)
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
                for (int i = 0; i < numVertices; i++)
                {
                    if (IncidenceMatrix[i, j] == 1 || IncidenceMatrix[i, j] == -1)
                    {
                        IncidenceMatrix[i, j] = 1;
                    }
                }
            }
        }
    }

    // Função para transformar grafo lido em um Lista de Adjacência
    public void TransformToAdjacencyList()
    {
        int numVertices = AdjacencyMatrix.GetLength(0);
        AdjacencyList = new List<List<int>>(numVertices);

        for (int i = 0; i < numVertices; i++)
        {
            AdjacencyList.Add(new List<int>());

            for (int j = 0; j < numVertices; j++)
            {
                if (AdjacencyMatrix[i, j] != 0)
                {
                    AdjacencyList[i].Add(j);
                }
            }
        }
    }
    
    // Função para transformar o grafo lido em uma Matriz de Adjacência
    public void TransformToAdjacencyMatrix()
    {
        int numVertices = AdjacencyList.Count;
        AdjacencyMatrix = new int[numVertices, numVertices];

        for (int i = 0; i < numVertices; i++)
        {
            foreach (int neighbor in AdjacencyList[i])
            {
                AdjacencyMatrix[i, neighbor] = 1;
            }
        }
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
    public void ExecuteBFS(int startVertex)
    {
        var bfs = new BFS();
        bfs.Execute(this, startVertex);
    }

    // Busca em Profundidade (Depth-First Search - DFS)
    public void ExecuteDFS(int startVertex)
    {
        var dfs = new DFS();
        dfs.Execute(this, startVertex);
    }

    // Prim
    public void ExecutePrim()
    {
        var prim = new Prim();
        prim.Execute(this);
    }

    // Dijkstra
    public void ExecuteDijkstra(int startVertex)
    {
        var dijkstra = new Dijkstra();
        dijkstra.Execute(this, startVertex);
    }

    // Ordenação Topologica
    public void ExecuteTopologicalSort()
    {
        var topologicalSort = new TopologicalSort();
        topologicalSort.Execute(this);
    }

    // Ciclo Euleriano
    public void ExecuteEulerianCycle()
    {
        var eulerianCycle = new EulerianCycle();
        eulerianCycle.Execute(this); 
    }
}