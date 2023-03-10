using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using FamilyTree.Model.PersonWithFamily;
using FamilyTree.Service.PersonWithFamily;

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

        [HttpPost("Photo/{id}")]
        public async Task<IActionResult> UploadPhotoAsync(int id,IFormFile file)
        {
            var result = await _personWithFamilyService.UploadPhotoAsync(id,file);
            return Ok(result);
        }

        [HttpDelete("Photo/{id}")]
        public async Task<IActionResult> DeletePhotoAsync(int id)
        {
            var result = await _personWithFamilyService.DeletePhotoAsync(id);
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

}
