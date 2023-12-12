using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph;

public interface IGraphAlgorithm
{
    void Execute(Grafo grafo, GraphUtils graphUtils, int startVertex);
    void Execute(Grafo grafo, GraphUtils graphUtils);
}