using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using SharpCompress.Archive.Rar;
using SharpCompress.Archive;
using SharpCompress.Common;

namespace Comic_Book_Translater
{
    public partial class frmReader : Form
    {
        string text = "";
        public static object width { get; private set; }

        public frmReader()
        {
            InitializeComponent();
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
           







        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
           


        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
           
        }

    

        private void frmReader_Load(object sender, EventArgs e)
        {
            frmReader.width = 1478;
        }
        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                Tuple<Image, String> data = (Tuple<Image, String>)item.Tag;
                label1.Text = data.Item2;
                pictureBox1.Image = data.Item1;
                Size sz = data.Item1.Size;
                label2.Text = sz.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Enter(object sender, EventArgs e)
        {

        }

        private void loadBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            imageList1.Images.Clear();

            string[] pics = System.IO.Directory.GetFiles(@"TestFolder"); //"TestFolder//");
            listView1.View = View.SmallIcon;
            listView1.SmallImageList = imageList1;

            imageList1.ImageSize = new Size(64, 64);
            for (int i = 0; i < pics.Length; i++)
            {
                Image img;
                using (FileStream fs = new FileStream(pics[i], FileMode.Open))
                {
                    try
                    {
                        img = Image.FromStream(fs);
                        imageList1.Images.Add(img);

                        ListViewItem item = new ListViewItem();
                        item.ImageIndex = imageList1.Images.Count - 1;
                        item.Text = System.IO.Path.GetFileNameWithoutExtension(pics[i]);
                        item.Tag = new Tuple<Image, String>(img, pics[i]);
                        listView1.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error is "+ ex +" "); 
                    };
                }


            }
        }

        private void viewSelectedPageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                Tuple<Image, String> data = (Tuple<Image, String>)item.Tag;
                label1.Text = data.Item2;
                pictureBox1.Image = data.Item1;
                Size sz = data.Item1.Size;
                label2.Text = sz.ToString();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                Tuple<Image, String> data = (Tuple<Image, String>)item.Tag;
                label1.Text = data.Item2;
                pictureBox1.Image = data.Item1;
                Size sz = data.Item1.Size;
                label2.Text = sz.ToString();
            }
        }

        private void frmReader_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == (Keys.Enter))
            {
                btnView_Click(sender,  e);
            }
        
    }

        private void btnView_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                Tuple<Image, String> data = (Tuple<Image, String>)item.Tag;
                label1.Text = data.Item2;
                pictureBox1.Image = data.Item1;
                Size sz = data.Item1.Size;
                label2.Text = sz.ToString();
            }

           
            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            try
            {
                var dir = new DirectoryInfo("TestFolder");
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
                
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void documentwriter(string data)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    wordApp.Visible = false;
                    object missing = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Word.Document writer = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                    writer.Content.SetRange(0, 0);

                    writer.Content.Text = data + Environment.NewLine;

                    object filename = saveFileDialog1.FileName;
                    writer.SaveAs(ref filename);
                    writer.Close(ref missing, ref missing, ref missing);
                    writer = null;
                    wordApp.Quit(ref missing, ref missing, ref missing);

                    wordApp = null;

                    MessageBox.Show("Document Saved At " + filename.ToString());
                }
                catch (Exception ep)
                {
                    MessageBox.Show("" + ep);

                }


            }


        }

        private void btnSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            text = " " + txtTxt.Text;
            documentwriter(text);
        }

        private void btnOpen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Open Word File";
            openFileDialog1.Filter = "Word Files (*doc)|*docx";   

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();

                object fileName = openFileDialog1.FileName;
                // Define an object to pass to the API for missing parameters
                object missing = System.Type.Missing;
                doc = word.Documents.Open(ref fileName,ref missing, ref missing);

                String read = string.Empty;
                List<string> data = new List<string>();
                for (int i = 0; i < doc.Paragraphs.Count; i++)
                {
                    string temp = doc.Paragraphs[i + 1].Range.Text.Trim();
                    if (temp != string.Empty)
                        data.Add(temp);
                }
                doc.Close();
                word.Quit();
                foreach(var item in data)
                   {
                    txtTxt.Text += item;
                }

            } 
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string subPath = "TestFolder";

            bool exists = System.IO.Directory.Exists((subPath));

            if (!exists)
            {
                System.IO.Directory.CreateDirectory((subPath));
            }

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    ZipFile.ExtractToDirectory(openFileDialog1.FileName, "TestFolder");
                    MessageBox.Show("File extracted successfully!");
                }
                catch
                {

                    using (var archive = RarArchive.Open(openFileDialog1.FileName))
                    {
                        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                        {
                            entry.WriteToDirectory("TestFolder", new ExtractOptions()
                            {

                            });

                        }
                        MessageBox.Show("File extracted successfully!");
                    }
                }
            }
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Open Word File";
            openFileDialog1.Filter = "Word Files (*doc)|*docx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();

                object fileName = openFileDialog1.FileName;
                // Define an object to pass to the API for missing parameters
                object missing = System.Type.Missing;
                doc = word.Documents.Open(ref fileName, ref missing, ref missing);

                String read = string.Empty;
                List<string> data = new List<string>();
                for (int i = 0; i < doc.Paragraphs.Count; i++)
                {
                    string temp = doc.Paragraphs[i + 1].Range.Text.Trim();
                    if (temp != string.Empty)
                        data.Add(temp);
                }
                doc.Close();
                word.Quit();
                foreach (var item in data)
                {
                    txtTxt.Text += item;
                }

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text = " " + txtTxt.Text;
            documentwriter(text);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            try
            {
                var dir = new DirectoryInfo("TestFolder");
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            imageList1.Images.Clear();

            string[] pics = System.IO.Directory.GetFiles(@"TestFolder"); //"TestFolder//");
            listView1.View = View.SmallIcon;
            listView1.SmallImageList = imageList1;

            imageList1.ImageSize = new Size(64, 64);
            for (int i = 0; i < pics.Length; i++)
            {
                Image img;
                using (FileStream fs = new FileStream(pics[i], FileMode.Open))
                {
                    try
                    {
                        img = Image.FromStream(fs);
                        imageList1.Images.Add(img);

                        ListViewItem item = new ListViewItem();
                        item.ImageIndex = imageList1.Images.Count - 1;
                        item.Text = System.IO.Path.GetFileNameWithoutExtension(pics[i]);
                        item.Tag = new Tuple<Image, String>(img, pics[i]);
                        listView1.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error is " + ex + " ");
                    };
                }


            }
        }
    }
}