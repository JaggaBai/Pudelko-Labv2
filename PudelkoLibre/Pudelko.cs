    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Collections;

namespace PudelkoLibre
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        private readonly UnitOfMeasure unit2;
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private List<double> krawedzie;
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
                x = Convert.ToDouble((x / 100).ToString("#.###"));
            }
            else if (z == UnitOfMeasure.milimeter)
            {
                x = Convert.ToDouble((x / 1000).ToString("#.###"));
            }
            return x;
        }

        public string WykopujemyMiejscaPoPrzecinku(double z, UnitOfMeasure mm)
        {

            double zz = 0;

            if (mm == UnitOfMeasure.meter)
            {
                zz = Math.Round((z), 3, MidpointRounding.ToZero);
            }
            
            else if (mm == UnitOfMeasure.centimeter)
            {
                zz = Math.Round(((z)), 1, MidpointRounding.ToZero);
            }
            else if (mm == UnitOfMeasure.milimeter)
            {
                zz = Math.Round(((z)), 0, MidpointRounding.ToZero);
            }

            return zz.ToString();

        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            UnitOfMeasure u = unit;
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

            if ((a1 <= 0 | b1 <= 0 | c1 <= 0) || ((u == UnitOfMeasure.milimeter) & ((a1 > 10000) | (b1 > 10000) | (c1 > 10000))) || ((u == UnitOfMeasure.centimeter) & ((a1 > 1000) | (b1 > 1000) | (c1 > 1000))) || ((u == UnitOfMeasure.meter) & ((a1 > 10) | (b1 > 10) | (c1 > 10))))
            { throw new ArgumentOutOfRangeException(); }

            _a = Zmiana(a1, u);
            _b = Zmiana(b1, u);
            _c = Zmiana(c1, u);
            krawedzie = new List<double>(new double[] { _a, _b, _c });
            unit2 = u;
        }

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
        public double Objętość
        {
            get
            {
                return Math.Round((A * B * C), 9);
            }

        }
        public double Pole
        {
            get
            {
                return Math.Round(2 * (A * B + A * C + B * C), 6);
            }
        }
        public double SumaKrawedzi //dodatk
        {
            get
            {
                return Math.Round(A+B+C, 6);
            }
        }
        public bool Equals(Pudelko other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other))
                return true;
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

        public static Pudelko operator +(Pudelko pa, Pudelko pb)
        {
            List<double> paLista = new List<double>() { pa.A, pa.B, pa.C };
            List<double> pbLista = new List<double>() { pb.A, pb.B, pb.C };
            paLista.Sort();
            pbLista.Sort();
            double pcA;
            double pcB;
            double pcC;
            if (paLista[2] >= pbLista[2])
            { pcA = paLista[2]; }
            else { pcA = pbLista[2]; }
            pcC = paLista[0] + pbLista[0];
            if (paLista[1] >= pbLista[1])
            { pcB = paLista[1]; }
            else { pcB = pbLista[1]; }
            return new Pudelko(pcA, pcB, pcC, UnitOfMeasure.meter);
        }

        static public implicit operator Pudelko(ValueTuple<int, int, int> v1)
        {

            return new Pudelko(a: v1.Item1, b: v1.Item2, c: v1.Item3, unit: UnitOfMeasure.milimeter);
        }

        static public explicit operator double[](Pudelko v2)
        {
            return new double[3] { v2.A, v2.B, v2.C };
        }
        public double this[int i]
        {
            get
            {
                if (i == 0)
                {
                    return A;
                }
                else if (i == 1)
                { return B; }
                else if (i == 2) { return C; }
                else return 0;
            }
        }
        public IEnumerator GetEnumerator()
        {
             return krawedzie.GetEnumerator(); 
        }
        public static Pudelko Parse(string f)
        {
            string[] firstA = f.Trim().Split("×");
            string[] fA1 = (firstA[0].Trim()).Split(" ");
            string[] fA2 = (firstA[1].Trim()).Split(" ");
            string[] fA3 = (firstA[2].Trim()).Split(" ");
            string[] secondA = new string[] { fA1[0], fA2[0], fA3[0], fA3[1] };
            string format = secondA[3];
            string ANs = secondA[0].Trim();
            string BNs = secondA[1].Trim();
            string CNs = secondA[2].Trim();
            Console.WriteLine(ANs);
            double AN = Convert.ToDouble(ANs, CultureInfo.InvariantCulture);
            double BN = Convert.ToDouble(BNs, CultureInfo.InvariantCulture);
            double CN = Convert.ToDouble(CNs, CultureInfo.InvariantCulture);

            switch (format)
            {
                case "m":
                    return new Pudelko(AN, BN, CN, unit: UnitOfMeasure.meter);
                case "cm":
                    return new Pudelko(AN, BN, CN, unit: UnitOfMeasure.centimeter);
                case "mm":
                    return new Pudelko(AN, BN, CN, unit: UnitOfMeasure.milimeter);
                default:
                    throw new FormatException();
            }
        }

    }
}

