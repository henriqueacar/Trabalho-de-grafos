using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class DFS: IGraphAlgorithm
{
    public void Execute(Grafo grafo, int startVertex)
    {
        Grafo copia = grafo;
        List<Vertice> verticesVisitados = new List<Vertice>();
        Stack<Vertice> pilha = new Stack<Vertice>(); //pilha para controlar ordem de visita
        foreach(Vertice v in copia.Vertices){
            v.Pai = null; //limpa informação de visitação
        }

        pilha.Push(copia.Vertices[startVertex]);

        while (pilha.Count > 0){
            var verticeAtual = pilha.Pop();
            //tira um vertice da pilha para checar se ele está na lista de
            //vertices visitados, caso nao esteja entao entro nele
            if(!verticesVisitados.Contains(verticeAtual)){
                verticesVisitados.Add(verticeAtual); //adiciono na lista
                foreach(var aresta in verticeAtual.Arestas){
                    //checa se o verticeAtual é igual vertice da esquerda ou direita da aresta
                    //Se for da esquerda, o vizinho é o da direita. Se for da direita, vizinho é da esquerda
                    
                    var vizinho = aresta.LeftVertice == verticeAtual ? 
                    aresta.RightVertice : aresta.LeftVertice;
                    //Determino o vizinho e checo se ele já foi visitado
                    //Caso contrário, adiciono a pilha
                    if(vizinho.Pai == null){
                        pilha.Push(vizinho);
                        vizinho.Pai = verticeAtual;
                    }
                }
            }
        }

        Console.WriteLine("\nVertices visitados:");
        foreach(var vertice in verticesVisitados){
            Console.Write($"{vertice}");
        }
        Console.WriteLine("\n");
    }
    
    // Not Used
    public void Execute(Grafo grafo, GraphUtils graphUtils)
    {
        throw new NotImplementedException();
    }
}