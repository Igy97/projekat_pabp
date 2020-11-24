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
    public partial class Form4 : Form
    {
        NorthwindDataSet dataSet;
        int id;
        public Form4()
        {
            InitializeComponent();
        }

        public Form4(NorthwindDataSet dataSet, int id)
        {
            InitializeComponent();
            this.dataSet = dataSet;
            this.id = id;
            ordersBindingSource.Filter = "OrderID =" + this.id;
        }

        private void ordersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ordersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Orders' table. You can move, or remove it, as needed.
            this.ordersTableAdapter.Fill(this.northwindDataSet.Orders);

        }
    }
}
