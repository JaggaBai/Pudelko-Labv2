using PudelkoLibre;
using System;
using System.Collections.Generic;


namespace PudelkoApp
{

    static class Exten { //o *prawie* takiej samej objętości
        public static Pudelko Kompresuj(this Pudelko str)
        {
            double bok = Math.Round(Math.Pow(str.Objętość, (1.0/3.0)), 6, MidpointRounding.AwayFromZero);

            return new Pudelko(bok, bok, bok, UnitOfMeasure.meter);
        }
    }
    class Program
    {
        static void Main(string[] args)

        {   //Sprawdzanie pudełka domyślnego oraz A, B, C gettera
            Pudelko dom = new Pudelko();
            double Adom = dom.A;
            double Bdom = dom.B;
            double Cdom = dom.C;
            Console.WriteLine("Utworzone domyślne pudełko ma wymiary: A: {0}, B: {1}, C: {2}", Adom, Bdom, Cdom);

            Console.WriteLine();

            //ToString oraz domyślny "unit"- powinno wyjść "2.500 m × 9.321 m × 0.100 m"
            Pudelko sprStr = new Pudelko(2.5, 9.321, 0.1);
            string efektSprStr = sprStr.ToString();
            Console.WriteLine("Sprawdzenie działania ToString P(2.5, 9.321, 0.1), otrzymujemy: {0}", efektSprStr);

            Console.WriteLine();

            // Sprawdza Equals:
            Pudelko p1 = new Pudelko(2, 3, 4, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(300.00, 200.01, 400, UnitOfMeasure.centimeter);
            bool a = p1.Equals(p2);
            if (a == true)
                Console.WriteLine("Pudełka są takie same dla : 2, 3, 4, UnitOfMeasure.meter, i 300.00, 200.01, 400, UnitOfMeasure.centimeter");
            else Console.WriteLine("Pudełka są różne");

            Console.WriteLine();

            //Sprawdza dodawanie pudełek oraz Objętość
            Pudelko pp = new Pudelko(1, 2, 3, UnitOfMeasure.meter);
            Pudelko pp2 = new Pudelko(2, 4, 3, UnitOfMeasure.meter);
            var pp3 = pp + pp2;
            Console.Write("Nowe pudełko po dodaniu dwóch pudełek ma wymiary A: {0} B: {1}, C: {2}, Obj {3}", pp3.A, pp3.B, pp3.C, pp3.Objętość);

            Console.WriteLine();

            //Objętość
            Pudelko pudOb = new Pudelko(1, 2, 3, UnitOfMeasure.meter);
            Console.Write("Pudełko ma pole:  {0}", pudOb.Pole);
            Console.WriteLine();

            //Metoda rozszerzajaca Kompresuj zwraca pudełko sześcienne o prawie takiej samej objętości, jak pudełko oryginalne
            Pudelko kom = pp3.Kompresuj();
            Console.Write("Pudełko po kompresji ma wymiary A: {0} B: {1}, C: {2}, Obj {3}", kom.A, kom.B, kom.C, kom.Objętość);

            Console.WriteLine();

            //Sprawdza możliwość użycia Parse
            Pudelko an = Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");
            Console.Write("Sprawdza utworzenie nowego pudełka przez użycie Parse, o wymiarach: A: {0} B: {1}, C: {2}", an.A, an.B, an.C);
            Pudelko an2 = Pudelko.Parse("220 mm × 200.3888 mm × 1000 mm");
            Console.Write("Sprawdza utworzenie nowego pudełka przez użycie Parse, o wymiarach: A: {0} B: {1}, C: {2}", an2.A, an2.B, an2.C);
            Pudelko an3 = Pudelko.Parse("2 cm × 20 cm × 10 cm");
            Console.Write("Sprawdza utworzenie nowego pudełka przez użycie Parse, o wymiarach: A: {0} B: {1}, C: {2}", an3.A, an3.B, an3.C);
            Console.WriteLine();

            //Operacje konwersji
            ValueTuple<int, int, int> kon1 = (1000, 3000, 2000);
            Pudelko Pkon1 = kon1;
            Console.Write("Sprawdza utworzenie nowego pudełka przez użycie implicit konwersji, pudełko o wymiarach: A: {0} B: {1}, C: {2}", Pkon1.A, Pkon1.B, Pkon1.C);
            Console.WriteLine();
            double[] ExpKon = (double[])Pkon1;
            Console.WriteLine("Konwersja Explicit - poszczególne wymiary z double[] wyciągnięte:");
            foreach (double x in ExpKon) { Console.Write(x + " "); }

            Console.WriteLine();

            //indexer i foreach pętla [IEnumerable]
            Console.WriteLine("Użycie indeksera - wymiar A: ");
            Console.WriteLine(Pkon1[0]);
            Console.WriteLine("Użycie IEnumerable wymiary: ");
            foreach (double x in Pkon1) { Console.Write(x + " "); }
            Console.WriteLine();

            Pudelko pudL1 = new Pudelko(10, 20, 30, UnitOfMeasure.centimeter);
            Pudelko pudL2 = new Pudelko(1000, 2000, 3000, UnitOfMeasure.milimeter);

            Pudelko pudL3 = pudL2 + pudL1;
            Console.Write("Sprawdza utworzenie nowego pudełka przez użycie łączenia pudełko o wymiarach: A: {0} B: {1}, C: {2}", pudL3.A, pudL2.B, pudL3.C);
            Console.WriteLine();

            var lista = new List<Pudelko>();
            lista.Add(new Pudelko(10));
            lista.Add(new Pudelko(unit: UnitOfMeasure.centimeter));
            lista.Add(new Pudelko());
            lista.Add(new Pudelko(9, 9, 9, UnitOfMeasure.meter));
            lista.Add(new Pudelko(250, 933, 10, UnitOfMeasure.centimeter));
            lista.Add(new Pudelko(2.500, 9.32, 0.100, UnitOfMeasure.meter));
            lista.Add(new Pudelko(100, 101, 100, UnitOfMeasure.milimeter));
            lista.Add(new Pudelko(4, 2, 1, UnitOfMeasure.meter));
            lista.Add(new Pudelko(2, 2, 2, UnitOfMeasure.meter));
            Console.WriteLine("Lista przed sortowaniem:");
            foreach (var p in lista)
            {
                Console.WriteLine(p);
                
            }

            Comparison<Pudelko> pudComparer = new Comparison<Pudelko>(PComparer);
            lista.Sort(pudComparer);

            Console.WriteLine("Po sortowaniu wedle Comparer delegate: ");
            foreach (var p in lista)
            {
                Console.WriteLine(p);
         
            }
            Console.WriteLine("Lista po sortowaniu cdn. (pokazuje pierwsze kryterium, Objętość:):");
            
                foreach (var p in lista)
            {
                Console.WriteLine(p.Objętość);
            }
            Console.WriteLine("Lista po sortowaniu cdn. (pokazuje drugie kryterium Pole - dwie Objętości równe 8 są ustawione w sortowanium wedle drugiego kryterium):");
            foreach (var p in lista)
            {
                Console.WriteLine(p.Pole);
            }
            
        }
        private static int PComparer(Pudelko pp1, Pudelko pp2)
        {
            if (pp1.Objętość < pp2.Objętość || pp1.Objętość> pp2.Objętość)
            {
                return (pp1.Objętość.CompareTo(pp2.Objętość));
            }
            else
            {
                if (pp1.Pole < pp2.Pole || pp1.Pole > pp2.Pole)

                    return (pp1.Pole.CompareTo(pp2.Pole));
                else return (pp1.SumaKrawedzi.CompareTo(pp2.SumaKrawedzi));
            }
        }
        
    }
    }

