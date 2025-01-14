using System;
using Toverland_Api.Models;
using Xunit;

namespace Toverland_Api.Tests
{
    public class FeedbackTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var id = 1;
            var description = "Great park!";
            var date = "2021-09-01";
            var rating = 2;

            // Act
            var feedback = new Feedback(id, description, date, rating);

            // Assert
            Assert.Equal(id, feedback.Id);
            Assert.Equal(description, feedback.Description);
            Assert.Equal(date, feedback.Date);
            Assert.Equal(rating, feedback.Rating);
        }

        [Fact]
        public void Rating_Setter_ShouldThrowException_WhenInvalidValue()
        {
            // Arrange
            var feedback = new Feedback();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => feedback.Rating = 3);
        }

        [Fact]
        public void Date_Setter_ShouldSetToToday_WhenNullOrEmptyOrString()
        {
            // Arrange
            var feedback = new Feedback();

            // Act
            feedback.Date = null;
            var dateWhenNull = feedback.Date;

            feedback.Date = "";
            var dateWhenEmpty = feedback.Date;

            feedback.Date = "string";
            var dateWhenString = feedback.Date;

            // Assert
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            Assert.Equal(today, dateWhenNull);
            Assert.Equal(today, dateWhenEmpty);
            Assert.Equal(today, dateWhenString);
        }

        [Fact]
        public void Date_Setter_ShouldSetToGivenDate_WhenValidDate()
        {
            // Arrange
            var feedback = new Feedback();
            var validDate = "2021-09-01";

            // Act
            feedback.Date = validDate;

            // Assert
            Assert.Equal(validDate, feedback.Date);
        }

        [Fact]
        public void Description_Setter_ShouldSetDescription()
        {
            // Arrange
            var feedback = new Feedback();
            var description = "Great park!";

            // Act
            feedback.Description = description;

            // Assert
            Assert.Equal(description, feedback.Description);
        }

        [Fact]
        public void Id_Setter_ShouldSetId()
        {
            // Arrange
            var feedback = new Feedback();
            var id = 1;

            // Act
            feedback.Id = id;

            // Assert
            Assert.Equal(id, feedback.Id);
        }
    }
}
