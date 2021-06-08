using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScolloLib
{
    public class SalesLib
    {

        //Basic sales tax is applicable at a rate of 10% on all goods, except books, food, and medical products that are exempt
        const decimal basicSales = 0.1m;

        const decimal importDuty = 0.05m;
        //Import duty is an additional sales tax applicable on all imported goods at a rate of 5%, with no exemptions

        public GoodReceipt createReceipt(List<Good> goods)
        {
            GoodReceipt receipt = new GoodReceipt();
            receipt.goods = goods;
            foreach(var good in receipt.goods)
            {
                good.Total = good.Quantity * good.Price;
                good.Taxes = 0;

                decimal taxPercentage = 0;

                //calculate sales taxes
                if (!(good.Type is GoodType.Book) && !(good.Type is GoodType.Food) && !(good.Type is GoodType.Medical))
                {
                    taxPercentage = basicSales;
                }

                //calculate import duty
                if(good.Imported)
                {
                    taxPercentage += importDuty;
                }

                //The rounding rules for sales tax are that for a tax rate of n%, a shelf price of p contains (np/100 rounded up to the nearest 0.05
                decimal price_taxed = (good.Price * taxPercentage); 
                decimal round_price_taxed = this.Round(price_taxed);

                good.Taxes += good.Quantity * round_price_taxed;

                System.Diagnostics.Trace.TraceInformation("Taxes % = " + taxPercentage);
                System.Diagnostics.Trace.TraceInformation("Taxes calculated = " + price_taxed);
                System.Diagnostics.Trace.TraceInformation("Taxes round calculated = " + round_price_taxed);

                

                receipt.salesTaxes += good.Taxes;
                receipt.total += (good.Total + good.Taxes);
            }

            return receipt;
        }

        private decimal Round(decimal amount)
        {
            return Math.Ceiling(amount * 20) / 20;

        }

        public String PrintReceipt(GoodReceipt receipt)
        {
            String receiptMsg = Environment.NewLine;
            float total;
            foreach (var good in receipt.goods)
            {
                receiptMsg += good.Quantity + " " + good.Label + ":" + good.TotalWithTaxes.ToString("#.##") + Environment.NewLine;
                /*
                 2 book: 24.98
                 1 music CD: 16.49
                 1 chocolate bar: 0.85
                 Sales Taxes: 1.50
                 Total: 42.32
                 */
            }
            receiptMsg += "Sales Taxes:" + receipt.salesTaxes.ToString("#.##") + Environment.NewLine;
            receiptMsg += "Total:" + receipt.total.ToString("#.##") + Environment.NewLine;

            System.Diagnostics.Trace.TraceInformation(receiptMsg);

            return receiptMsg;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Good
    {
        public Good(int quantity, decimal price, Boolean imported, GoodType type, String label)
        {
            this.Quantity = quantity;
            this.Price = price;
            this.Imported = imported;
            this.Type = type;
            this.Label = label;

            this.Total = 0;
            this.Taxes = 0;
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Boolean Imported { get; set; }
        public GoodType Type { get; set; }
        public String Label { get; set; }

        public decimal Total { get; set; }
        public decimal Taxes { get; set; }
        public decimal TotalWithTaxes {
            get
            {
                return this.Total + this.Taxes;
            } 
        }
        /*
         Input 1:
         2 book at 12.49
         1 music CD at 14.99
         1 chocolate bar at 0.85
         
         Input 2:
         1 imported box of chocolates at 10.00
         1 imported bottle of perfume at 47.50
         
         Input 3:
         1 imported bottle of perfume at 27.99
         1 bottle of perfume at 18.99
         1 packet of headache pills at 9.75
         3 box of imported chocolates at 11.25

         OUTPUT

         Output 1:
         2 book: 24.98
         1 music CD: 16.49
         1 chocolate bar: 0.85
         Sales Taxes: 1.50
         Total: 42.32
         
         Output 2:
         1 imported box of chocolates: 10.50
         1 imported bottle of perfume: 54.65
         Sales Taxes: 7.65
         Total: 65.15
         
         Output 3:
         1 imported bottle of perfume: 32.19
         1 bottle of perfume: 20.89
         1 packet of headache pills: 9.75
         3 imported box of chocolates: 35.55
         Sales Taxes: 7.90
         Total: 98.38

         */
    }

    public class GoodReceipt
    {
        public List<Good> goods { get; set; }
        public decimal salesTaxes { get; set; }
        public decimal total { get; set; }
    }

    public enum GoodType
    {
        Book,
        Food,
        Medical,
        Others
    }
}
