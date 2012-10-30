using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Sopfim.CustomControls
{
    public class ZoomToLayer : BaseCommand 
    {
        private IMapControl3 _mapControl;
        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl3)hook;
            
        }

        public override string Caption
        {
            get
            {
                return "Zoom to layer";
            }
        }

        public override void OnClick()
        {
            ILayer layer = (ILayer) _mapControl.CustomProperty;
            _mapControl.Extent = layer.AreaOfInterest;
        }
    }
}