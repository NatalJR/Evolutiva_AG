using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Evolutiva_AG_SharpC {
    class Program {
        public static Random rnd = new Random(123);
        static void Main(string[] args) {
            int popSize = 10;
            int generations = 0, generationsWithoutImprovement = 0;
            int totalFitness = 0, sorteio = 0;

            List<Cromosome> population = new List<Cromosome>();
            List<Cromosome> descendents = new List<Cromosome>();
            int[] accFitness = new int[popSize];
            int[,] pairs = new int[popSize / 2, 2];

            GenerateInitialPopulation(population, popSize, rnd);
            GenerateInitialDescendents(descendents, popSize, rnd);

            Console.WriteLine("mean fitness: " + MeanFitness(population, popSize));

            //Parada = 32 geracoes sem melhora
            while (generationsWithoutImprovement < 5000){

                //Selecao

                {//for (int i = 0; i < popSize/2; i++)
                 //{
                 //    pairs[i,0] = rnd.Next(popSize);
                 //    pairs[i,1] = rnd.Next(popSize);
                 //    while (pairs[i, 1] == pairs[i, 0]){
                 //        pairs[i, 1] = rnd.Next(popSize);
                 //    }
                 //    //Console.WriteLine(pairs[i, 0] + " " + pairs[i, 1]);
                 //}
                }

                //tentativa de fazer sorteio com fitness
                totalFitness = 0;
                for (int i = 0; i < popSize; i++){
                    totalFitness += population[i].Fitness;
                    accFitness[i] = totalFitness;
                    //Console.WriteLine(accFitness[i]);
                }

                int match = 0, pair = 0;
                for (int i = 0; i < popSize; i++)
                {
                    sorteio = rnd.Next(totalFitness);
                    if (sorteio <= accFitness[0])
                    {
                        pairs[pair, match] = 0;
                    }
                    else
                    {
                        for (int j = 1; j < popSize; j++)
                        {
                            if (sorteio > accFitness[j - 1] && sorteio <= accFitness[j])
                            {
                                pairs[pair, match] = j;
                                break;
                            }
                        }
                    }
                    if (match == 1) {
                        while (pairs[pair, 0] == pairs[pair, 1])
                        {
                            sorteio = rnd.Next(totalFitness);
                            if (sorteio <= accFitness[0])
                            {
                                pairs[pair, match] = 0;
                            }
                            else
                            {
                                for (int j = 1; j < popSize; j++)
                                {
                                    if (sorteio > accFitness[j - 1] && sorteio <= accFitness[j])
                                    {
                                        pairs[pair, match] = j;
                                        break;
                                    }
                                }
                            }
                        }
                        match = 0;
                        pair++;
                    } else {
                        match = 1;
                    }
                }

                //cruzamento
                for (int i = 1, k = 0; i < popSize-1 && k<popSize/2; i+=2, k++){
                    if (rnd.NextDouble() <= 0.8)
                    {
                        descendents[i-1].Gene1 = population[pairs[k, 0]].Gene1;
                        descendents[i-1].Gene2 = population[pairs[k, 1]].Gene2;

                        descendents[i].Gene1   = population[pairs[k, 1]].Gene1;
                        descendents[i].Gene2   = population[pairs[k, 0]].Gene2;
                    }
                    //mutacao
                    for (int j = 0; j < descendents[i].Size/2; j++){
                        if (rnd.NextDouble()<=2) {
                            descendents[i-1].Gene1[j] = !descendents[i-1].Gene1[j];
                            descendents[i-1].Gene2[j] = !descendents[i-1].Gene2[j];
                            descendents[i]  .Gene1[j] = !descendents[i].Gene1[j];
                            descendents[i]  .Gene2[j] = !descendents[i].Gene2[j];
                        }
                    }
                }

                //avaliacao
                for (int i = 0; i < popSize; i++)
                {
                    descendents[i].Evaluate();
                    population[i].Evaluate();
                }

                generations++;
                if (MeanFitness(descendents, popSize) < MeanFitness(population, popSize))
                {                    
                    generationsWithoutImprovement++;
                }
                else
                {
                    generationsWithoutImprovement = 0;
                    for (int i = 0; i < popSize; i++)
                    {
                        for (int j = 0; j < population[i].Size / 2; j++)
                        {   
                            population[i].Gene1[j] = descendents[i].Gene1[j];
                            population[i].Gene2[j] = descendents[i].Gene2[j];
                        }
                    }
                    Console.WriteLine("generation: " + generations + " mean fitness (new): " + MeanFitness(population, popSize));
                }
            }
            Console.WriteLine("generations wOut imp: " + generationsWithoutImprovement);
            Console.WriteLine("mean fitness (desc): " + MeanFitness(population, popSize));
            Console.WriteLine("Pressione uma tecla");
            Console.ReadKey();
        }

        public static void PrintGene(BitArray gene, int myWidth)
        {
            int i, width = myWidth;
            Console.WriteLine("Gene: ");
            for (i = 0; i < gene.Count; i++, width--)
            {
                if (width == 0)
                {
                    width = myWidth;
                    Console.WriteLine();
                }
                if (gene[i] == true)
                {
                    Console.Write("1 ");
                }
                else
                {
                    Console.Write("0 ");
                }

            }
            Console.WriteLine();
        }
        public static void GenerateInitialPopulation(List<Cromosome> population, int popSize, Random rnd)
        {
            for (int i = 0; i < popSize; i++)
            {
                population.Add(new Cromosome(512));
                population[i].Initialize(rnd);
            }
        }
        public static void GenerateInitialDescendents(List<Cromosome> desc, int popSize, Random rnd)
        {
            for (int i = 0; i < popSize; i++)
            {
                desc.Add(new Cromosome(512));
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
    }

}
