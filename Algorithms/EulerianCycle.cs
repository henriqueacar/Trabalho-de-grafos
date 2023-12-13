using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class EulerianCycle: IGraphAlgorithm
{
    public void Execute(Grafo grafo, GraphUtils graphUtils)
    {
        //Nao funciona com grafo desconexo
        var cicloEuleriano = new List<Aresta>();

        if(!ChecaCondicaoCicloEuleriano(grafo)){
            Console.WriteLine("Grafo não possui ciclo euleriano pois nem todos os vértices tem grau par.");
            return; //não sei bem como interromper a operação caso nao haja ciclo
        }

        Vertice verticeInicial = grafo.Vertices[0]; //vertice para iniciar busca pelo ciclo        

        BuscaCicloEuleriano(grafo, verticeInicial, cicloEuleriano);

        Console.WriteLine("\nArestas:");
        foreach(var aresta in cicloEuleriano){
            Console.Write($"({aresta.LeftVertice},{aresta.RightVertice})");
        }
        Console.WriteLine("\n");

    }

    public void BuscaCicloEuleriano(Grafo grafo, Vertice vertice, 
    List<Aresta> cicloEuleriano)
    {
        foreach(var aresta in vertice.Arestas.ToList()){
            //checa as arestas na lista de arestas do vertice atual
            vertice.Arestas.Remove(aresta);
            //procura na aresta atual quais os vertices ligados nela e acha o vizinho
            //do vertice atual
            var vizinho = aresta.LeftVertice == vertice ? 
                    aresta.RightVertice : aresta.LeftVertice;
            vizinho.Arestas.Remove(aresta);
            //Se a lista do cicloEuleriano não contem a aresta atual, adiciona ela
            if(!cicloEuleriano.Contains(aresta))
                cicloEuleriano.Add(aresta);
            //envia o vertice vizinho encontrado e repete o processo
            BuscaCicloEuleriano(grafo, vizinho, cicloEuleriano);
        }
    
    }


    public bool ChecaCondicaoCicloEuleriano(Grafo grafo){
        if(grafo.Vertices.Any(v => v.Arestas.Count % 2 != 0))
        {
            return false;
        }

        return true;
    }
    
    // Not Used
    public void Execute(Grafo grafo, GraphUtils graphUtils, int startVertex)
    {
        throw new NotImplementedException();
    }

}