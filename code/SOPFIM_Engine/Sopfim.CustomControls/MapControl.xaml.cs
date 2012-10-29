using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using log4net;

namespace Sopfim.CustomControls
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl, IMapControl
    {
        private AxMapControl _mapControl;
        private AxToolbarControl _toolbarControl;
        private AxTOCControl _tocControl;
        private readonly string _mxdFile;
        public event EventHandler MapLoaded;

          // Invoke the Changed event; called whenever list changes
        protected virtual void OnMapLoaded(EventArgs e) 
        {
              if (MapLoaded != null)
                  MapLoaded(this, e);
        }

        public MapControl()
        {
            InitializeComponent();
            _mxdFile = ConfigurationManager.AppSettings["mxdFile"];
        }

        private void CreateMapControl()
        {
            try
            {
                _mapControl = new AxMapControl();
                mapHost.Child = _mapControl;
                _mapControl.LoadMxFile(_mxdFile);
                _toolbarControl = new AxToolbarControl();

                toolbarHost.Child = _toolbarControl;
                _toolbarControl.SetBuddyControl(_mapControl);
                _toolbarControl.AddItem("esriControls.ControlsMapNavigationToolbar");
                _toolbarControl.AddItem("esriControls.ControlsSelectFeaturesTool");
                _toolbarControl.AddItem("esriControls.ControlsClearSelectionCommand");
                _toolbarControl.AddItem("esriControls.ControlsMapIdentifyTool");
                _toolbarControl.AddItem("esriControls.ControlsMapMeasureTool");

                _tocControl = new AxTOCControl();
                _tocHost.Child = _tocControl;
                _tocControl.Update();
                _tocControl.SetBuddyControl(_mapControl);
                LogManager.GetLogger(typeof(MapControl)).Info("Loaded Map: " + _mxdFile);
            }
            catch(TargetInvocationException exception)
            {
                var ex = (COMException) exception.InnerException;
                if (ex.ErrorCode == -2146827269)
                {
                    throw new FileNotFoundException(string.Format("map file {0} was not found", _mxdFile), _mxdFile);
                }
            }
            catch (Exception exception)
            {
                LogManager.GetLogger("ErrorLogger").Fatal("Error Initializing the map", exception);
                LogManager.GetLogger("ErrorLogger").Fatal("the map file should be in the location: " + _mxdFile);
                throw;
            }
        }

        private void mapHost_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateMapControl();
            OnMapLoaded(EventArgs.Empty);
        }

        public void ZoomToExtent(IEnvelope extent)
        {
            if (extent.IsEmpty)
                Xceed.Wpf.Toolkit.MessageBox.Show("The record has empty geometry");
            else
            {
                _mapControl.ActiveView.Extent = extent;
                _mapControl.ActiveView.Refresh();
            }
        }
    }
}
