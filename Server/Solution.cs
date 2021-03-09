using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Solution
    {
        private int rootNumber;
        private double root1, root2;

        public int GetRootNumber()
        {
            return rootNumber;
        }

        public double GetRoot1()
        {
            return root1;
        }

        public double GetRoot2()
        {
            return root2;
        }

        public Solution(int rootNumber, double root1, double root2)
        {
            this.rootNumber = rootNumber;
            this.root1 = root1;
            this.root2 = root2;
        }
    }
}
