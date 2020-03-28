using System;
using System.Text;
using System.Threading.Tasks;

namespace Devscord.Progressor
{
    public static class ChartGenerator
    {
        private static RenderingService _renderingService;

        internal static RenderingService RenderingService
        {
            get
            {
                if (_renderingService == null)
                    _renderingService = new RenderingService();
                return _renderingService;
            }
        }

        public static ChartFile Bar(ChartData chartData, ResultImageConfiguration configuration)
        {
            return RenderingService.RenderBar(chartData, configuration);
        }
    }
}
