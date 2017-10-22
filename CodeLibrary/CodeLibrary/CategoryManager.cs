using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace CodeLibrary
{
    public partial class CategoryManager : Form
    {

        public CategoryManager()
        {
            InitializeComponent();
        }

        private void CategoryManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            string jsonData = JsonConvert.SerializeObject(Main.Categories);
            File.WriteAllText(Main.CatFile, jsonData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.Categories.Add(textBox1.Text);
            loadCategories();
          
        }
        private void loadCategories()
        {
            listBox1.Items.Clear();
            foreach (string x in Main.Categories)
            {
                listBox1.Items.Add(x);
            }
        }

        private void CategoryManager_Load(object sender, EventArgs e)
        {
            loadCategories();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            textBox2.Text = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.Categories[listBox1.SelectedIndex] = textBox2.Text;
            loadCategories();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                Main.Categories.RemoveAt(listBox1.SelectedIndex);
                loadCategories();
            }
            else
            {
                MessageBox.Show("Please select a category");
            }
        }

       
    }
}
