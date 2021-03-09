using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Solver
    {
        static public Solution Solve(double A, double B, double C)
        {
            int rootNumber = 0;
            double root1 = 0;
            double root2 = 0;

            if ((A == 0) && (C == 0))
            {
                rootNumber = 1;
                root1 = 0;
                return new Solution(rootNumber, root1, root2);
            }

            if ((B == 0) && (C == 0))
            {
                rootNumber = 1;
                root1 = 0;
                return new Solution(rootNumber, root1, root2);
            }

            if ((A == 0) && (B == 0))
            {
                rootNumber = 0;
                return new Solution(rootNumber, root1, root2);
            }

            if (A == 0)
            {
                rootNumber = 1;
                root1 = (-1) * C / B;
                return new Solution(rootNumber, root1, root2);
            }

            if (B == 0)
            {
                rootNumber = 2;
                root1 = (-1) * C / A;
                if (root1 < 0)
                {
                    rootNumber = 0;
                    return new Solution(rootNumber, root1, root2);
                }
                else
                {
                    root1 = Math.Sqrt(root1);
                    root2 = (-1) * root1;
                }
            }

            double D = B * B - 4 * A * C;

            if (D > 0)
            {
                rootNumber = 2;
                root1 = (-B - Math.Sqrt(D)) / (2 * A);
                root2 = (-B + Math.Sqrt(D)) / (2 * A);
            }

            if (D == 0)
            {
                rootNumber = 1;
                root1 = -B / (2 * A);
            }

            if (D < 0)
            {
                rootNumber = 0;
            }

            return new Solution(rootNumber, root1, root2);
        }
    }
}
