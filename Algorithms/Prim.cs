using TrabalhoGrafos.Graph.Models;

namespace TrabalhoGrafos.Graph.Algorithms;

public class Prim
{
    public void Execute(Grafo grafo)
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

        //While para construir a arvore geradora minima
        while (verticesVisitados.Count < grafo.Vertices.Count)
        {
            //Controle para aresta de menor peso
            Aresta menorAresta = null;
            int menorPeso = int.MaxValue;

            foreach (var verticeVisitado in verticesVisitados.ToList())
            {
                //Pega a lista de arestas do vertice atual da iteraçao
                //E cria uma lista ordenada por peso
                var arestasAdjacentes = verticeVisitado.Arestas.Where(a =>
                    (!verticesVisitados.Contains(a.LeftVertice) && verticesVisitados.Contains(a.RightVertice)) ||
                    (!verticesVisitados.Contains(a.RightVertice) && verticesVisitados.Contains(a.LeftVertice))
                ).OrderBy(a => a.Peso);

                //Pega aresta com o menor peso da lista
                var arestaMenor = arestasAdjacentes.FirstOrDefault();

                //Atribui a aresta de menor peso da lista na variavel de controle
                if (arestaMenor != null && arestaMenor.Peso < menorPeso)
                {
                    menorAresta = arestaMenor;
                    menorPeso = arestaMenor.Peso;
                }
            }

            //Adiciona a aresta na arvore geradora e calcula o peso total
            if (menorAresta != null)
            {
                arestasArvorePrim.Add(menorAresta);
                pesoTotal += menorAresta.Peso;
                //Verifica se o LeftVertice da menorAresta já foi visitado
                //Caso sim, atribui verticeAtual com o rightVertice
                //Caso nao, atribui verticeAtual com o leftVertice
                verticeAtual = verticesVisitados.Contains(menorAresta.LeftVertice)
                    ? menorAresta.RightVertice
                    : menorAresta.LeftVertice;
                //Adiciona o vertice na lista de verticesVisitados
                verticesVisitados.Add(verticeAtual);
            }
        }

        //Imprimindo a lista de arestas e o peso total
        Console.WriteLine("\nArestas:");
        foreach (var aresta in arestasArvorePrim)
        {
            Console.Write($"({aresta.LeftVertice},{aresta.RightVertice})");
        }

        Console.WriteLine($"\nPeso:{pesoTotal}\n");
    }
}