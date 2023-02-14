using Microsoft.AspNetCore.Mvc;
using FamilyTree.Service.PersonWithFamily;
using FamilyTree.Model.PersonWithFamily;

namespace FamilyTree.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonWithFamilyController : ControllerBase
    {
        private readonly IPersonWithFamilyService _personWithFamilyService;
        public PersonWithFamilyController(IPersonWithFamilyService personWithFamilyService) => _personWithFamilyService = personWithFamilyService;

        [HttpPost]
        public async Task<IActionResult> Create(PersonWithFamilyDTO dto)
        {
            var result=await _personWithFamilyService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonWithFamilyDTO dto)
        {
            var result = await _personWithFamilyService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personWithFamilyService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
