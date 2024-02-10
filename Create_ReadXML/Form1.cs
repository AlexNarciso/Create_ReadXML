using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace Create_ReadXML
{
    public partial class Form1 : Form
    {
        string names;
        string types;
        static List<Media> tv = new List<Media>();
        string f;
        static XDocument xdoc;
        


        public Form1()
        {
            InitializeComponent();
            atualizar();
            
        }


        public void atualizar()
        {

            var files = System.IO.Directory.GetFiles(@"D:\\Programas\\C#\\Create_ReadXML\\Create_ReadXML\\Save_Media", "*.*", System.IO.SearchOption.AllDirectories);

            if (files.Length > 0)
            {

                XmlSerializer serial = new XmlSerializer(typeof(List<Media>));
                using (FileStream f = new FileStream("D:\\Programas\\C#\\Create_ReadXML\\Create_ReadXML\\Save_Media\\media.xml", FileMode.Open, FileAccess.Read))
                {


                    tv = serial.Deserialize(f) as List<Media>;
                    

                }
                

                dataGridView1.DataSource = tv;
                
               
            }
            else
            {
                MessageBox.Show("Precisa de Dados");
            }
        }




        public string AddQuotesIfRequired(string path)
        {
           
                    return !string.IsNullOrWhiteSpace(path) ?
                        path.Contains(" ") && (!path.StartsWith("\"") && !path.EndsWith("\"")) ?
                            "\"" + path + "\"" : path :
                            string.Empty;
                
            
        }



        private void Writebtn_Click(object sender, EventArgs e)
        {
            
            
            XmlSerializer serial = new XmlSerializer(typeof(List<Media>));
            using (FileStream f = new FileStream("D:\\Programas\\C#\\Create_ReadXML\\Create_ReadXML\\Save_Media\\media.xml", FileMode.Create, FileAccess.Write))
            {
                serial.Serialize(f, tv);
                MessageBox.Show("Created");
            }
            atualizar();
        
        }

        public void button1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dlg2 = new FolderBrowserDialog();
            dlg2.ShowDialog();
            var files = System.IO.Directory.GetFiles(dlg2.SelectedPath, "*.*", System.IO.SearchOption.AllDirectories);

            for (int iFile = 0; iFile < files.Length; iFile++)
            {


                names = new FileInfo(files[iFile]).Name;
                types = new FileInfo(files[iFile]).Extension;
                tv.Add(new Media() { Id = iFile, Name = names, Path = files[iFile], Type = types});


            }
        }

        private void Readbtn_Click(object sender, EventArgs e)
        {
            XmlSerializer serial = new XmlSerializer(typeof(List<Media>));
             using (FileStream f = new FileStream("D:\\Programas\\C#\\Create_ReadXML\\Create_ReadXML\\Save_Media\\media.xml", FileMode.Open, FileAccess.Read))
             {


                 tv = serial.Deserialize(f) as List<Media>;

             }


             dataGridView1.DataSource = tv;
            
           


        }

        private void button2_Click(object sender, EventArgs e)
        {
          


           f = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
            
            
            System.Diagnostics.Process pr = new System.Diagnostics.Process();
            string n = AddQuotesIfRequired(@"""C:\Program Files\VideoLAN\VLC\vlc.exe""");
            pr.StartInfo.FileName = n;
            pr.StartInfo.Arguments = AddQuotesIfRequired(f);
            Console.WriteLine(pr.StartInfo.Arguments);
            pr.Start();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument(); 
            xdoc.Load(@"D:\Programas\C#\Create_ReadXML\Create_ReadXML\Save_Media\media.xml");
           foreach(XmlNode xNode in xdoc.SelectNodes("Media"))
            {
                if(xNode.SelectSingleNode("Type").InnerText == textBox1.Text) xNode.RemoveAll();
            }
            xdoc.Save(@"D:\Programas\C#\Create_ReadXML\Create_ReadXML\Save_Media\media.xml");
            if (xdoc!= null)
            {
                MessageBox.Show("Deleted");
                atualizar();
            }
            else
            {
                MessageBox.Show("Erro");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
 }

    

