using Microsoft.VisualStudio.TestTools.UnitTesting;
using PudelkoLibre;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace PudelkoUnitTests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    [TestClass]
    public class UnitTestsPudelkoConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dok�adno�� 3 miejsca po przecinku

        private void AssertPudelko(Pudelko p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }


        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Pudelko p = new Pudelko();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metr�w licz� si� 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metr�w licz� si� 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)] // dla centymert�w liczy si� tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UnitOfMeasure.centimeter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
                 0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
                 0.1, 0.025, 0.003)] // dla milimetr�w nie licz� si� miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                     double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b, c: c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a, b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a: a, b: b, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }


        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
        }


        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.milimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.milimeter);
        }


        #endregion


        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        public void ToString_Default_Culture_EN()
        {
            var p = new Pudelko(2.5, 9.321);
            string expectedStringEN = "2.500 m � 9.321 m � 0.100 m";

            Assert.AreEqual(expectedStringEN, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m � 9.321 m � 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m � 9.321 m � 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm � 932.1 cm � 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm � 9321 mm � 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Pudelko(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }
          #region Pole, Obj�to�� ===================================
        //        Utw�rz testy jednostkowe(unit tests) dla:
        //    properties Objetosc i Pole,
        [TestMethod]
        public void SprawdzanieObjetosciCent()
        {
            // Arrange
            double expected = 0.00001;
            Pudelko pudOb = new Pudelko(1, 1, 1, UnitOfMeasure.centimeter);

            // Act

            // Assert
            double actual = pudOb.Obj�to��;
            Assert.AreEqual(expected, actual, 0.001, "Obj�to�� oczekiwana i otrzymana s� r�ne");
        }

        [TestMethod]
        public void SprawdzanieObjetosciMetry()
        {
            // Arrange
            double expected = 4.1956;
            Pudelko pudOb = new Pudelko(1.234, 0.34, 10, UnitOfMeasure.meter);

            // Act

            // Assert
            double actual = pudOb.Obj�to��;
            Assert.AreEqual(expected, actual, 0.001, "Obj�to�� oczekiwana i otrzymana s� r�ne");
        }

        [TestMethod]
        public void SprawdzanieObjetosciMilimetry()
        {
            // Arrange
            double expected = 0.031177993;//zaokragla do 9 chyba ok
            Pudelko pudOb = new Pudelko(1111, 211, 133, UnitOfMeasure.milimeter);

            // Act

            // Assert
            double actual = pudOb.Obj�to��;
            Assert.AreEqual(expected, actual, 0.001, "Obj�to�� oczekiwana i otrzymana s� r�ne");
        }

        [TestMethod]
        public void SprawdzaniePolaMetry()
        {
            // Arrange
            double expected = 22;
            Pudelko pudOb = new Pudelko(1, 2, 3, UnitOfMeasure.meter);

            // Act

            // Assert
            double actual = pudOb.Pole;
            Assert.AreEqual(expected, actual, 0.1, "Obj�to�� oczekiwana i otrzymana s� r�ne");
        }



        [TestMethod]
        public void SprawdzaniePolaMilimetry6miejscPoPrzecinku()
        {
            // Arrange
            double expected = 2.036406;//6.ok
            Pudelko pudOb2 = new Pudelko(111, 2331, 311, UnitOfMeasure.milimeter);

            // Act

            // Assert
            double actual = pudOb2.Pole;
            Assert.AreEqual(expected, actual, 0.1, "Obj�to�� oczekiwana i otrzymana s� r�ne");
        }

        #endregion

        //    operatora ��czenia pude�ek,
        [TestMethod]
        public void SprawdzanieLaczeniaPudelekRozneMiary()
        {
            // Arrange
            double expected1 = 3;//najd�.
            double expected2 = 2;//najd� ze �red.
            double expected3 = 1.1;//dodane

            Pudelko pudL1 = new Pudelko(10, 20, 30, UnitOfMeasure.centimeter);
            Pudelko pudL2 = new Pudelko(1000, 2000, 3000, UnitOfMeasure.milimeter);

            // Act
            Pudelko pudL3 = pudL2 + pudL1;
            // Assert
            double actual1 = pudL3.A;
            double actual2 = pudL3.B;
            double actual3 = pudL3.C;
            Assert.AreEqual(expected1, actual1, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne");
            Assert.AreEqual(expected2, actual2, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne");
            Assert.AreEqual(expected3, actual3, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne");
            
        }
        [TestMethod]
        public void SprawdzanieLaczeniaPudelekDomyslneMiarySzescian()
        {
            // Arrange
            double expected1 = 2;//najd�.
            double expected2 = 2;//najd� ze �red.
            double expected3 = 4;//dodane

            Pudelko pudL4 = new Pudelko(2, 2, 2);
            Pudelko pudL5 = new Pudelko(2, 2, 2);

            // Act
            Pudelko pudL6 = pudL4 + pudL5;
            // Assert
            double actual1 = pudL6.A;
            double actual2 = pudL6.B;
            double actual3 = pudL6.C;
            Assert.AreEqual(expected1, actual1, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne");
            Assert.AreEqual(expected2, actual2, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne");
            Assert.AreEqual(expected3, actual3, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne");

        }


        #endregion

        #region Equals ===========================================
        [TestMethod]
        public void SprawdzanieEqualsMieszaneJednostkiMixUstawien()
        {

            // Arrange

            Pudelko p1 = new Pudelko(2, 3, 4, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(300.00, 200.01, 400, UnitOfMeasure.centimeter);
            // Act

            bool a = p1.Equals(p2);
            // Assert

            Assert.IsTrue(a);
        }

        [TestMethod]
        public void SprawdzanieEqualsMetryIDomyslnyMix()
        {

            // Arrange

            Pudelko p1 = new Pudelko(3, 2, 1, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(1, 2, 3);
            // Act

            bool a = p1.Equals(p2);
            // Assert

            Assert.IsTrue(a);
        }
        #endregion


        #region Operators overloading ===========================

        [TestMethod]
        public void SprawdzanieOverlodingTakieSamo()
        {

            // Arrange

            Pudelko p11 = new Pudelko(3, 2, 1, UnitOfMeasure.meter);
            Pudelko p22 = new Pudelko(1, 2, 3);
            bool ono = true;
            // Act

            ono= (p11 == p22);
            // Assert

            Assert.IsTrue(ono);
        }

        [TestMethod]
        public void SprawdzanieOverlodingNieTakieSamo()
        {

            // Arrange

            Pudelko p11 = new Pudelko(3, 2, 1, UnitOfMeasure.meter);
            Pudelko p22 = new Pudelko(1, 2, 3);
            bool ono = true;
            // Act

            ono = (p11 != p22);
            // Assert

            Assert.IsFalse(ono);
        }

        #endregion

        //        #region Conversions =====================================
        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            double[] tab = (double[])p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }

        [TestMethod]
        public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
            Pudelko p = (a, b, c);
            Assert.AreEqual((int)(p.A * 1000), a);
            Assert.AreEqual((int)(p.B * 1000), b);
            Assert.AreEqual((int)(p.C * 1000), c);
        }


        //        #endregion

         #region Indexer, enumeration ============================
        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }


        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            var tab = new[] { p.A, p.B, p.C };
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }
  

            #endregion

        #region Parsing =========================================


            [TestMethod]
            public void ParsingMetry()
            {
            // Arrange
            double expected1 = 2.5;
            double expected2 = 9.3;
            double expected3 = 0.1;

            // Act
            Pudelko an = Pudelko.Parse("2.5 m � 9.3 m � 0.1 m");
            // Assert
            double actual1 = an.A;
            double actual2 = an.B;
            double actual3 = an.C;
            Assert.AreEqual(expected1, actual1, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania");
            Assert.AreEqual(expected2, actual2, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania");
            Assert.AreEqual(expected3, actual3, 0.001, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania parsowania");
        }


        [TestMethod]
        public void ParsingMilimetry()
        {
            // Arrange
            double expected1 = 0.22;
            double expected2 = 0.2;
            double expected3 = 1;

            // Act
            Pudelko an2 = Pudelko.Parse("220 mm � 200.3888 mm � 1000 mm");
            // Assert
            double actual1 = an2.A;
            double actual2 = an2.B;
            double actual3 = an2.C;
            Assert.AreEqual(expected1, actual1, 0.1, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania");
            Assert.AreEqual(expected2, actual2, 0.1, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania");
            Assert.AreEqual(expected3, actual3, 0.1, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania parsowania");
        }
        [TestMethod]
        public void ParsingCentymetry()
        {
            // Arrange
            double expected1 = 0.02;
            double expected2 = 0.2;
            double expected3 = 0.1;

            // Act
            Pudelko an3 = Pudelko.Parse("2 cm � 20 cm � 10 cm");
            // Assert
            double actual1 = an3.A;
            double actual2 = an3.B;
            double actual3 = an3.C;
            Assert.AreEqual(expected1, actual1, 0.1, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania");
            Assert.AreEqual(expected2, actual2, 0.1, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania");
            Assert.AreEqual(expected3, actual3, 0.1, "D�ugo�� boku oczekiwana i otrzymana s� r�ne/ brak parsowania parsowania");
        }

        #endregion

    }
}