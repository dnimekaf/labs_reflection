using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serializer.Library.Abstract;
using Serializer.Library.Service;
using Serializer.Tests.Assets;
using Xunit;
using Xunit.Abstractions;

namespace Serializer.Tests
{
    public class SerializerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SerializerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private ISerializer Arrange()
        {
            return new CsvSerializer();
        }
        
        [Fact]
        public void Serialize_String()
        {
            var source = "10 негритят пошли купаться в море";
            var service = Arrange();
            var result = service.Serialize(source);
            Assert.NotNull(result);
            Assert.Equal(source, result);
        }
        
        [Fact]
        public void Deserialize_String()
        {
            var source = "10 негритят пошли купаться в море";
            var service = Arrange();
            var serialized = service.Serialize(source);
            var result = service.Deserialize<string>(serialized);
            Assert.NotNull(result);
            Assert.Equal(source, result);
        }

        [Fact]
        public void Serialize_F_Empty_Ok()
        {
            var source = new F();
            var service = Arrange();
            var serialized = service.Serialize(source);
            Assert.NotNull(serialized);
            Assert.Equal("0,0,0,0,0", serialized);
        }
        
        [Fact]
        public void Serialize_F_With_Data_Ok()
        {
            var source = new F();
            var service = Arrange();
            var serialized = service.Serialize(source.Get());
            Assert.NotNull(serialized);
            Assert.Equal("1,2,3,4,5", serialized);
        }

        [Fact]
        public void Deserialize_Empty_F()
        {
            var source = new F();
            var service = Arrange();
            var serialized = service.Serialize(source);
            var result = service.Deserialize<F>(serialized);
            Assert.NotNull(result);
            Assert.IsType<F>(result);
            Assert.Equal("00000", result.ToString());
        }
        
        [Fact]
        public void Deserialize_F_With_Data()
        {
            var source = new F();
            var service = Arrange();
            var serialized = service.Serialize(source.Get());
            var result = service.Deserialize<F>(serialized);
            Assert.NotNull(result);
            Assert.IsType<F>(result);
            Assert.Equal("12345", result.ToString());
        }

        [Fact]
        public async Task Deserialize_Addresses_From_File()
        {
            var addresses = await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "addresses.csv"));
            Assert.NotNull(addresses);

            var service = Arrange();
            var result = service.Deserialize<Address>(addresses);
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.NotEmpty(resultList);
            Assert.Equal(6, resultList.Count);
            var john = resultList[0];
            Assert.Equal("John", john.FirstName);
            Assert.Equal("Doe", john.LastName);
            Assert.Equal("120 jefferson st.", john.AddressLine);
            Assert.Equal("Riverside", john.City);
            Assert.Equal(" NJ", john.State);
            Assert.Equal(" 08075", john.Zip);

            var daMan = resultList[2];
            Assert.Equal("John \"Da Man\"", daMan.FirstName);
            var blankman = resultList[4];
            Assert.Equal(string.Empty, blankman.FirstName);
            Assert.Equal("Blankman", blankman.LastName);
            Assert.Equal(string.Empty, blankman.AddressLine);

            var doubleQuotes = resultList[5];
            Assert.Equal("Joan \"the bone\", Anne", doubleQuotes.FirstName);
            Assert.Equal("Jet", doubleQuotes.LastName);
            Assert.Equal("9th, at Terrace plc", doubleQuotes.AddressLine);
        }
        
        [Fact]
        public async Task Deserialize_Addresses_Timing()
        {
            var addresses = await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "addresses.csv"));
            var service = Arrange();

            var sw = new Stopwatch();
            sw.Start();
            var result = service.Deserialize<Address>(addresses);
            sw.Stop();
            _testOutputHelper.WriteLine($"Addresses deserialization in ticks: {sw.ElapsedTicks}");
        }
    }
}