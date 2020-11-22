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
        private int categoryID;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(NorthwindDataSet dataSet, int categoryID, Form1 f1)
        {
            InitializeComponent();
            this.dataSet = dataSet;
            productsBindingSource.DataSource = this.dataSet;
            this.f1 = f1;
            this.categoryID = categoryID;
            productsBindingSource.Filter = "CategoryID = " + this.categoryID; 

        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
        }

        private void productsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
        }

        private void productsBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.northwindDataSet.Suppliers);
            // TODO: This line of code loads data into the 'northwindDataSet.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.northwindDataSet.Suppliers);
            this.productsTableAdapter.Fill(this.dataSet.Products);

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Show();
            this.Dispose();
        }
    }
}
