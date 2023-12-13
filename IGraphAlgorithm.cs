using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph;

public interface IGraphAlgorithm
{
    void Execute(Grafo grafo, int startVertex);
    void Execute(Grafo grafo, GraphUtils graphUtils);
}