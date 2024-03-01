using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamplePhoneBook
{
    public partial class Form1 : Form
    {
        List<string> groups = new List<string> { "Family", "Work",
            "Friends", "Acquaintances", "Neighbors",
            "Colleagues", "Contacts", "Partners"};
        private ContactController contactController;
        public Form1()
        {
            InitializeComponent();
            //groups.Sort();
            boxGroup.DataSource = groups;
            boxGroup.SelectedIndex = 0;

        }
        public DataGridView DataGrid
        {
            get { return dataGrid; }
        }
        public TextBox vName
        {
            get { return txtName; }
        }
        public TextBox vEmail
        {
            get { return txtEmail; }
        }
        public TextBox vPhone
        {
            get { return txtPhone; }
        }
        public TextBox vFind
        {
            get { return txtFind; }
        }
        public ComboBox vGroup
        {
            get { return boxGroup; }
        }

        public GroupBox vGroupBox1
        {
            get { return groupBox1; }
        }

        public Button bAdd
        {
            get { return buttonAdd; }
        }
        
        public Button bEdit
        {
            get { return buttonEdit; }
        }

        private void button_Click(object sender, EventArgs e)
        {
            contactController.Btn_Click(sender, e);
          
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            contactController.find(txtFind.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            contactController = new ContactController(this);
        }
    }
}
