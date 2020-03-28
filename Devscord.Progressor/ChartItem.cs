namespace Devscord.Progressor
{
    public class ChartItem
    {
        public string Label { get; set; }
        public int Value { get; set; }

        public ChartItem()
        {
        }

        public ChartItem(string label, int value)
        {
            this.Label = label;
            this.Value = value;
        }
    }
}
