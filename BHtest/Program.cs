using System;

namespace BHtest
{
    class Program
    {
        static void Main(string[] args)
        {
        
            int i=0, a=0, b=0;
            bool te;
            int s;
            /*for (s = 0; s < 10; s++) {
                a = 0;
                b = 0;
                for (i = 0; i < 1000000; i++)
                {
                    te = Logic.RNGroll(10f);
                    if (te)
                    {
                        a++;
                    }
                    else { b++; }
                }
                Console.WriteLine(a);
                Console.WriteLine(b); }*/
            Random rnd = new Random();
            Console.WriteLine("Hello World!");
            Simulation.hellosim();
            Console.ReadLine();
        }
    }
}