using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace ADONetExam
{
    public class Catgory
    {
        public string CatName { get; set; }
        public int CatId { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private static VacancyContainer2 db = new VacancyContainer2();
        private static string path = "http://vacancy.kharkov.ua/widgets/rssfeeds/rss";
       private static List<Vacancy> VacancyList = new List<Vacancy>();
        XDocument xdoc = new XDocument();
        Catgory cat, catF;
        public MainWindow()
        {
            InitializeComponent();
            List<Catgory> ctegories = new List<Catgory>();
            ctegories.Add(new Catgory() { CatName = "Бухгалтерия, финансы и учёт", CatId = 1 });
            ctegories.Add(new Catgory() { CatName = "Дизайн, полиграфия", CatId = 2 });
            ctegories.Add(new Catgory() { CatName = "Логистика, склад, ВЭД", CatId = 3 });
            ctegories.Add(new Catgory() { CatName = "Маркетинг, реклама и PR", CatId = 4 });
            ctegories.Add(new Catgory() { CatName = "Медицина, фармацевтика", CatId = 5 });
            CategorySave.ItemsSource = ctegories;
            CategorySearch.ItemsSource = ctegories;
           



        }
        private void CategorySave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cat = ((Catgory)((ComboBox)sender).SelectedItem);
            path = string.Format("{0}/?category={1}", path, cat.CatId);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            #region Load Xml
            xdoc = XDocument.Load(path);
            VacancyList = xdoc.Element("rss").Element("channel").Elements()
                        .Where(w => w.Name == "item")
                        .Select(s => new Vacancy
                        {
                            NameVacancy = s.Element("title").Value,
                            URLVacancy = s.Element("link").Value,
                            DescriptionVacancy = s.Element("description").Value,
                            DatePublicationVacancy = DateTime.Parse(s.Element("pubDate").Value),
                            EmailAuthorVacancy = s.Element("author").Value

                        })
                            .ToList();
            #endregion


            LVVacancy2.ItemsSource = VacancyList;
            DBInfo.ItemsSource = VacancyList;
            path = "http://vacancy.kharkov.ua/widgets/rssfeeds/rss";

        }

      
        private void Find_Click(object sender, RoutedEventArgs e)
        {
           
            Vacancy newvacancy = new Vacancy();
            
            try
            {             
                newvacancy.Id = catF.CatId-1;
                newvacancy.DatePublicationVacancy = DateSearch.SelectedDate.Value;
                newvacancy.EmailAuthorVacancy = Email.Text;
                newvacancy.DescriptionVacancy = Fraza.Text;
            }
            catch (Exception)
            {

                MessageBox.Show("Заполните основные поля Категория и Дату");
            }

            try
            {
                var finded = VacancyList.Where(w => w.Id == newvacancy.Id
                ||
                w.DatePublicationVacancy <= newvacancy.DatePublicationVacancy ||
                w.EmailAuthorVacancy == newvacancy.EmailAuthorVacancy ||
                w.DescriptionVacancy == newvacancy.DescriptionVacancy
                );
                LVVacancy.ItemsSource = finded;

            
              
            }
            catch (Exception)
            {

                MessageBox.Show("Not Find");
            }
            newvacancy = null;
           

        }

        private void CategoryFind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            catF = ((Catgory)((ComboBox)sender).SelectedItem);
            path = string.Format("{0}/?category={1}", path, catF.CatId);
            LVVacancy.ItemsSource = "";
          

        }

        private void TabItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VacancyList.Count == 0)
                MessageBox.Show("Для поиска данные сначала надо сохранить!");
        }

        private void DropTable_Click(object sender, RoutedEventArgs e)
        {
            DBInfo.ItemsSource = "";
            VacancyList = null;
            //удалить таблицу с базы данных ?
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            VacancyList.Clear();
            
            LVVacancy2.ItemsSource = "";
        }
    }
}
