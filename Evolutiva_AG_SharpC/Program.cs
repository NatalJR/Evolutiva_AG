using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolutiva_AG_SharpC {
    class Program {
        static void Main(string[] args) {
            Cromosome cromosome = new Cromosome();
            
            cromosome.Gene1 = new BitArray(256);
            cromosome.Gene2 = new BitArray(256);
            
            //Populacao inicial aleatoria + Fitness inicial
            cromosome.Initialize();

            //Console.WriteLine(cromosome.Fitness);
            //PrintGene(cromosome.Gene1, 32);
            //PrintGene(cromosome.Gene2, 32);

            Console.WriteLine("Pressione uma tecla");
            Console.ReadKey();

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
