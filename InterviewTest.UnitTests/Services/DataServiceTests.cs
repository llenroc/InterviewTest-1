using InterviewTest.Abstractions;
using InterviewTest.Framework;
using InterviewTest.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Collections;
using InterviewTest.Definitions;
using System.Linq;
using Newtonsoft.Json;

namespace InterviewTest.UnitTests.Services
{
    public class PersonGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<Person>() { }, "Corrupted JSON" },
            new object[] { new List<Person>() { }, "" },
            new object[] { new List<Person>() { new Person() {
                 age = 23,
                 name = "Bob",
                 gender = "Male",
                 pets = new List<Pet>()
                 {
                     new Pet() { name = "Garfield", type="Cat" },
                     new Pet() {name = "Fido", type="Dog"}
                 }

            } },
                "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]}]" }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    public class DataServiceTests
    {
        private readonly DataService _dataService;
        private readonly Mock<IHttpClient> _httpClient;
        private readonly IConfigurationRoot _configuration = new Configuration();

        public DataServiceTests()
        {
            _httpClient = new Mock<IHttpClient>();

            _dataService = new DataService(_httpClient.Object, _configuration);
        }

        /// <summary>
        /// Ensures JSON is correctly converted into list of Persons
        /// </summary>
        [Theory]
        [ClassData(typeof(PersonGenerator))]
        public async Task DataMatchesJson(IList<Person> persons, string json)
        {
            _httpClient.Reset();
            _httpClient.Setup(x => x.Get(It.IsAny<IHttpRequest>())).ReturnsAsync(new HttpResponse() { IsSuccess = true, Data = json });
            
            if (json == "Corrupted JSON")
                await Assert.ThrowsAsync<JsonReaderException>(async () => { await _dataService.GetPersonData(); });
            else
            {
                var result = await _dataService.GetPersonData();

                Assert.NotNull(result);
                Assert.Equal(persons.Count, result.Count);
                Assert.True(persons.SequenceEqual(result));
            }
        }
    }

    class Configuration : IConfigurationRoot
    {
        public string this[string key]
        {
            get
            {
                if (key == "PersonUrl") return "https://testurl.com";

                throw new KeyNotFoundException();
            }
            set { }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }
    }

}
