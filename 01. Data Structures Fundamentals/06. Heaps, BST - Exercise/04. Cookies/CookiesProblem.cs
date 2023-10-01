using System;
using System.Linq;
using _03.MinHeap;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int minSweetness, int[] cookies)
        {
            OrderedBag<int> bag = new OrderedBag<int>();
            bag.AddMany(cookies);

            int steps = 0;
            int currentSweetness = bag.GetFirst();

            while(currentSweetness < minSweetness && bag.Count > 1)
            {
                int firstCookie = bag.RemoveFirst();
                int secondCookie = bag.RemoveFirst();

                int newCookie = firstCookie + 2 * secondCookie;
                bag.Add(newCookie);
                currentSweetness = bag.GetFirst();

                steps++;
            }

            return currentSweetness < minSweetness ? - 1 : steps;
        }
    }
}
