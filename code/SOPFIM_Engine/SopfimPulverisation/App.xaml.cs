using System.Windows.Threading;
using Sopfim.CustomControls;
using log4net;
using log4net.Config;

namespace SopfimPulverisation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : EsriWpfApp
    {
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            XmlConfigurator.Configure();
            base.OnStartup(e);
        }

        private void EsriWpfApp_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogManager.GetLogger("ErrorLogger").Fatal("unhandled error caught in the application", e.Exception);
            Xceed.Wpf.Toolkit.MessageBox.Show("Error caught: " + e.Exception.Message);
        }
    }
}
