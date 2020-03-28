using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Devscord.Progressor.UnitTests
{
    [TestFixture]
    public class ProgressorTests
    {
        [Test]
        //[Ignore("")]
        public void ShouldSaveImageOnDrive()
        {
            //Arrange
            var itemsCount = 60;

            var items = new List<ChartItem>();
            for (int i = 0; i < itemsCount; i++)
            {
                var item = new ChartItem(DateTime.Now.AddDays(i).ToShortDateString(), new Random().Next(0, 500));
                items.Add(item);
            }

            var chartData = new ChartData { Items = items };
            var configuration = new ResultImageConfiguration { Width = 16 * itemsCount, Height = 9 * itemsCount, Path = $@"Temp\{Guid.NewGuid()}.png" };

            //Act
            using var result = ChartGenerator.Bar(chartData, configuration);

            //Assert
            var fileInfo = new FileInfo(result.Path);
            Assert.That(fileInfo.Exists, Is.True);
            Assert.That(fileInfo.Length, Is.GreaterThan(1024));
        }
    }
}
