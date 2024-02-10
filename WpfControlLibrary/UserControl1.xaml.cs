using Create_ReadXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        string names;
        static List<Media> tv = new List<Media>();
        string f;
        public UserControl1()
        {
            InitializeComponent();
        }
        public void atualizar()
        {

            var files = System.IO.Directory.GetFiles(@"D:\\Pasta de coisas\\Programação\\Create_ReadXML\\Create_ReadXML\\Save_Media", "*.*", System.IO.SearchOption.AllDirectories);

            if (files.Length > 0)
            {

                XmlSerializer serial = new XmlSerializer(typeof(List<Media>));
                using (FileStream f = new FileStream("D:\\Pasta de coisas\\Programação\\Create_ReadXML\\Create_ReadXML\\Save_Media\\media.xml", FileMode.Open, FileAccess.Read))
                {


                    tv = serial.Deserialize(f) as List<Media>;

                }


                
                datagrid.DataContext = tv;
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

   



        private void Writebtn_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serial = new XmlSerializer(typeof(List<Media>));
            using (FileStream f = new FileStream("D:\\Pasta de coisas\\Programação\\Create_ReadXML\\Create_ReadXML\\Save_Media\\media.xml", FileMode.Create, FileAccess.Write))
            {
                serial.Serialize(f, tv);
                MessageBox.Show("Created");
            }
            atualizar();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Readbtn_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serial = new XmlSerializer(typeof(List<Media>));
            using (FileStream f = new FileStream("D:\\Pasta de coisas\\Programação\\Create_ReadXML\\Create_ReadXML\\Save_Media\\media.xml", FileMode.Open, FileAccess.Read))
            {


                tv = serial.Deserialize(f) as List<Media>;

            }


            datagrid.DataContext = tv;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            f = Convert.ToString(datagrid.CurrentItem);


            System.Diagnostics.Process pr = new System.Diagnostics.Process();
            string n = AddQuotesIfRequired(@"""C:\Program Files\VideoLAN\VLC\vlc.exe""");
            pr.StartInfo.FileName = n;
            pr.StartInfo.Arguments = AddQuotesIfRequired(f);
            Console.WriteLine(pr.StartInfo.Arguments);
            pr.Start();
        }
    }
}