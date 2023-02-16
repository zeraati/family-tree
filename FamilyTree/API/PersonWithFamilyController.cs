using Microsoft.AspNetCore.Mvc;
using FamilyTree.Service.PersonWithFamily;
using FamilyTree.Model.PersonWithFamily;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace FamilyTree.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonWithFamilyController : ControllerBase
    {
        private readonly IPersonWithFamilyService _personWithFamilyService;
        public PersonWithFamilyController(IPersonWithFamilyService personWithFamilyService) => _personWithFamilyService = personWithFamilyService;

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PersonWithFamilyDTO dto)
        {
            var result=await _personWithFamilyService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, PersonWithFamilyDTO dto)
        {
            var result = await _personWithFamilyService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _personWithFamilyService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UploadPhotoAsync(int id,IFormFile file)
        {
            var result = await _personWithFamilyService.UploadPhotoAsync(id,file);
            return Ok(result);
        }

        [HttpPost("/api/OrgChartJS")]
        public async Task<IActionResult> OrgChartJS([FromBody] object param)
        {
            dynamic result = new {};

            var obj = JObject.Parse(param.ToString());
            var root = obj["r"].FirstOrDefault();
            
            if(root != null)
            {
                obj = JObject.Parse(param.ToString().Replace("null", "0").Replace($"\"{root}\",",""));
                var p = obj["n"][0]["p"];
                result = JObject.Parse($"{{\"{root}\":{{}}}}");
                result[root] = new JObject()["p"]=p;
            }
            
            return Ok(result);
        }
    }

    public class x {
        public List<node> n { get; set; }//نودها
        public int c { get; set; }
        public List<string> r { get; set; }//روت اصلی
        public string v { get; set; }//ورژن
    }

    public class node
    {
        public List<string?> p { get; set; }//پرنت - آید پرنت +‌مختصات - اولین رکورد آید است
        public List<string> c { get; set; }// چایلدها
        public List<int> q { get; set; }
        public int g { get; set; }
        public int e { get; set; }
        public int i { get; set; }
    }
}
