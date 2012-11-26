using System;
using System.Windows;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using Esri.CommonUtils;


namespace Sopfim.CustomControls
{
    public class EsriEngineApp : Application
    {
        private AoInitialize _oInitialize;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                Logger.Log("=====================================================");
                Logger.Log("Initialize license");
                InitializeEngineLicense();
                Logger.Log("License was initialized");
            }
            catch (Exception ex)
            {
                Logger.Error("Cannot initailize license", ex);
                Application.Current.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
            _oInitialize.Shutdown();
            Logger.Log("Shutdown the license");
            Logger.Log("=====================================================");
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