using GestionBanque.Models.DataService;
using GestionBanque.ViewModels;
using System.Windows;


namespace GestionBanque.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            InteractionUtilisateurGui iug = new InteractionUtilisateurGui();
            DataContext = new MainViewModel(iug, new BanqueViewModel(iug, new ClientSqliteDataService("banque.bd"), new CompteSqliteDataService("banque.bd")));
        }
    }
}
