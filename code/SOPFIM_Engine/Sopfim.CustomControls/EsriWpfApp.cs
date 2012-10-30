using System;
using System.Windows;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using log4net;

namespace Sopfim.CustomControls
{
    public class EsriWpfApp : Application
    {
        private AoInitialize _oInitialize;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                LogManager.GetLogger(typeof(EsriWpfApp)).Info("=====================================================");
                LogManager.GetLogger(typeof(EsriWpfApp)).Info("Start Product");
                InitializeEngineLicense();
                LogManager.GetLogger(typeof(EsriWpfApp)).Info("Initialized license");
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ErrorLogger").Fatal("Cannot initailize license", ex);
                Application.Current.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
            _oInitialize.Shutdown();
            LogManager.GetLogger(typeof(EsriWpfApp)).Info("Shutdown the license");
            LogManager.GetLogger(typeof(EsriWpfApp)).Info("=====================================================");
            base.OnExit(e);
        }

        private void InitializeEngineLicense()
        {
            var t = RuntimeManager.Bind(ProductCode.Desktop);
            _oInitialize = new AoInitialize();

            const esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeArcInfo;
            var licenseStatus = _oInitialize.IsProductCodeAvailable(productCode);
            if (_oInitialize.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
                _oInitialize.Initialize(productCode);
            else
                MessageBox.Show(string.Format("La licence demandée ({0}) n'est pas disponible({1})",
                                              productCode.ToString(), licenseStatus.ToString()));
        }
    }
}