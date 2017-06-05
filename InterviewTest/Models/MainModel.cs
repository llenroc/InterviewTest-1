using InterviewTest.Abstractions;
using InterviewTest.Definitions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTest.Models
{
    public class MainModel: IMainModel
    {
        private readonly IDataService _dataService;
        public MainModel(IDataService dataService)
        {
            _dataService = dataService;
        }
       
        public async Task<IDictionary<string, IEnumerable<Pet>>> GetPetList()
        {
            var list = await _dataService.GetPersonData();

            var categorizedList = list.GroupBy(x => x.gender)
                                      .ToDictionary(x => x.Key, x=>x.Where(z=>z.pets != null).SelectMany(y=>y.pets)
                                      .OrderBy(y=>y.name).AsEnumerable());
            
            return categorizedList;
        }

    }
}
