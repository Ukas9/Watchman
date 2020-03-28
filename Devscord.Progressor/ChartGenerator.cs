using System;
using System.Text;
using System.Threading.Tasks;

namespace Devscord.Progressor
{
    public static class ChartGenerator
    {
        public static string Bar(ChartData chartData, ResultImageConfiguration configuration)
        {
            new RenderingService(configuration).RenderBar(chartData);
            return default;
        }
    }
}
