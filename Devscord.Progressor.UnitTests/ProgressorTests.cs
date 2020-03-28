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
        [Ignore("")]
        public void ShouldSaveImageOnDrive()
        {
            //Arrange
            var chartData = new ChartData 
            { 
                Items = new List<ChartItem> 
                { 
                    new ChartItem(DateTime.UtcNow.AddDays(-2).ToShortDateString(), 1),
                    new ChartItem(DateTime.UtcNow.AddDays(-1).ToShortDateString(), 2),
                    new ChartItem(DateTime.UtcNow.AddDays(0).ToShortDateString(), 3),
                    new ChartItem(DateTime.UtcNow.AddDays(1).ToShortDateString(), 2),
                } 
            };
            var configuration = new ResultImageConfiguration { Width = 1280, Height = 720, Path = $@"C:\Temp\{Guid.NewGuid()}.png" };

            //Act
            ChartGenerator.Bar(chartData, configuration);

            //Assert
            var fileInfo = new FileInfo(configuration.Path);
            Assert.That(fileInfo.Exists, Is.True);
            Assert.That(fileInfo.Length, Is.GreaterThan(128));
        }
    }
}
