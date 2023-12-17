using Moq;
using NUnit.Framework;
using Serilog;


namespace SearchEngine.Tests
{
    [TestFixture]
    public class SearchWordEngineShould
    {
        [Test]
        public void SearchEngine_ReturnsCorrectResults()
        {
            // Arrange
            string[] dataset = { "Today is Sunday", "Today is not Monday" };

            // Create a mock logger
            var mockLogger = new Mock<ILogger>();

            // Action
            var searchEngine = new SearchWordEngine(dataset, mockLogger.Object);
            List<string> resultToday = searchEngine.Search("Today");
            List<string> resultSunday = searchEngine.Search("Sunday");
            List<string> resultTuesday = searchEngine.Search("Tuesday");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultToday, Is.EquivalentTo(new[] { "Today is Sunday", "Today is not Monday" }));
                Assert.That(resultSunday, Is.EquivalentTo(new[] { "Today is Sunday" }));
                Assert.That(resultTuesday, Is.Empty);
            });
        }

        [Test]
        public void SearchEngine_ReturnsCorrectResults2()
        {
            // Arrange
            string[] dataset = { "My name is Yael", "My name is John", "My cat drinks milk" };

            // Create a mock logger
            var mockLogger = new Mock<ILogger>();

            // Action
            var searchEngine = new SearchWordEngine(dataset, mockLogger.Object);
            List<string> myResult = searchEngine.Search("My");
            List<string> nameResult = searchEngine.Search("name");
            List<string> catResult = searchEngine.Search("cat");
            List<string> dogResult = searchEngine.Search("dog");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(myResult, Is.EquivalentTo(new[] { "My name is Yael", "My name is John", "My cat drinks milk" }));
                Assert.That(nameResult, Is.EquivalentTo(new[] { "My name is Yael", "My name is John" }));
                Assert.That(catResult, Is.EquivalentTo(new[] { "My cat drinks milk" }));
                Assert.That(dogResult, Is.Empty);
            });
        }

        [Test]
        public void SearchEngine_SameWordTwice_ReturnsCorrectResults()
        {
            // Arrange
            string[] dataset = { "My name is name", "My name is John", "My cat drinks milk" };

            // Create a mock logger
            var mockLogger = new Mock<ILogger>();

            // Action
            var searchEngine = new SearchWordEngine(dataset, mockLogger.Object);
            List<string> myResult = searchEngine.Search("My");
            List<string> nameResult = searchEngine.Search("name");
            List<string> catResult = searchEngine.Search("cat");
            List<string> dogResult = searchEngine.Search("dog");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(myResult, Is.EquivalentTo(new[] { "My name is name", "My name is John", "My cat drinks milk" }));
                Assert.That(nameResult, Is.EquivalentTo(new[] { "My name is name", "My name is John" }));
                Assert.That(catResult, Is.EquivalentTo(new[] { "My cat drinks milk" }));
                Assert.That(dogResult, Is.Empty);
            });
        }

        [Test]
        public void SearchEngine_EmptyDataSet_ReturnsCorrectResults()
        {
            // Arrange
            string[] dataset = { };

            // Create a mock logger
            var mockLogger = new Mock<ILogger>();

            // Action
            var searchEngine = new SearchWordEngine(dataset, mockLogger.Object);
            List<string> myResult = searchEngine.Search("My");

            // Assert
            Assert.That(myResult, Is.Empty);
        }

        [Test]
        public void SearchEngine_NullDataSet_ReturnsCorrectResults()
        {
            // Arrange
            string[] dataset = null;

            // Create a mock logger
            var mockLogger = new Mock<ILogger>();

            // Action
            var searchEngine = new SearchWordEngine(dataset, mockLogger.Object);
            List<string> myResult = searchEngine.Search("My");

            // Assert
            Assert.That(myResult, Is.Empty);
        }
    }
}
