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
    public partial class Form2 : Form
    {
        private Form1 f1;
        NorthwindDataSet dataSet;
        private int categoryID = -1;
        private double unitPriceFilter = -1;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(NorthwindDataSet dataSet, int categoryID, Form1 f1)
        {
            InitializeComponent();
            this.dataSet = dataSet;
            suppliersBindingSource.DataSource = this.dataSet;
            productsBindingSource.DataSource = this.dataSet;      
            this.f1 = f1;
            this.categoryID = categoryID;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Order_Details' table. You can move, or remove it, as needed.
            this.order_DetailsTableAdapter.Fill(this.dataSet.Order_Details);
            // TODO: This line of code loads data into the 'northwindDataSet.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.dataSet.Suppliers);
            this.productsTableAdapter.Fill(this.dataSet.Products);
            filtersForProducts();

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Show();
            this.Dispose();
        }

        private void productsDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if(productsDataGridView.CurrentRow != null)
            {
                decimal unitPrice = 0;
                decimal.TryParse(productsDataGridView.CurrentRow.Cells["unitPriceDataGridViewTextBoxColumn"].Value.ToString(), out unitPrice);
                decimal unitsInStock = 0;
                decimal.TryParse(productsDataGridView.CurrentRow.Cells["unitsInStockDataGridViewTextBoxColumn"].Value.ToString(), out unitsInStock);
                decimal cena = unitPrice * unitsInStock;
                label6.Text = "Ukupna cena proizvoda:" + cena.ToString("0.##"); 
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                label6.Text = "Ukupna cena proizvoda:0";
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void filtersForProducts()
        {
            if (this.categoryID != -1 || suppliersComboBox.Items.Count > 0)
            {
                string executeQuery = "CategoryID = " + this.categoryID + " AND " + "SupplierID = " + suppliersComboBox.SelectedValue;
                if (unitPriceFilter >= 0)
                {
                    executeQuery += " AND " + "UnitPrice <= " + unitPriceFilter;
                }
                productsBindingSource.Filter = executeQuery;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                try
                {
                    unitPriceFilter = Convert.ToDouble(textBox1.Text);
                    filtersForProducts();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.StackTrace);
                    textBox1.Text = "";
                    unitPriceFilter = -1;
                }
            }
            else
            {
                unitPriceFilter = -1;
                filtersForProducts();
            }
        }

        private void suppliersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(suppliersComboBox.SelectedIndex != -1)
            {
                filtersForProducts();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this.dataSet, true, -1);
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            editOtvaranjeForme();
        }


        private void editOtvaranjeForme()
        {
            int productID;
            int.TryParse(productsDataGridView.CurrentRow.Cells["productIDDataGridViewTextBoxColumn"].Value.ToString(), out productID);
            Form3 f3 = new Form3(this.dataSet, false, productID);
            f3.Show();
        }

        private void productsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editOtvaranjeForme();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(productsDataGridView.CurrentRow != null)
            {
                productsDataGridView.Rows.RemoveAt(productsDataGridView.CurrentRow.Index);
                productsBindingSource.EndEdit();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                productsTableAdapter.Update(dataSet);
                MessageBox.Show("Baza je azurirana!");
            }
            catch(Exception error)
            {
                MessageBox.Show(error.StackTrace);
            }
            

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id =(int)dataGridView1.CurrentRow.Cells["OrderID"].Value;
            Form4 f4 = new Form4(this.dataSet, id);
            f4.Show();
        }

        //TODO: Add new form for Order, double click on orders detail

    }
}
