using System;
using ESRI.ArcGIS.Geometry;
using SOPFIM.Domain;
using Sopfim.Reports;

namespace Sopfim.CustomControls
{
    public interface IMapControl
    {
        event EventHandler MapLoaded;
        void ZoomToExtent(IEnvelope extent);
        void AddReportMenuItems<T>(IBaseExcelExportCommand<T>[] reportCommand) where T : EditableEntity, new();
        void RefreshMap();
    }
}