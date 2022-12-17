using Kontaklar.DbOperations;
using Kontaklar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kontaklar.UI
{
    public partial class Form1 : Form
    {
        AppContextDb db = new AppContextDb();
        DialogResult alert;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshPage();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Select contact from inside from table rows...
            try
            {
                textBoxName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBoxSurname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBoxEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Add event...
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Contact contact = new Contact();
            try
            {
                AddContact(contact); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Update event...
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                 int entityId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                 UpdateContact(entityId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Delete event...
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int entityId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                DeleteContact(entityId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Seacrh event...
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchContact();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // ************* CUSTOM METHODS **************

        // Add method...
        private void AddContact(Contact contact)
        {
            contact.Ad = textBoxName.Text;
            contact.Soyad = textBoxSurname.Text;
            contact.Telefon = textBoxPhone.Text;
            contact.Email = textBoxEmail.Text;
            db.Contacts.Add(contact);
            db.SaveChanges();
            RefreshPage();
        }

        // Update method...
        private void UpdateContact(int entityId)
        {
            Contact contact = db.Contacts.SingleOrDefault(c => c.Id == entityId);
            contact.Ad = textBoxName.Text;
            contact.Soyad = textBoxSurname.Text;
            contact.Telefon = textBoxPhone.Text;
            contact.Email = textBoxEmail.Text;
            db.SaveChanges();
            RefreshPage();
        }

        // Delete method...
        public void DeleteContact(int entityId)
        {
            var name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            var surname = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            Contact contact = db.Contacts.SingleOrDefault(c => c.Id == entityId);

            alert = MessageBox.Show($"{name}  {surname}  " +
                $"adındaki şəxsi silmək istədiyindən əminsən?", "İstifadəçi silinəcək!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (alert == DialogResult.Yes)
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
            RefreshPage();
        }

        // Filters entities...
        public void SearchContact()
        {
               dataGridView1.DataSource = db.Contacts.Where(c => c.Ad.Contains(textBoxSearch.Text) ||
               c.Soyad.Contains(textBoxSearch.Text) || c.Telefon.Contains(textBoxSearch.Text) ||
               c.Email.Contains(textBoxSearch.Text)).ToList();
        }

        // refresh page method...
        private void RefreshPage()
        {
            dataGridView1.DataSource = db.Contacts.ToList();
            ClearAllInputs();
        }

        // Clear all inputs values
        public void ClearAllInputs()
        {
            textBoxName.Text = "";
            textBoxSurname.Text = "";
            textBoxPhone.Text = "";
            textBoxEmail.Text = "";
        }

    }
}
