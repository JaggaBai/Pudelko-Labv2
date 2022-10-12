    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;


namespace PudelkoLibre
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
    {

       private readonly UnitOfMeasure unit2;
                           //Zaimplementuj properties o nazwach A, B i C zwracające wymiary pudełka w metrach(z dokładnością do 3 miejsc po przecinku).
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        public double A
        {
            get
            {

                return _a;
            }

        }
        public double B
        {
            get
            {
                return _b;
            }

        }
        public double C
        {
            get
            { return _c; }

        }
        public double Zmiana(double x, UnitOfMeasure z)
        {
            if (z == UnitOfMeasure.meter)
            {
                x = Convert.ToDouble(x.ToString("#.###"));
            }
            else if (z == UnitOfMeasure.centimeter)
            {
               x=  Convert.ToDouble((x/100).ToString("#.###"));
            }
            else if (z == UnitOfMeasure.milimeter)
            {
                x = Convert.ToDouble((x / 1000).ToString("#.###"));
              //  x = Math.Round((x/ 1000), 3);
            }
            return x;
        }

        public string WykopujemyMiejscaPoPrzecinku(double z, UnitOfMeasure mm)
        {
            
            double zz=0;
            
                if (mm == UnitOfMeasure.meter)
                {
                    zz = Math.Round((z), 3, MidpointRounding.ToZero);
                    //zwrot = z.ToString("#.###");
                }
                // zwrot = zz.ToString("F3", CultureInfo.CurrentCulture); }
                else if (mm == UnitOfMeasure.centimeter)
                {
                    zz = Math.Round(((z)), 1, MidpointRounding.ToZero);
                    //zwrot = z.ToString("#.#");
                }
                else if (mm == UnitOfMeasure.milimeter)
                {
                    zz = Math.Round(((z)), 0, MidpointRounding.ToZero);
                    // zwrot = z.ToString("#.");
                }

            return zz.ToString();
               
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            UnitOfMeasure u = unit;
            // Wszystkie parametry konstruktora są opcjonalne.
            //Jeśli podano mniej niż 3 wartości liczbowe, pozostałe przyjmuje się jako o wartości 10 cm, ale dla ustalonej jednostki miary.
            if (!a.HasValue & u == UnitOfMeasure.milimeter)
            {
                a = 100;
            }
            else if (!a.HasValue & u == UnitOfMeasure.centimeter)
            {
                a = 10;
            }
            else if (!a.HasValue & u == UnitOfMeasure.meter)
            { a = 0.1; }

            if (!b.HasValue & u == UnitOfMeasure.milimeter)
            {
                b = 100;
            }
            else if (!b.HasValue & u == UnitOfMeasure.centimeter)
            {
                b = 10;
            }
            else if (!b.HasValue & u == UnitOfMeasure.meter)
            { b = 0.1; }

            if (!c.HasValue & u == UnitOfMeasure.milimeter)
            {
                c = 100;
            }
            else if (!c.HasValue & u == UnitOfMeasure.centimeter)
            {
                c = 10;
            }
            else if (!c.HasValue & u == UnitOfMeasure.meter)
            { c = 0.1; }
            double a1 = Convert.ToDouble(WykopujemyMiejscaPoPrzecinku((double)a, u));
            double b1 = Convert.ToDouble(WykopujemyMiejscaPoPrzecinku((double)b, u));
            double c1 = Convert.ToDouble(WykopujemyMiejscaPoPrzecinku((double)c, u));
            //double a1 = (double)a;
            // double b1 = (double)b;
            // double c1 = (double)c;
            //W przypadku próby utworzenia pudełka z którymkolwiek z parametrów niedodatnim, zgłaszany jest wyjątek ArgumentOutOfRangeException
            if ((a1 <= 0 | b1 <= 0 | c1 <= 0) || ((u == UnitOfMeasure.milimeter) & ((a1 > 10000) | (b1 > 10000) | (c1 > 10000))) || ((u == UnitOfMeasure.centimeter) & ((a1 > 1000) | (b1 > 1000) | (c1 > 1000))) || ((u == UnitOfMeasure.meter) & ((a1 > 10) | (b1 > 10) | (c1 > 10))))
            { throw new ArgumentOutOfRangeException(); }
            //W przypadku próby utworzenia pudełka z którymkolwiek z parametrów większym niż 10 metrów, zgłaszany jest wyjątek ArgumentOutOfRangeException.

           // { throw new ArgumentOutOfRangeException(); }
          
            //double a12 = WykopujemyMiejscaPoPrzecinku(a1, u);
            //double b12 = WykopujemyMiejscaPoPrzecinku(b1, u);
            //double c12 = WykopujemyMiejscaPoPrzecinku(c1, u);
            _a = Zmiana(a1, u) ;
            _b = Zmiana(b1, u);
            _c = Zmiana(c1, u);
            unit2 = u;
        }
 
            //        Zapewnij reprezentację tekstową obiektu według formatu:
            //«liczba» «jednostka» × «liczba» «jednostka» × «liczba» «jednostka»
            //znak rozdzielający wymiary, to znak mnożenia × (Unicode: U+00D7, multiplication sign, times)
            //pomiędzy liczbami, nazwami jednostek miar i znakami × jest dokładnie jedna spacja
            //domyślne formatowanie liczb(przesłonięcie ToString()) w metrach, z dokładnością 3. miejsc po przecinku


            public string ToString(string format)
            {
                return this.ToString(format, CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {

                return this.ToString("G", CultureInfo.CurrentCulture);
            }

            public string ToString(string format, IFormatProvider provider)
            {
                if (String.IsNullOrEmpty(format)) format = "G";
                if (provider == null) provider = CultureInfo.CurrentCulture;

                switch (format.ToUpperInvariant())
                {
                    case "G":
                        return A.ToString("F3") + " m " + "× " + B.ToString("F3") + " m" + " × " + C.ToString("F3") + " m";
                    case "M":
                        return A.ToString("F3") + " m " + "× " + B.ToString("F3") + " m" + " × " + C.ToString("F3") + " m";
                    case "CM":
                        return (A * 100).ToString("F1") + " cm " + "× " + (B * 100).ToString("F1") + " cm" + " × " + (C * 100).ToString("F1") + " cm";
                    case "MM":
                        return (A * 1000).ToString("F0") + " mm " + "× " + (B * 1000).ToString("F0") + " mm" + " × " + (C * 1000).ToString("F0") + " mm";
                    default:
                        throw new FormatException();
                }
            }

  
        //Zaimplementuj property Objetosc zwracające objętość pudełka w m³. Wynik zaokrąglij(Math.Round) do 9. miejsc po przecinku.
        public double Objętość
            {
                get
                {
                    return Math.Round((A * B * C), 9);
                }

            }
            //Zaimplementuj property Pole zwracające pole powierzchni całkowitej pudełka(prostopadłościanu) w m². Wynik zaokrąglij(Math.Round) do 6. miejsc po przecinku.
            public double Pole
            {
                get
                {
                    return Math.Round(2 * (A * B + A * C + B * C), 6);
                }


            }



    //    Equals

    //        Dwa pudelka są takie same, jeśli mają takie same wymiary w tych samych jednostkach, z dokładnością do kolejności wymiarów, tzn. pudełko P(1, 2.1, 3.05) jest takie samo jak pudełko P(1, 3.05, 2.1), a to jest takie samo jak P(2.1, 1, 3.05), a to jest takie samo jak P(2100, 1000, 3050, unit: UnitOfMeasure.milimeter), i.t.d.
    //        Zaimplementuj interfejs IEquatable<Pudelko>.
    //        Zaimplementuj Equals(object) i GetHashCode().
    //Zaimplementuj przeciążone operatory == oraz !=.


        public bool Equals(Pudelko other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other)) 
                return true;
            // cos tu nie tak ale na razie nie mysle
            return (Objętość == other.Objętość &&
                    (A == other.A || A == other.B || A == other.C) && ((B == other.A || B == other.B || B == other.C) && (C == other.A || C == other.B || C == other.C)))
                    ;
        }
        public override bool Equals(object obj) //wym form
        {
            if (obj is Pudelko)
                return Equals((Pudelko)obj);
            else
                return false;
        }
        public override int GetHashCode() => (A, B, C).GetHashCode();
       
        public static bool Equals(Pudelko p1, Pudelko p2)
        {
            if ((p1 is null) && (p2 is null)) return true; 
            if ((p1 is null)) return false;

            
            return p1.Equals(p2);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2) => Equals(p1, p2);
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);
    }

}