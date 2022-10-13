using PudelkoLibre;
using System;

namespace PudelkoApp
{
    //public static class Utility
    //{
    //    public static Pudelko Kompresuj(this Pudelko p)
    //    {
    //        return Pudelko //(.....) 
    //    }
    //}
    class Program
    {
        static void Main(string[] args)
        {
            // Pudelko p1 = new Pudelko(2, 3, 4, UnitOfMeasure.meter);
            // Pudelko p2 = new Pudelko(300.00, 200.01, 400, UnitOfMeasure.centimeter);
            //bool a= p1.Equals(p2);
            // if (a == true)
            //     Console.WriteLine("Correct!");

            Pudelko pp = new Pudelko(1, 2, 3, UnitOfMeasure.meter);
            Pudelko pp2 = new Pudelko(2, 4, 3, UnitOfMeasure.meter);
            var pp3 = pp + pp2;
            Console.Write("A: {0} B: {1}, C: {2}, Obj {3}", pp3.A, pp3.B, pp3.C, pp3.Objętość);

           Pudelko a=  Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");
            Console.Write("A: {0} B: {1}, C: {2}, Obj {3}", a.A, a.B, a.C, a.Objętość);

        } 
    }
}
