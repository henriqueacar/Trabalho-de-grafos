using TrabalhoGrafos.Graph.Algorithms;

namespace TrabalhoGrafos.Graph;

public class Graph
{
    private int[,] incidenceMatrix;
    private int[,] adjacencyMatrix;
    private List<List<int>> adjacencyList;

    public Graph(int numVertices)
    {
        incidenceMatrix = new int[numVertices, numVertices];
        adjacencyMatrix = new int[numVertices, numVertices];
        adjacencyList = new List<List<int>>(numVertices);

        for (var i = 0; i < numVertices; i++)
        {
            adjacencyList.Add(new List<int>());
        }
    }


    // Displays the adjacency list representation of the graph.
    public void DisplayInscidenceList()
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

    public void DisplayAdjacencyMatrix()
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

    public void DisplayIncidenceTable()
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
    
    // Reads a graph from a file in the specified format and initializes the internal representation.
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

            incidenceMatrix = new int[numVertices, numVertices];
            adjacencyMatrix = new int[numVertices, numVertices];
            adjacencyList = new List<List<int>>();

            for (var i = 0; i < numVertices; i++)
            {
                var lineValues = sr.ReadLine()?.Split(' ');

                adjacencyList.Add(new List<int>());

                for (var j = 0; j < numVertices; j++)
                {
                    adjacencyMatrix[i, j] = int.Parse(lineValues?[j]);
                    incidenceMatrix[i, j] = adjacencyMatrix[i, j];

                    if (adjacencyMatrix[i, j] != 0)
                    {
                        adjacencyList[i].Add(j);
                    }
                }
            }

            Console.WriteLine("Graph read successfully from the file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void SaveGraph(string filePath)
    {
        try
        {
            using var sw = new StreamWriter(filePath);

            sw.WriteLine(adjacencyList.Count);

            for (var i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    sw.Write($"{adjacencyMatrix[i, j]} ");
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