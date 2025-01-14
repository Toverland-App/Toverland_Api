using System;
using System.Collections.Generic;
using Toverland_Api.Models;
using Xunit;

namespace Toverland_Api.Tests
{
    public class AreaTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var id = 1;
            var name = "Avalon";
            var size = 15.0;

            // Act
            var area = new Area { Id = id, Name = name, Size = size };

            // Assert 
            Assert.Equal(id, area.Id);
            Assert.Equal(name, area.Name);
            Assert.Equal(size, area.Size);
        }

        [Fact]
        public void Name_Setter_ShouldSetName()
        {
            // Arrange
            var area = new Area();
            var name = "Avalon";

            // Act
            area.Name = name;

            // Assert
            Assert.Equal(name, area.Name);
        }

        [Fact]
        public void Size_Setter_ShouldSetSize()
        {
            // Arrange
            var area = new Area();
            var size = 15.0;

            // Act
            area.Size = size;

            // Assert
            Assert.Equal(size, area.Size);
        }

        [Fact]
        public void Attractions_Setter_ShouldSetAttractions()
        {
            // Arrange
            var area = new Area();
            var attractions = new List<Attraction>
            {
                new Attraction { Id = 1, Name = "Fēnix" },
                new Attraction { Id = 2, Name = "Troy" }
            };

            // Act
            area.Attractions = attractions;

            // Assert
            Assert.Equal(attractions, area.Attractions);
        }

        [Fact]
        public void Area_ShouldHaveDefaultValues_WhenDefaultConstructorIsUsed()
        {
            // Arrange & Act
            var area = new Area();

            // Assert
            Assert.Equal(0, area.Id);
            Assert.Null(area.Name);
            Assert.Equal(0, area.Size);
            Assert.Empty(area.Attractions);
        }

        [Fact]
        public void AddAttraction_ShouldAddAttractionToArea()
        {
            // Arrange
            var area = new Area();
            var attraction = new Attraction { Id = 1, Name = "Fēnix" };

            // Act
            area.Attractions.Add(attraction);

            // Assert
            Assert.Contains(attraction, area.Attractions);
        }

        [Fact]
        public void RemoveAttraction_ShouldRemoveAttractionFromArea()
        {
            // Arrange
            var area = new Area();
            var attraction = new Attraction { Id = 1, Name = "Fēnix" };
            area.Attractions.Add(attraction);

            // Act
            area.Attractions.Remove(attraction);

            // Assert
            Assert.DoesNotContain(attraction, area.Attractions);
        }

        [Fact]
        public void ClearAttractions_ShouldRemoveAllAttractionsFromArea()
        {
            // Arrange
            var area = new Area();
            var attractions = new List<Attraction>
            {
                new Attraction { Id = 1, Name = "Fēnix" },
                new Attraction { Id = 2, Name = "Troy" }
            };
            area.Attractions = attractions;

            // Act
            area.Attractions.Clear();

            // Assert
            Assert.Empty(area.Attractions);
        }
    }
}

