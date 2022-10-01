    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;

    namespace PudelkoLibre
    {
        public sealed class Pudelko : IFormattable
        {

            UnitOfMeasure unit { get; set; }
            //Zaimplementuj properties o nazwach A, B i C zwracające wymiary pudełka w metrach(z dokładnością do 3 miejsc po przecinku).
            private double _a;
            private double _b;
            private double _c;
            public double A
            {
                get
                {
                    if (unit == UnitOfMeasure.meter)
                    {
                        _a = Math.Round(_a, 3);
                    }
                    else if (unit == UnitOfMeasure.centimeter)
                    {

                        _a = Math.Round((_a / 100), 3);
                    }
                    else if (unit == UnitOfMeasure.milimeter)
                    {

                        _a = Math.Round((_a / 1000), 3);
                    }
                    return _a;
                }

            }
            public double B
            {
                get
                {

                    if (unit == UnitOfMeasure.meter)
                    {
                        _b = Math.Round(_b, 3);
                    }
                    else if (unit == UnitOfMeasure.centimeter)
                    {

                        _b = Math.Round((_b / 100), 3);
                    }
                    else if (unit == UnitOfMeasure.milimeter)
                    {

                        _b = Math.Round((_b / 1000), 3);
                    }
                    return _b;
                }

            }
            public double C
            {
                get
                {

                    if (unit == UnitOfMeasure.meter)
                    {
                        _c = Math.Round(_c, 3);
                    }
                    else if (unit == UnitOfMeasure.centimeter)
                    {

                        _c = Math.Round((_c / 100), 3);
                    }
                    else if (unit == UnitOfMeasure.milimeter)
                    {

                        _c = Math.Round((_c / 1000), 3);
                    }
                    return _c;
                }

            }

            private List<double> ListaPo = new List<double>();

            public Pudelko(List<double> lista = default(List<double>)/*null?*/, UnitOfMeasure unit = UnitOfMeasure.meter)
            {
                if (lista == null)
                {
                    lista = new List<double>();
                    lista.Add(10);
                    lista.Add(10);
                    lista.Add(10);
                    unit = UnitOfMeasure.centimeter;
                }
                foreach (double n in lista)
                {
                    //W przypadku próby utworzenia pudełka z którymkolwiek z parametrów niedodatnim, zgłaszany jest wyjątek ArgumentOutOfRangeException
                    if (n < 0)
                    { throw new ArgumentOutOfRangeException(); }
                    //W przypadku próby utworzenia pudełka z którymkolwiek z parametrów większym niż 10 metrów, zgłaszany jest wyjątek ArgumentOutOfRangeException.
                    else if (((unit == UnitOfMeasure.milimeter) & (n > 100000)) || ((unit == UnitOfMeasure.centimeter) & (n > 10000)) || ((unit == UnitOfMeasure.meter) & (n > 10)))

                    { throw new ArgumentOutOfRangeException(); }
                }
                // Wszystkie parametry konstruktora są opcjonalne.


                //Jeśli podano mniej niż 3 wartości liczbowe, pozostałe przyjmuje się jako o wartości 10 cm, ale dla ustalonej jednostki miary.
                if (lista.Count < 3) //może być prościej ale nie wiem jak to kompetentnie zrobić/ bez błędu
                {
                    if (lista.Count == 0)
                    {
                        if (unit == UnitOfMeasure.milimeter)
                        {
                            lista.Add(1000);
                            lista.Add(1000);
                            lista.Add(1000);
                        }
                        else if (unit == UnitOfMeasure.centimeter)
                        {
                            lista.Add(10);
                            lista.Add(10);
                            lista.Add(10);
                        }
                        else
                        {
                            lista.Add(0.1);
                            lista.Add(0.1);
                            lista.Add(0.1);
                        }
                    }
                    else if (lista.Count == 1)
                    {


                        if (unit == UnitOfMeasure.milimeter)
                        {
                            lista.Add(1000);
                            lista.Add(1000);
                        }
                        else if (unit == UnitOfMeasure.centimeter)
                        {
                            lista.Add(10);
                            lista.Add(10);
                        }
                        else
                        {
                            lista.Add(0.1);
                            lista.Add(0.1);
                        }

                    }
                    else if (lista.Count == 2)
                    {
                        if (unit == UnitOfMeasure.milimeter)
                        {
                            lista.Add(1000);
                        }
                        else if (unit == UnitOfMeasure.centimeter)
                        {
                            lista.Add(10);
                        }
                        else
                        {
                            lista.Add(0.1);
                        }
                    }
                }
                _a = lista[0];
                _b = lista[1];
                _c = lista[2];
                this.unit = unit;
                ListaPo = lista;
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
        }
    }