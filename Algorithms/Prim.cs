using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class Prim: IGraphAlgorithm
{
    public void Execute(Grafo grafo, GraphUtils graphUtils)
    {
        //Peso total da arvore
        int pesoTotal = 0;
        //Arestas que fazem parte da arvore
        var arestasArvorePrim = new List<Aresta>();
        //Controle para vertices visitados
        var verticesVisitados = new HashSet<Vertice>();
        //Seta vertice inicial para o algoritmo com o primeiro vertice do grafo
        Vertice verticeAtual = grafo.Vertices[0];
        //Adiciona vertice atual na lista de visitados
        verticesVisitados.Add(verticeAtual);

        while(verticesVisitados.Count < grafo.Vertices.Count){
            //Controle para aresta de menor peso
            Aresta menorPeso = null;

            foreach(var verticeVisitado in verticesVisitados){
                //Pega a lista de arestas do vertice atual da iteraçao
                //E cria uma lista ordenada por peso
                var arestasAdjacentes = verticeVisitado.Arestas.Where(
                    a => !verticesVisitados.Contains(a.LeftVertice) || !verticesVisitados.Contains(a.RightVertice))
                    .OrderBy(a => a.Peso); 
                //Pega a menor aresta da lista
                var arestaMenor = arestasAdjacentes.FirstOrDefault();
                
                //Compara a aresta da lista com a aresta menorPeso para checar
                //qual é a menor
                if(arestaMenor != null && (menorPeso == null || arestaMenor.Peso < menorPeso.Peso)){
                    menorPeso = arestaMenor;
                }

                //Adiciona a aresta da lista da arvore e soma o peso
                if(menorPeso != null){
                    arestasArvorePrim.Add(menorPeso);
                    verticeAtual = verticeAtual == menorPeso.LeftVertice ? menorPeso.RightVertice
                     : menorPeso.LeftVertice;
                    verticesVisitados.Add(verticeAtual);
                    pesoTotal += menorPeso.Peso;
                }
            }
        }

        //Imprimindo a lista de arestas e o peso total
        Console.WriteLine("\nArestas:");
        foreach(var aresta in arestasArvorePrim){
            Console.Write($"({aresta.LeftVertice},{aresta.RightVertice})");
        }
        Console.WriteLine($"\nPeso:{pesoTotal}");
    }
    
    // Not Used
    public void Execute(Grafo grafo, GraphUtils graphUtils, int startVertex)
    {
        throw new NotImplementedException();
    }
}