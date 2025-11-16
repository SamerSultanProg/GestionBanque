using Autofac;
using Autofac.Configuration;
using GestionBanque.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace GestionBanque.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            var config = new ConfigurationBuilder()
                .AddJsonFile("autofac.json")
                .Build();

            var module = new ConfigurationModule(config);
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
            var container = builder.Build();

            var mainVm = container.Resolve<MainViewModel>();
            DataContext = mainVm;
        }
    }
}
