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
    public partial class ManageSnippets : Form
    {
        string title_copy = string.Empty;
        public ManageSnippets()
        {
            InitializeComponent();
        }

        private void ManageSnippets_Load(object sender, EventArgs e)
        {
            foreach(string ct in Main.Categories)
            {
                comboBox1.Items.Add(ct);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void saveSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                var item = Main.Snippets.First(x => x.title == textBox2.Text);
                Main.Snippets.Remove(item);
                savesnippets();
                MessageBox.Show("Snippet deleted");
                textBox2.Clear();


        }

        private void savesnippets()
        {
            string jsonData = JsonConvert.SerializeObject(Main.Snippets);
            File.WriteAllText(Main.file, jsonData);
        }

        private void saveSnippetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            var item = Main.Snippets.First(x => x.title == title_copy);
            item.title = textBox2.Text;
            item.category = comboBox1.SelectedItem.ToString();
            item.code = textBox1.Text;
            savesnippets();
        }

        private void search()
        {
            StringBuilder strb = new StringBuilder();
            string title = textBox2.Text;
            title_copy = textBox2.Text;
            string cat;
            if (comboBox1.SelectedIndex > -1)
            {
                cat = comboBox1.SelectedItem.ToString();
            }
            else
            {
                cat = null;
            }

            foreach (Snippet x in Main.Snippets)
            {
                if (title == x.title)
                {

                    if (cat != null && cat == x.category)
                    {
                        textBox1.Text = x.code;
                    }

                }
            }
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                search();
            }
        }
    }
}
