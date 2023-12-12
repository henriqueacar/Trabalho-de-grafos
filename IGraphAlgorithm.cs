namespace TrabalhoGrafos.Graph;

public interface IGraphAlgorithm
{
    void Execute(GraphUtils graphUtils, int startVertex);
    void Execute(GraphUtils graphUtils);
}