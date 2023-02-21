using Newtonsoft.Json;
using FamilyTree.Model;
using FamilyTree.Service.User;
using Microsoft.AspNetCore.Mvc;
using FamilyTree.Helper.Extension;
using FamilyTree.Service.PersonWithFamily;

namespace FamilyTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IPersonWithFamilyService _personWithFamilyService;
        public HomeController(ILogger<HomeController> logger, IUserService userService,IPersonWithFamilyService personWithFamilyService)
        {
            _logger = logger;
            _userService = userService;
            _personWithFamilyService = personWithFamilyService;
        }

        public async Task<IActionResult> Index(string username,string pass)
        {
            var checkUser=(await _userService.CheckUserAsync(username, pass)).Data;
            if (checkUser == false) { 
                ViewData["family-tree"] = "IsNotLogin";
                return View();
            }

            var personFamily = (await _personWithFamilyService.GetAllAsync()).Data;

            var familyTree = new List<PersonFamilyTreeDTO>();
            var itemsColorStyle = "";
            foreach (var person in personFamily)
            {
                if (person.FullName != null && person.Gender != null)
                {
                    var personFamilyTree = new PersonFamilyTreeDTO(person.PersonId, person.FullName, person.Gender.ToLower())
                    {
                        birthDate = person.BirthDate != null ? person.BirthDate.GetValueOrDefault().ToDate() : null,
                        deathDate = person.DeathDate != null ? person.DeathDate.GetValueOrDefault().ToDate() : null,
                        photo = person.Photo,
                        backgroundColor = person.BackgroundColor,
                        description = person.Description,
                        fid = person.FatherId,
                        mid = person.MotherId,
                        pids = person.SpouseIds,
                    };
                    familyTree.Add(personFamilyTree);

                    if (person.BackgroundColor!=null)
                    { itemsColorStyle += $"svg.tommy [data-n-id='{person.PersonId}'] rect{{fill:{person.BackgroundColor}}}"; }
                }
            }

            var setting = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore };
            var result = JsonConvert.SerializeObject(familyTree, setting);
            ViewData["family-tree"] = result;

            ViewData["item-color-style"] = itemsColorStyle;

            return View();
        }
    }
}