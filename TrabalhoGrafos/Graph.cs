namespace TrabalhoGrafos.Graph;

class Graph
{
    private int[,] incidenceMatrix;
    private int[,] adjacencyMatrix;
    private List<List<int>> adjacencyList;

    public Graph(int numVertices)
    {
        incidenceMatrix = new int[numVertices, numVertices];
        adjacencyMatrix = new int[numVertices, numVertices];
        adjacencyList = new List<List<int>>(numVertices);

        for (int i = 0; i < numVertices; i++)
        {
            adjacencyList.Add(new List<int>());
        }
    }

    public void ReadGraph(string filePath)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                int numVertices = int.Parse(sr.ReadLine());

                if (numVertices <= 0)
                {
                    Console.WriteLine("Invalid number of vertices in the file.");
                    return;
                }

                incidenceMatrix = new int[numVertices, numVertices];
                adjacencyMatrix = new int[numVertices, numVertices];
                adjacencyList = new List<List<int>>();

                for (int i = 0; i < numVertices; i++)
                {
                    string[] lineValues = sr.ReadLine().Split(' ');

                    adjacencyList.Add(new List<int>());

                    for (int j = 0; j < numVertices; j++)
                    {
                        adjacencyMatrix[i, j] = int.Parse(lineValues[j]);

                        if (adjacencyMatrix[i, j] == 1)
                        {
                            incidenceMatrix[i, j] = 1;
                            incidenceMatrix[j, i] = 1; // For undirected graphs
                            adjacencyList[i].Add(j);
                        }
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

    public void DisplayInscidenceList()
    {
        Console.WriteLine("Adjacency List:");

        for (int i = 0; i < adjacencyList.Count; i++)
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
        
        int rows = adjacencyMatrix.GetLength(0);
        int cols = adjacencyMatrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
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
        for (int j = 0; j < incidenceMatrix.GetLength(1); j++)
        {
            Console.Write($"({(char)('A' + j)})\t");
        }
        Console.WriteLine();

        for (int i = 0; i < incidenceMatrix.GetLength(0); i++)
        {
            Console.Write($"{(char)('A' + i)}\t");
            for (int j = 0; j < incidenceMatrix.GetLength(1); j++)
            {
                Console.Write($" {incidenceMatrix[i, j]}\t");
            }
            Console.WriteLine();
        }
    }
}