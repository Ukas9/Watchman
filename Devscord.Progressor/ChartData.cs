using System.Collections.Generic;
using System.Linq;

namespace Devscord.Progressor
{
    public class ChartData
    {
        public IEnumerable<string> Labels => Items.Select(x => x.Label).Distinct();
        public IEnumerable<ChartItem> Items { get; set; }
    }
}
