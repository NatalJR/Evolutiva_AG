using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolutiva_AG_SharpC {
    class Program {
        static void Main(string[] args) {
            int popSize = 10;
            int generations = 0, generationsWithoutImprovement = 0,
                totalFitness = 0, sorteio = 0;
            List<Cromosome> population = new List<Cromosome>();
            List<Cromosome> descendents = new List<Cromosome>();
            int[] accFitness = new int[popSize];
            int[,] pairs = new int[popSize / 2, 2];
            Random rnd = new Random();

            GenerateInitialPopulation(population, popSize);
            GenerateInitialPopulation(descendents, popSize);
            Console.WriteLine("mean fitness: "+MeanFitness(population, popSize));

            //Parada = 32 geracoes sem melhora
            while (generationsWithoutImprovement < 100){
                //Selecao
                for (int i = 0; i < popSize/2; i++)
                {
                    pairs[i,0] = rnd.Next(0, popSize);
                    pairs[i,1] = rnd.Next(0, popSize);
                    while (pairs[i, 1] == pairs[i, 0]){
                        pairs[i, 1] = rnd.Next(0, popSize);
                    }
                    //Console.WriteLine(pairs[i, 0] + " " + pairs[i, 1]);
                }

                //tentativa de fazer sorteio com fitness
                /* accFitness[0] = population[0].Fitness;
                 for (int i = 1; i < popSize; i++){
                     totalFitness += population[i].Fitness;
                     accFitness[i] = totalFitness;
                    //Console.WriteLine(accFitness[i]);
                 }

                /*for (int i = 0; i < popSize/2; i++){
                     sorteio = rnd.Next(0, totalFitness);
                     if (Enumerable.Range(0, accFitness[0]).Contains(sorteio)){
                         pairs[i, 0] = 0;
                     } else{
                         for (int j = 1; j < popSize; j++){
                             if (Enumerable.Range(accFitness[j - 1]+1, accFitness[j]).Contains(sorteio)){
                                 pairs[i, 0] = j;
                                 break;
                             }
                         }
                     }

                     sorteio = rnd.Next(0, totalFitness);
                     if (Enumerable.Range(0, accFitness[0]).Contains(sorteio))
                     {
                         pairs[i, 1] = 0;
                     }
                     else
                     {
                         for (int j = 1; j < popSize; j++)
                         {
                             if (Enumerable.Range(accFitness[j - 1] + 1, accFitness[j]).Contains(sorteio))
                             {
                                 pairs[i, 1] = j;
                                 break;
                             }
                         }
                     }
                 }*/

                //cruzamento

                for (int i = 0; i < (popSize/2)-1; i++){
                    if (rnd.NextDouble()*(100-0)+0 <= 80)
                    {
                        descendents[i].Gene1        = population[pairs[i, 0]].Gene1;
                        descendents[i].Gene2        = population[pairs[i, 1]].Gene2;
                        descendents[i + 1].Gene1    = population[pairs[i, 1]].Gene1;
                        descendents[i + 1].Gene2    = population[pairs[i, 0]].Gene2;
                    }
                    //mutacao
                    for (int j = 0; j < descendents[i].Size/2; j++){
                        if (rnd.NextDouble()*(100-0)+0<=2) {
                            descendents[i].Gene1[j]     = !descendents[i].Gene1[j];
                            descendents[i].Gene2[j]     = !descendents[i].Gene2[j];
                            descendents[i+1].Gene1[j]   = !descendents[i+1].Gene1[j];
                            descendents[i+1].Gene2[j]   = !descendents[i + 1].Gene2[j];
                        }
                    }
                }

                for (int i = 0; i < popSize; i++)
                {
                    descendents[i].Evaluate();
                    population[i].Evaluate();
                }

                generations++;
                if (MeanFitness(descendents, popSize) < MeanFitness(population, popSize))
                    generationsWithoutImprovement++;
                else
                {
                    for (int i = 0; i < popSize; i++)
                    {
                        population[i] = descendents[i];
                    }
                    //population = descendents;                
                }
                
            }

            Console.WriteLine("mean fitness (desc): " + MeanFitness(descendents, popSize));

            /*Cromosome cromosome = new Cromosome();
            cromosome.Gene1 = new BitArray(256);
            cromosome.Gene2 = new BitArray(256);
            //Populacao inicial aleatoria + Fitness inicial
            cromosome.Initialize();
            Console.WriteLine(cromosome.Fitness);
            PrintGene(cromosome.Gene1, 32);
            PrintGene(cromosome.Gene2, 32);*/

            Console.WriteLine("Pressione uma tecla");
            Console.ReadKey();

        }


        public static void GenerateInitialPopulation(List<Cromosome> population, int popSize)
        {
            for (int i = 0; i < popSize; i++)
            {
                population.Add(new Cromosome(512));
                population[i].Initialize();
            }
        }

        public static double MeanFitness(List<Cromosome> pop, int popSize)
        {
            double mean = 0;
            for (int i = 0; i < popSize; i++)
            {
                mean += pop[i].Fitness;
            }
            return mean/popSize;
        }
        
        public static void PrintGene(BitArray gene, int myWidth)
        {
            int i, width=myWidth;
            Console.WriteLine("Gene: ");
            for (i = 0; i < gene.Count; i++, width--)
            {
                if (width == 0)
                {
                    width = myWidth;
                    Console.WriteLine();
                }
                if (gene[i] == true){
                    Console.Write("1 ");
                }
                else{
                    Console.Write("0 ");
                }
                
            }
            Console.WriteLine();
        }
    }

}
