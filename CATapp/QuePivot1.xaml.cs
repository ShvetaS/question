using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace CATapp
{
    public partial class QuePivot1 : UserControl
    {
        private CatAppDataContext catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
      //  public Int64 op;
        public QuePivot1()
        {
            InitializeComponent();
            // Connect to the database and instantiate data context.
            catAppDB = new CatAppDataContext(DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;


        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
 private ObservableCollection<Question> _ques;
        public ObservableCollection<Question> Questions
        {
            get
            {
                return _ques;
            }
            set
            {
                if (_ques != value)
                {
                    _ques = value;
                    NotifyPropertyChanged("Questions");
                }
            }
        }




        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);

            var q1 = from Question qs1 in catAppDB.Questions
                     join Question_category qs2 in catAppDB.Question_categories on qs1._id equals qs2.Q_id
                     where qs2.C_id == l
                     select qs1;
            Questions = new ObservableCollection<Question>(q1);
           

        }
    }
}
