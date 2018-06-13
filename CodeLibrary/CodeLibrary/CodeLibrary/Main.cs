using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace CodeLibrary
{
    public partial class Main : Form
    {
        static internal string file = "snippet.json";
        static internal string CatFile = "categories.json";
        static internal List<Snippet> Snippets;
        static internal List<string> Categories;

        public Main()
        {
            InitializeComponent();
            Snippets = new List<Snippet>();
            Categories = new List<string>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Snippet snpt = new Snippet();
            snpt.code = textBox1.Text;
            snpt.category = addCatCombobox.Text;
            snpt.title = textBox2.Text;
            Snippets.Add(snpt);
            string jsonData = JsonConvert.SerializeObject(Snippets);
            File.WriteAllText(file, jsonData);
            MessageBox.Show("Snippet added");
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(file))
            {
                string temp = File.ReadAllText(file);
                Snippets = JsonConvert.DeserializeObject<List<Snippet>>(temp);
                //Deserialise Categories
            }

            if (File.Exists(CatFile))
            {
                string temp2 = File.ReadAllText(CatFile);
                Categories = JsonConvert.DeserializeObject<List<string>>(temp2);
            }

            // Load categories
            loadcat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SnD skata = new SnD();
            skata.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CategoryManager ctm = new CategoryManager();
            ctm.ShowDialog();
            addCatCombobox.Items.Clear();
            addCatCombobox.ResetText();
            loadcat();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void loadcat()
        {
            addCatCombobox.Items.Add(string.Empty);
            foreach (string Ct in Categories)
            {
                addCatCombobox.Items.Add(Ct);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ManageSnippets ms = new ManageSnippets();
            ms.ShowDialog();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.Shift && e.KeyCode == Keys.S)
            {
                SnD temp = new SnD();
                temp.ShowDialog();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.S)
            {
                SnD temp = new SnD();
                temp.ShowDialog();
            }
        }
    }
}
