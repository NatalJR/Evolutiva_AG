using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Evolutiva_AG_SharpC{
    class Cromosome{
        BitArray _gene1;
        BitArray _gene2;
        int _fitness;

        public BitArray Gene1 { get => _gene1; set => _gene1 = value; }
        public BitArray Gene2 { get => _gene2; set => _gene2 = value; }
        public int Fitness { get => _fitness; set => _fitness = value; }

        public void Initialize()
        {
            Random rnd = new Random();
            for (int i = 0; i < Gene1.Count; i++)
            {
                Gene1[i] = rnd.Next(0, 100) > 80 ? true : false;
                Gene2[i] = rnd.Next(0, 100) > 80 ? true : false;
            }
            Evaluate();
        }

        public int Evaluate()
        {
            Fitness=0;
            for (int i = 0; i < Gene1.Count; i++)
            {
                if (Gene1[i])
                {
                    Fitness++;
                }
                if (Gene2[i])
                {
                    Fitness++;
                }
            }
            return Fitness;
        }

    }
}
