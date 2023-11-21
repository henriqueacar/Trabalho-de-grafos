using System.Text;
using TrabalhoGrafos.Graph;

class Program
{
    public static void Main()
    {
        string filePath =
            Path.GetFullPath("/Users/igorwestermann/Dev/UFJF/Grafos/Trabalho-de-grafos/TrabalhoGrafos/TestFile.txt");

        Graph graph = new Graph(0);

        graph.ReadGraph(filePath);
        graph.DisplayInscidenceList();
        graph.DisplayAdjacencyMatrix();
        graph.DisplayIncidenceTable();

        Console.ReadLine();
    }
}