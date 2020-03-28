using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Devscord.Progressor
{
    internal class RenderingService
    {
        internal RenderingService()
        {
        }

        internal ChartFile RenderBar(ChartData chartData, ResultImageConfiguration configuration)
        {
            var chartFile = new ChartFile(configuration.Path);
            var image = new Bitmap(configuration.Width, configuration.Height, Graphics.FromHwnd(IntPtr.Zero));
            image.PaintBackground(configuration.Width, configuration.Height, Color.White);

            var itemsCount = chartData.Items.Count();
            var paddingHorizontal = configuration.Width * 0.1;
            var paddingVertical = configuration.Height * 0.1;

            var contentChartWidth = configuration.Width - paddingHorizontal * 2;
            var contentChartHeight = configuration.Height - paddingVertical * 2;

            var itemFullWidth = contentChartWidth / itemsCount;
            var itemMargin = itemFullWidth * 0.1;

            var maxValue = chartData.Items.Max(x => x.Value);
            var lastLength = 0;

            var roundedUpMax = RoundUp(maxValue);

            //draw scale
            var heightPerPoint = contentChartHeight / roundedUpMax;
            var possibleNumberIsDividedBy = roundedUpMax / 20;

            var widthPerPoint = (int)itemFullWidth - (int)(itemMargin * 2);

            for (int i = 0; i <= roundedUpMax; i ++)
            {
                if(i == 0 || i % possibleNumberIsDividedBy == 0)
                {
                    var h = paddingHorizontal / 2 + contentChartHeight - heightPerPoint * i;
                    image.DrawRectangle((int)(paddingHorizontal - widthPerPoint * 4) + heightPerPoint.ToString().Length * 10, (int)(h + widthPerPoint), widthPerPoint, widthPerPoint, Color.FromArgb(150, 150, 150));
                    using (Graphics graphic = Graphics.FromImage(image))
                    {
                        var rectf = new RectangleF((int)((int)(paddingHorizontal - widthPerPoint * 5)), (int)h, 100, 20);
                        graphic.DrawString(i.ToString(), new Font("Arial", (int)10), Brushes.Black, rectf);
                    }
                }
            }

            for (int i = 0; i < itemsCount; i++)
            {
                var currentItem = chartData.Items.Skip(i).First();

                var leftUpX = paddingHorizontal + itemFullWidth * i;
                if (i > 0)
                    leftUpX += (int)itemMargin;

                var height = currentItem.Value * heightPerPoint;
                var leftDownY = (int)(configuration.Height - paddingVertical);
                image.DrawRectangle((int)leftUpX, leftDownY, widthPerPoint, (int)height, Color.FromArgb(3, 144, 252));

                if(i == 0 || i % (int)(lastLength * 0.7) == 0)
                {
                    image.DrawRectangle((int)leftUpX, leftDownY + widthPerPoint, widthPerPoint, (int)widthPerPoint, Color.FromArgb(150, 150, 150));
                    using (Graphics graphic = Graphics.FromImage(image))
                    {
                        var rectf = new RectangleF((int)leftUpX, leftDownY + widthPerPoint, currentItem.Label.Length * (int)widthPerPoint, (int)widthPerPoint * 2);
                        graphic.DrawString(currentItem.Label, new Font("Arial", (int)widthPerPoint), Brushes.Black, rectf);
                    }
                    lastLength = currentItem.Label.Length;
                }
                
            }
            image.Save(chartFile.Path, ImageFormat.Png);
            return chartFile;
        }

        private int RoundUp(int input)
        {
            var asString = input.ToString();
            var startLength = asString.Length;
            if (startLength <= 2)
                return input;
            var rounded = int.Parse(asString.Substring(0, 2)) + 1;

            for (int i = 0; i < startLength - 2; i++)
            {
                rounded *= 10;
            }
            return rounded;
        }
    }

    internal static class DrawExtensions
    {
        internal static void DrawRectangle(this Bitmap image, int startX, int startDownY, int width, int height, Color color)
        {
            for (int x = startX; x < startX + width; x++)
            {
                for (int y = startDownY; y > startDownY - height; y--)
                {
                    image.SetPixel(x, y, color);
                }
            }
        }

        internal static void PaintBackground(this Bitmap image, int width, int height, Color color)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }
        }
    }
}
