using InterviewTest.Abstractions;
using InterviewTest.Definitions;
using InterviewTest.Models;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using InterviewTest.UnitTests.Extensions;

namespace InterviewTest.UnitTests.Models
{


    public class PersonGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new List<Person>() { new Person() { name = "Jane", gender = "Female", pets = new List<Pet>() { new Pet() { name = "Zordon" }, new Pet() { name="Chip" } } }, new Person() { name="Joe", gender="Male" } } }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MainModelTests
    {
        private readonly MainModel _mainModel;
        private readonly Mock<IDataService> _dataService;
        public MainModelTests()
        {
            _dataService = new Mock<IDataService>();

            _mainModel = new MainModel(_dataService.Object);
        }

        [Theory]
        [ClassData(typeof(PersonGenerator))]
        public async Task SortingLogic(IList<Person> personList)
        {
            _dataService.Setup(x => x.GetPersonData()).ReturnsAsync(personList);

            var list = await _mainModel.GetPetList();

            Assert.NotNull(list);

            // Sum of all Persons is equal to source PersonList
            Assert.Equal(personList.Count, list.Sum(x => x.Value.Count()));

            // Gender Count to Categories
            Assert.Equal(personList.Select(x => x.gender).Distinct().Count(), list.Count);

            // Alphabetical ordering of Pets
            foreach (var gender in list)
                Assert.Equal(true, gender.Value.ToList().IsOrderedBy(x => x.name));

        }

    }
}
