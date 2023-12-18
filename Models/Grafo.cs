namespace TrabalhoGrafos.Graph.Models;

public class Grafo
{
    public List<Vertice> Vertices = new();
    public List<Aresta> Arestas = new();

    public Grafo CriarGrafo(int[,] matriz)
    {
        // Pega o tamanho da matriz com base no numero de elementos da primeira linha
        var numeroVertices = matriz.GetLength(0);

        Grafo grafo = new Grafo();
        
        for (var i = 0; i < numeroVertices; i++)
        {
            // Cria todos os vertices com nome igual ao numero do vertice
            var vertice = new Vertice(i.ToString());
            
            // Adiciona esse vertice no grafo
            grafo.Vertices.Add(vertice);
        }

        // Percorre a matriz
        for (var i = 0; i < numeroVertices; i++)
        {
            for (var j = 0; j < numeroVertices; j++)
            {
                // Verifica se é um valor maior que 0 e menor que 999 para grafos valorados
                if (matriz[i, j] > 0 && matriz[i, j] < 999)
                {
                    // Cria uma aresta com o vertice de "origem" e "destino" 
                    grafo.Arestas.Add(new Aresta(grafo.Vertices[i], grafo.Vertices[j]));
                    
                    // Pega o vertice vizinho
                    var verticeVizinho = grafo.Vertices[j];
                    
                    // Adiciona ao vertice o seu vertice vizinho
                    grafo.Vertices[i].Vizinhos.Add(verticeVizinho);
                }
            }
        }

        return grafo;
    }
}