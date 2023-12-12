using System.Text;
using TrabalhoGrafos.Graph;
using TrabalhoGrafos.Graph.Models;


class Program
{
    public static void Main()
    {
        var graph = new GraphUtils(0);
        var graphType = String.Empty;
        int choice;
        var grafo = new Grafo();

        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Simple Graph");
            Console.WriteLine("2. Simple Digraph");
            Console.WriteLine("3. Valued Simple Digraph");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        graphType = "GrafoSimples";
                        break;
                    case 2:
                        graphType = "DigrafoSimples";
                        break;
                    case 3:
                        graphType = "DigrafoSimplesValorado";
                        break;
                    case 4:
                        Console.WriteLine("Exiting the program. Press Enter to close.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        continue;
                }

                graph.ReadGraph($"./../../../{graphType}.txt");
                
                var matrizAdjacencia = graph.TransformToAdjacencyMatrix();
                
                grafo.CriarGrafo(matrizAdjacencia);
                grafo.ExibirGrafo();
                
                GraphUtils.DisplayAdjacencyList(graph.TransformToAdjacencyList());
                GraphUtils.DisplayAdjacencyMatrix(graph.TransformToAdjacencyMatrix());
                GraphUtils.DisplayIncidenceTable(graph.TransformToIncidenceMatrix());

                Console.WriteLine("\nAlgorithm Options:");
                Console.WriteLine("5. Breadth-First Search (BFS)");
                Console.WriteLine("6. Depth-First Search (DFS)");
                Console.WriteLine("7. Prim's Algorithm");
                Console.WriteLine("8. Dijkstra's Algorithm");
                Console.WriteLine("9. Topological Sort");
                Console.WriteLine("10. Eulerian Cycle");
                Console.WriteLine("11. Back to Main Menu");

                Console.Write("Enter your choice (5-11): ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    var initialVertex = new int();
                    
                    switch (choice)
                    {
                        case 5:
                            Console.WriteLine("Choose the initial vertex.");
                            initialVertex = int.Parse(Console.ReadLine());
                            graph.ExecuteBFS(initialVertex);

                            break;
                        case 6:
                            Console.WriteLine("Choose the initial vertex.");
                            initialVertex = int.Parse(Console.ReadLine());
                            graph.ExecuteDFS(initialVertex);

                            break;
                        case 7:
                            Console.WriteLine("Choose the initial vertex.");
                            initialVertex = int.Parse(Console.ReadLine());
                            graph.ExecuteDijkstra(initialVertex);

                            break;
                        case 8:
                            graph.ExecutePrim();
                            break;
                        case 9:
                            graph.ExecuteTopologicalSort();
                            break;
                        case 10:
                            graph.ExecuteEulerianCycle();
                            break;
                        case 11:
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 5 and 11.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        } while (choice != 4);
    }
}