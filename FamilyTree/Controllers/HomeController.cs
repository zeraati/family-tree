using Newtonsoft.Json;
using FamilyTree.Model;
using FamilyTree.Service.User;
using Microsoft.AspNetCore.Mvc;
using FamilyTree.Helper.Extension;
using FamilyTree.Service.PersonWithFamily;
using Serilog;

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
                ViewData["item-color-style"] = "body{font-family: sans-serif;color: #4A4A4A;display: flex;justify-content: center;align-items: center;min-height: 100vh;overflow: hidden;margin: 0;padding: 0}.form-div{width: 350px;position: relative}.form-div .form-field::before{font-size: 20px;position: absolute;left: 15px;top: 17px;color: #888888;content: \" \";display: block;background-size: cover;background-repeat: no-repeat}.form-div .form-field:nth-child(1)::before{background-image: url(/assets/img/user-icon.png);width: 20px;height: 20px;top: 15px}.form-div .form-field:nth-child(2)::before{background-image: url(/assets/img/lock-icon.png);width: 16px;height: 16px}.form-div .form-field{display: -webkit-box;display: -ms-flexbox;display: flex;-webkit-box-pack: justify;-ms-flex-pack: justify;justify-content: space-between;-webkit-box-align: center;-ms-flex-align: center;align-items: center;margin-bottom: 1rem;position: relative}.form-div input{font-family: inherit;width: 100%;outline: none;background-color: #fff;border-radius: 4px;border: none;display: block;padding: 0.9rem 0.7rem;box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.16);font-size: 17px;color: #4A4A4A;text-indent: 40px}.form-div .btn{outline: none;border: none;cursor: pointer;display: inline-block;margin: 0 auto;padding: 0.9rem 2.5rem;text-align: center;background-color: #47AB11;color: #fff;border-radius: 4px;box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.16);font-size: 17px}#login-message{margin:auto;color:red}";
                ViewData["family-tree"] = "-1";
                ViewData["login-message"] = username.IsEmpty()==false || pass.IsEmpty()==false?"User name or password is wrong!":"";
                
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