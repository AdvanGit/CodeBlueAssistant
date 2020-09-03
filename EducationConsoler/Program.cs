using System;

namespace EducationConsoler
{
    class Program
    {

        public delegate void  MyDelegate();
        static void Main(string[] args)
        {
            MyDelegate del = new MyDelegate(WriteDisplay);

            del();




        }

        static void WriteDisplay()
        {
            Console.WriteLine("Runaway");
        }
    }
}
