using System.Collections.Generic;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.CustomControls;

namespace Sopfim.ViewModels
{
    // Hey , I know static class is not good, but there is no way to inject some service into the view model of the 
    // entities without rewriting the code
    // I don't have time to do this refactoring now
    // Ghassan Karwchan.
    public static class ApplicationSources
    {
        public static List<BlocTBE> Blocks { get; set; }
        public static IDataService DataService { get; set; }
        public static IMapControl MapControl { get; set; }
    }
}