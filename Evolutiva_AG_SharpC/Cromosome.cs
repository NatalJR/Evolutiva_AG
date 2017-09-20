using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace Evolutiva_AG_SharpC{
    class Cromosome{
        BitArray _gene1;
        BitArray _gene2;
        int _size;
        int _fitness;

        public BitArray Gene1 { get => _gene1; set => _gene1 = value; }
        public BitArray Gene2 { get => _gene2; set => _gene2 = value; }
        public int Fitness { get => _fitness; set => _fitness = value; }
        public int Size { get => _size; set => _size = value; }

        public Cromosome(){
            Size = 512;
            Fitness = 0;
            Gene1 = new BitArray(256);
            Gene2 = new BitArray(256);
        }

        public Cromosome(int size){
            Size = size;
            Fitness = 0;
            Gene1 = new BitArray(size / 2);
            Gene2 = new BitArray(size / 2);
        }

        public void Initialize(Random rnd){
            for (int i = 0; i < Gene1.Length; i++)
            {
                Gene1[i] = rnd.Next(100)>=80 ? true : false;
                Gene2[i] = rnd.Next(100)>=80 ? true : false;
            }
            Evaluate();
        }

        public void Evaluate(){
            int fitness=0;
            for (int i = 0; i < Gene1.Count; i++){
                if (Gene1[i]){
                    fitness++;
                }
                if (Gene2[i]){
                    fitness++;
                }
            }
            this.Fitness = fitness;
        }
    }
}
