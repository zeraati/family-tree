using Newtonsoft.Json;
using FamilyTree.Model;
using Microsoft.AspNetCore.Mvc;
using FamilyTree.Service.PersonWithFamily;

namespace FamilyTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonWithFamilyService _personWithFamilyService;
        public HomeController(ILogger<HomeController> logger, IPersonWithFamilyService personWithFamilyService)
        {
            _logger = logger;
            _personWithFamilyService = personWithFamilyService;
        }

        public async Task<IActionResult> Index()
        {
            var personFamily = (await _personWithFamilyService.GetAllAsync()).Data;

            var familyTree = new List<PersonFamilyTreeDTO>();
            foreach (var person in personFamily)
            {
                if(person.FullName!=null && person.Gender != null)
                {
                    var personFamilyTree = new PersonFamilyTreeDTO(person.PersonId, person.FullName, person.Gender.ToLower())
                    {
                        photo=person.Photo,
                        fid = person.FatherId,
                        mid = person.MotherId,
                        pids = person.SpouseIds,
                    };
                    familyTree.Add(personFamilyTree);
                }
            }

            var setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.SerializeObject(familyTree, setting);

            ViewData["family-tree"] = result;
            return View();
        }
    }
}