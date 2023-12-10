using System.Text;
using TrabalhoGrafos.Graph;

class Program
{
    public static void Main()
    {
        var graph = new Graph(0);
        var graphType = String.Empty;
        int choice;

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
                        break;
                }
                graph.ReadGraph($"C:/Users/iwest/RiderProjects/Trabalho-de-grafos/TrabalhoGrafos/{graphType}.txt");
                graph.DisplayInscidenceList();
                graph.DisplayAdjacencyMatrix();
                graph.DisplayIncidenceTable();
                graph.SaveGraph($"C:/Users/iwest/RiderProjects/Trabalho-de-grafos/TrabalhoGrafos/{graphType}_output.txt");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        } while (choice < 4);
    }
}