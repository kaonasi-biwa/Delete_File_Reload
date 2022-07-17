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
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;

namespace Delete_Files_Reload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { FileName = "SelectFolder", Filter = "Folder|.", CheckFileExists = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var path = Path.GetDirectoryName(ofd.FileName);
                    textBox1.Text = path;
                    listBox1.Items.Remove(path);
                    listBox1.Items.Insert(0,path);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string path in Properties.Settings.Default.directory.Split(","))
            {
                if(path != "")
                    listBox1.Items.Add(path);
            }
            foreach (string path in Properties.Settings.Default.extension.Split(","))
            {
                if (path != "")
                    listBox2.Items.Add(path);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(listBox1.Items.Count != 0)
            {
                string save_text = "";
                foreach (string list_item in listBox1.Items)
                {
                    if (list_item != "")
                        save_text = save_text + list_item + ",";
                }
                save_text = save_text.Substring(0, save_text.Length - 1);
                Properties.Settings.Default.directory = save_text;

            }

            if (listBox2.Items.Count != 0)
            {
                string save_text = "";
                foreach (string list_item in listBox2.Items)
                {
                    if (list_item != "")
                        save_text = save_text + list_item + ",";
                }
                save_text = save_text.Substring(0, save_text.Length - 1);
                Properties.Settings.Default.extension = save_text;
            }
            Properties.Settings.Default.Save();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            listBox2.Items.Clear();
            foreach (string path in Properties.Settings.Default.extension.Split(","))
            {
                if (path != "")
                    listBox2.Items.Add(path);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Insert(0, textBox2.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(listBox2.SelectedIndex >= 1)
            {
                var list_temp = listBox2.Items[listBox2.SelectedIndex - 1];
                listBox2.Items[listBox2.SelectedIndex - 1] = listBox2.SelectedItem;
                listBox2.Items[listBox2.SelectedIndex] = list_temp;
                textBox2.Text = list_temp.ToString();
                listBox2.SelectedIndex = listBox2.SelectedIndex - 1;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < listBox2.Items.Count - 1 && listBox2.SelectedIndex >= 0)
            {
                var list_temp = listBox2.Items[listBox2.SelectedIndex + 1];
                listBox2.Items[listBox2.SelectedIndex + 1] = listBox2.SelectedItem;
                listBox2.Items[listBox2.SelectedIndex] = list_temp;
                textBox2.Text = list_temp.ToString();
                listBox2.SelectedIndex = listBox2.SelectedIndex + 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("ディレクトリを選択してください",
    "エラー",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error);
            }else if (textBox2.Text == "")
            {
                MessageBox.Show("拡張子を選択してください",
"エラー",
MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
            else
            {

                var form2 = new Form2(textBox1.Text, textBox2.Text, checkBox1.Checked);
                form2.Owner = this;
                form2.StartPosition = FormStartPosition.Manual;
                form2.Height = this.Height;
                form2.Width = this.Width;
                form2.Top = this.Top;
                form2.Left = this.Left;
                form2.ShowDialog();
            }


            
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if(listBox2.SelectedItem.ToString() != "")
                textBox2.Text = listBox2.SelectedItem.ToString();
            */
        }

        private void listBox1_Click(object sender, EventArgs e)
        {/*
            if (listBox1.SelectedItem.ToString() != "")
                textBox1.Text = listBox1.SelectedItem.ToString();
            */
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
                textBox2.Text = listBox2.SelectedItem.ToString();
        }
        

        public void main_visible()
        {
            
            this.Focus();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
