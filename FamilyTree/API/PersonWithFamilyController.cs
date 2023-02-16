using Microsoft.AspNetCore.Mvc;
using FamilyTree.Service.PersonWithFamily;
using FamilyTree.Model.PersonWithFamily;
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

        [HttpPost,Route("api/OrgChartJS")]
        public async Task<IActionResult> OrgChartJS(JObject obj)
        {
            var root = obj["n"][0]["p"][0];
            var p = obj["n"][0]["p"];
            var result = new {root = new {p=p}};
            return Ok(result);
        }
    }
}
