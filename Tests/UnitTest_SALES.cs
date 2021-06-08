using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ScolloLib;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UnitTest_SALES
    {
        [TestMethod]
        public void TestMethod1()
        {
            //DATASET
            /*
             Input 1:
             2 book at 12.49
             1 music CD at 14.99
             1 chocolate bar at 0.85
             
             Output 1:
             2 book: 24.98
             1 music CD: 16.49
             1 chocolate bar: 0.85
             Sales Taxes: 1.50
             Total: 42.32

             */

            Good g_1_01 = new Good(2, 12.49m, false, GoodType.Book, "book");
            Good g_1_02 = new Good(1, 14.99m, false, GoodType.Others, "music CD");
            Good g_1_03 = new Good(1, 0.85m, false, GoodType.Food, "chocolate bar");

            List<Good> input1 = new List<Good> { g_1_01, g_1_02, g_1_03 };

            SalesLib sl = new SalesLib();

            var receipt1_data = sl.createReceipt(input1);
            var receipt = sl.PrintReceipt(receipt1_data);

            Assert.AreEqual(Math.Round(g_1_01.TotalWithTaxes, 2), Math.Round(24.98m, 2));
            Assert.AreEqual(Math.Round(g_1_02.TotalWithTaxes, 2), Math.Round(16.49m, 2));
            Assert.AreEqual(Math.Round(g_1_03.TotalWithTaxes, 2), Math.Round(0.85m, 2));
            Assert.AreEqual(Math.Round(receipt1_data.total,2), Math.Round(42.32m,2));
            Assert.AreEqual(Math.Round(receipt1_data.salesTaxes, 2), Math.Round(1.50m, 2));
        }

        [TestMethod]
        public void TestMethod2()
        {
            //DATASET
            /*
             Input 2:
             1 imported box of chocolates at 10.00
             1 imported bottle of perfume at 47.50
             
             Output 2:
             1 imported box of chocolates: 10.50
             1 imported bottle of perfume: 54.65
             Sales Taxes: 7.65
             Total: 65.15
             
             */

            Good g_2_01 = new Good(1, 10.00m, true, GoodType.Food, "imported box of chocolates");
            Good g_2_02 = new Good(1, 47.50m, true, GoodType.Others, "imported bottle of perfume");

            List<Good> input1 = new List<Good> { g_2_01, g_2_02 };

            SalesLib sl = new SalesLib();

            var receipt1_data = sl.createReceipt(input1);
            var receipt = sl.PrintReceipt(receipt1_data);

            Assert.AreEqual(Math.Round(g_2_01.TotalWithTaxes, 2), Math.Round(10.50m, 2));
            Assert.AreEqual(Math.Round(g_2_02.TotalWithTaxes, 2), Math.Round(54.65m, 2));
            Assert.AreEqual(Math.Round(receipt1_data.total, 2), Math.Round(65.15m, 2));
            Assert.AreEqual(Math.Round(receipt1_data.salesTaxes, 2), Math.Round(7.65m, 2));
        }

        [TestMethod]
        public void TestMethod3()
        {
            //DATASET
            /*
             Input 3:
             1 imported bottle of perfume at 27.99
             1 bottle of perfume at 18.99
             1 packet of headache pills at 9.75
             3 box of imported chocolates at 11.25
                         
             Output 3:
             1 imported bottle of perfume: 32.19
             1 bottle of perfume: 20.89
             1 packet of headache pills: 9.75
             3 imported box of chocolates: 35.55
             Sales Taxes: 7.90
             Total: 98.38

             */

            Good g_3_01 = new Good(1, 27.99m, true, GoodType.Others, "imported bottle of perfume");
            Good g_3_02 = new Good(1, 18.99m, false, GoodType.Others, "bottle of perfume");
            Good g_3_03 = new Good(1, 9.75m, false, GoodType.Medical, "packet of headache pills");
            Good g_3_04 = new Good(3, 11.25m, true, GoodType.Food, "box of imported chocolates");

            List<Good> input1 = new List<Good> { g_3_01, g_3_02, g_3_03, g_3_04 };

            SalesLib sl = new SalesLib();

            var receipt1_data = sl.createReceipt(input1);
            var receipt = sl.PrintReceipt(receipt1_data);

            Assert.AreEqual(Math.Round(g_3_01.TotalWithTaxes, 2), Math.Round(32.19m, 2));
            Assert.AreEqual(Math.Round(g_3_02.TotalWithTaxes, 2), Math.Round(20.89m, 2));
            Assert.AreEqual(Math.Round(g_3_03.TotalWithTaxes, 2), Math.Round(9.75m, 2));
            Assert.AreEqual(Math.Round(g_3_04.TotalWithTaxes, 2), Math.Round(35.55m, 2));
            Assert.AreEqual(Math.Round(receipt1_data.total, 2), Math.Round(98.38m, 2));
            Assert.AreEqual(Math.Round(receipt1_data.salesTaxes, 2), Math.Round(7.90m, 2));
        }
    }
}
