using PudelkoLibre;
using System;
using System.Collections.Generic;


namespace PudelkoApp
{

    //    Metody rozszerzające

    //W projekcie typu Console App utwórz metodę rozszerzającą klasę Pudelko o nazwie Kompresuj, która zwraca pudełko sześcienne o takiej samej objętości, jak pudełko oryginalne.

    static class Exten {
        public static Pudelko Kompresuj(this Pudelko str)
        {
            double bok = Math.Pow(str.Objętość, (1.0/3.0));

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

            //ToString - powinno wyjść "2.500 m × 9.321 m × 0.100 m"
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

            //Metoda rozszerzajaca Kompresuj zwraca pudełko sześcienne o takiej samej objętości, jak pudełko oryginalne
            Pudelko kom = pp3.Kompresuj();
            Console.Write("Pudełko po kompresji ma wymiary A: {0} B: {1}, C: {2}, Obj {3}", kom.A, kom.B, kom.C, kom.Objętość);

            Console.WriteLine();

            //Sprawdza możliwość użycia Parse
            Pudelko an=  Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");
            Console.Write("Sprawdza utworzenie nowego pudełka przez użycie Parse, o wymiarach: A: {0} B: {1}, C: {2}", an.A, an.B, an.C);

            Console.WriteLine();

          Pudelko pudOb = new Pudelko(1111, 211, 133, UnitOfMeasure.milimeter);
            double wynik = pudOb.Objętość;
            Console.WriteLine(wynik);
            //        Sortowanie pudełek

            //W funkcji Main programu głównego(aplikacja konsolowa) utwórz listę kliku różnych pudełek, używając różnych wariantów konstruktora.
            //Wypisz pudełka umieszczone na liście(po jednym w wierszu).
            //Posortuj tę listę według następującego kryterium: p1 poprzedza p2 jeśli:
            //    objętość p1<objętość p2
            //    jeśli objętości są równe, to decyduje pole powierzchni całkowitej,
            //    jeśli również pola powierzchni całkowitej są równe, to decyduje suma długości krawędzi A+B + C.
            //Kryterium sortowania dostarcz jako delegat Comparison<Pudelko>.
            //Wypisz listę posortowaną.
            ////Użycie lambdy tu ma być

            var lista = new List<Pudelko>();
            lista.Add(new Pudelko(10));
            lista.Add(new Pudelko(unit: UnitOfMeasure.centimeter));
           

            foreach (var p in lista)
                Console.WriteLine(p);

        }
    }
}
