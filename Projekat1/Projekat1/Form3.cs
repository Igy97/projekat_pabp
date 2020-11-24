using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekat1
{
    public partial class Form3 : Form
    {
        private NorthwindDataSet dataSet;
        private int productID = -1;
        private bool dodavanjeForma;
        private bool azuriranjeBaze = false;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(NorthwindDataSet dataSet, bool dodavanjeForma, int id)
        {
            InitializeComponent();
            this.dataSet = dataSet;
            productsBindingSource.DataSource = this.dataSet;
            this.productID = id;
            productsBindingSource.Filter = "ProductID =" + this.productID;
            this.dodavanjeForma = dodavanjeForma;
            if (this.dodavanjeForma)
            {
                this.Text = "Dodavanje";
                button1.Text = "Dodaj";
            }
            else
            {           
                this.Text = "Izmena";
                button1.Text = "Izmeni";  
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            prikazUkupneCene();
        }

        private void prikazUkupneCene()
        {
            decimal unitPrice = 0;
            decimal.TryParse(unitPriceTextBox.Text, out unitPrice);
            decimal unitsInStock = 0;
            decimal.TryParse(unitsInStockTextBox.Text, out unitsInStock);
            decimal cena = unitPrice * unitsInStock;
            this.label2.Text = "Ukupna cena proizvoda:" + cena.ToString("0.##"); 
        }

        private void unitPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            prikazUkupneCene();
        }

        private void unitsInStockTextBox_TextChanged(object sender, EventArgs e)
        {
            prikazUkupneCene();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dodavanjeForma)
                {
                    NorthwindDataSet.ProductsRow row = this.dataSet.Products.NewProductsRow();                 
                    row.ProductName = productNameTextBox.Text;
                    row.SupplierID = int.Parse(supplierIDTextBox.Text);
                    row.CategoryID = int.Parse(categoryIDTextBox.Text);
                    row.QuantityPerUnit = quantityPerUnitTextBox.Text;
                    row.UnitPrice = decimal.Parse(unitPriceTextBox.Text);
                    row.UnitsInStock = short.Parse(unitsInStockTextBox.Text);
                    row.UnitsOnOrder = short.Parse(unitsOnOrderTextBox.Text);
                    row.ReorderLevel = short.Parse(reorderLevelTextBox.Text);
                    row.Discontinued = discontinuedCheckBox.Checked;
                    this.dataSet.Products.AddProductsRow(row);
                }
                azuriranjeBaze = true;
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.StackTrace);
            }
           
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(azuriranjeBaze)
            {
                productsBindingSource.EndEdit();
                MessageBox.Show("Izmena uradjena!");
            }
            else
            {
                this.dataSet.RejectChanges();
                MessageBox.Show("Vraceno na prethodno stanje!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            azuriranjeBaze = false;
            productsBindingSource.ResetBindings(true);
            this.Close();
        }
    }
}
