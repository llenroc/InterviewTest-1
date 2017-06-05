using InterviewTest.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTest.Abstractions
{
    public interface IDataService
    {
        Task<IList<Person>> GetPersonData();
    }
}
