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
            for (int i = 0; i < itemsCount; i++)
            {
                var currentItem = chartData.Items.Skip(i).First();

                var leftUpX = paddingHorizontal + itemFullWidth * i;
                if (i > 0)
                    leftUpX += (int)itemMargin;

                var width = (int)itemFullWidth - (int)(itemMargin * 2);
                var height = contentChartHeight * currentItem.Value / maxValue;
                var leftDownY = (int)(configuration.Height - paddingVertical);
                image.DrawRectangle((int)leftUpX, leftDownY, width, (int)height, Color.Blue);

                if(i == 0 || i % (int)(lastLength * 0.7) == 0)
                {
                    image.DrawRectangle((int)leftUpX, leftDownY + width,(int)width, (int)width, Color.Black);
                    using (Graphics graphic = Graphics.FromImage(image))
                    {
                        var rectf = new RectangleF((int)leftUpX, leftDownY + width, currentItem.Label.Length * (int)width, (int)width * 2);
                        graphic.DrawString(currentItem.Label, new Font("Arial", (int)width), Brushes.Black, rectf);
                    }
                    lastLength = currentItem.Label.Length;
                }
                
            }
            
            image.Save(chartFile.Path, ImageFormat.Png);
            return chartFile;
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
