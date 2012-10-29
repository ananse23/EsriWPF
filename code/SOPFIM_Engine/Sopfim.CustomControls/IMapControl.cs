using System;
using ESRI.ArcGIS.Geometry;

namespace Sopfim.CustomControls
{
    public interface IMapControl
    {
        event EventHandler MapLoaded;
        void ZoomToExtent(IEnvelope extent);
    }
}