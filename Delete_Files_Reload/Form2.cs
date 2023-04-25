using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Delete_Files_Reload
{
    public partial class Form2 : Form
    {
        public Form2(string directory,string extension,bool subdirectory)
        {
            InitializeComponent();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(directory);
            System.IO.FileInfo[] files;
            if (subdirectory)
            {
                files =
    di.GetFiles(extension, System.IO.SearchOption.AllDirectories);
            }
            else
            {
                files =
    di.GetFiles(extension, System.IO.SearchOption.TopDirectoryOnly);
            }
            checkedListBox1.CheckOnClick = true;

            checkedListBox1.Items.AddRange(files);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)this.Owner).main_visible();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(var item in checkedListBox1.CheckedItems)
            {
                bool delete = true;
                // FileInfoのインスタンスを生成する
                FileInfo fileInfo = new FileInfo(item.ToString());


                // 削除するファイルの読み取り専用属性を確認
                if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    //メッセージボックスを表示する
                    DialogResult result = MessageBox.Show(item.ToString() + " のReadOnlyを解除していいですか？",
                        "質問",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        // 読み取り専用属性を削除
                        fileInfo.Attributes &= ~FileAttributes.ReadOnly;
                    }
                    else
                    {
                        delete = false;
                    }

                }

                //try
                //{
                    // ファイルを削除する
                    if (delete)
                        fileInfo.Delete();
                //}


            }
            MessageBox.Show("削除処理が完了しました",
"やったね！",
MessageBoxButtons.OK);

            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, !checkedListBox1.GetItemChecked(i));
            }
        }
    }
}
