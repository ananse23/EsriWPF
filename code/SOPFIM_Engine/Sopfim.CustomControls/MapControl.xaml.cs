using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

using ESRI.ArcGIS.esriSystem;
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
        private IToolbarMenu2 _contextMenu;
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

        private void CreateGraphicParts()
        {
            try
            {
                _mapControl = CreateAndHostMap(mapHost);
                _toolbarControl = CreateToolbar(_mapControl);
                _tocControl = CreateToc(_mapControl);
               
                _contextMenu = CreateContextMenu(_mapControl);
                _tocControl.OnMouseDown += new ITOCControlEvents_Ax_OnMouseDownEventHandler(axTocControl_OnMouseDown);

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

        private AxMapControl CreateAndHostMap(WindowsFormsHost host)
        {
            var axMapControl = new AxMapControl();
            host.Child = axMapControl;
            axMapControl.LoadMxFile(_mxdFile);
            return axMapControl;
        }

        private AxTOCControl CreateToc(AxMapControl _map)
        {
            var axTocControl = new AxTOCControl();
            _tocHost.Child = axTocControl;
            axTocControl.Update();
            axTocControl.SetBuddyControl(_map);
            return axTocControl;
        }

        void axTocControl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2) return;
             esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
             IBasicMap map = null; ILayer layer = null;
             object other = null; object index = null;
             _tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
             if (item == esriTOCControlItem.esriTOCControlItemMap)
                 _tocControl.SelectItem(map, null);
             else
                 _tocControl.SelectItem(layer, null);
            _mapControl.CustomProperty = layer;

            //if (item == esriTOCControlItem.esriTOCControlItemMap) m_menuMap.PopupMenu(e.x, e.y, m_tocControl.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer) _contextMenu.PopupMenu(e.x, e.y, _tocControl.hWnd);
        }

        private AxToolbarControl CreateToolbar(AxMapControl mapControl)
        {
            var axToolbarControl = new AxToolbarControl();

            toolbarHost.Child = axToolbarControl;
            axToolbarControl.SetBuddyControl(mapControl);
            axToolbarControl.AddItem("esriControls.ControlsMapNavigationToolbar");
            axToolbarControl.AddItem("esriControls.ControlsSelectFeaturesTool");
            axToolbarControl.AddItem("esriControls.ControlsClearSelectionCommand");
            axToolbarControl.AddItem("esriControls.ControlsMapIdentifyTool");
            axToolbarControl.AddItem("esriControls.ControlsMapMeasureTool");
            return axToolbarControl;
        }

        private IToolbarMenu2 CreateContextMenu(AxMapControl map)
        {
            var menu = (IToolbarMenu2) new ToolbarMenuClass();
            menu.AddItem(new ZoomToLayer(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
            menu.SetHook(map);
            return menu;
        }

        private void mapHost_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateGraphicParts();
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
