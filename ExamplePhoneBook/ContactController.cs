using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ExamplePhoneBook
{
    internal class ContactController
    {
        private ContactManager contactManager;
        private Form1 view; // Замените Form1 на название вашей формы

        public ContactController(Form1 view)
        {
            this.view = view;
            contactManager = new ContactManager();
            WireUpEvents();
            refreshDataGrid(contactManager.GetContacts());
        }

        private void WireUpEvents()
        {
            view.Load += View_Load;
            view.bAdd.Click += Btn_Click;
            view.bEdit.Click += Btn_Click;
            view.DataGrid.CellClick += dataGrid_CellClick;
            // Другие события элементов управления
        }

        private void View_Load(object sender, EventArgs e)
        {
            // При загрузке формы загрузите контакты из модели и отобразите их в DataGridView
          //  view.DataGrid.DataSource = contactManager.GetContacts();
        }

        public void Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Text.ToLower())
            {
                case "add":
                    // При нажатии кнопки "Добавить контакт" получите данные из TextBox
                    // и добавьте новый контакт в модель
                    add();
                    break;
                case "edit":
                    edit();
                    break;
                case "delete":
                    break;
                case "clear":
                    clearTextBoxes();
                    break;

            }

        }

        Contact newContact;
        int id = 0;
        void edit()
        {
            checkValues();
            contactManager.EditContact(id, newContact);
            refreshDataGrid(contactManager.GetContacts());
        }
        bool checkValues()
        {
            if (view.vName.Text != "")
            {
                //email sam@mmmm.mmm
                //phone +996 000 000 000
                newContact = new Contact
                {
                    name = view.vName.Text,
                    email = view.vEmail.Text,
                    phoneNumber = view.vPhone.Text,
                    groups = view.vGroup.Text,
                };
                return true;
            }
            else { return false; }
        }
        // Другие методы для обработки событий и взаимодействия с моделью
        private void add()
        {
            if (checkValues())
            {
                contactManager.AddContact(newContact);
                refreshDataGrid(contactManager.GetContacts());
            }

        }

        void clearTextBoxes()
        {
            view.vGroupBox1.Controls.OfType<Button>().ToList().ForEach(x => x.Text = "");
            view.vGroup.SelectedIndex = 0;
        }

        internal void find(string text)
        {
            List<Contact> res = contactManager.GetContacts(text);
            refreshDataGrid(res);
        }
        void refreshDataGrid(List<Contact> source)
        {
            view.DataGrid.DataSource = source;
            var m = view.DataGrid.GetType().GetMethod("OnDataSourceChanged",
                BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(view.DataGrid, new object[] { EventArgs.Empty });
        }
        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(view.DataGrid.CurrentRow.Cells[0].Value.ToString());

            Contact contact = contactManager.GetContact(id);
            fillTextBoxes(contact);
        }
        void fillTextBoxes(Contact contact)
        {
            if (!string.IsNullOrEmpty(contact.name))
            {
                view.vName.Text = contact.name;
                view.vEmail.Text = contact.email;
                view.vPhone.Text = contact.phoneNumber;
                view.vGroup.Text = contact.groups;
            }
        }
    }

}
