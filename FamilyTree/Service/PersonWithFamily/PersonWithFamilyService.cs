﻿using FamilyTree.Model;
using FamilyTree.Helper;
using FamilyTree.Entity;
using FamilyTree.Model.Mapper;
using FamilyTree.Helper.Extension;
using Microsoft.EntityFrameworkCore;
using FamilyTree.Model.PersonFamily;
using FamilyTree.Model.PersonWithFamily;

namespace FamilyTree.Service.PersonWithFamily
{
    public class PersonWithFamilyService : IPersonWithFamilyService
    {
        private DataContext _context;
        public PersonWithFamilyService(DataContext context) => _context = context;

        public async Task<ServiceResponseDTO> CreateAsync(PersonWithFamilyDTO dto)
        {
            dto.LastName ??= "";
            var model = new Person(dto.FirsrtName, dto.LastName, dto.GenderId);

            if (dto.FatherId > 0 || dto.MotherId > 0)
            {
                model.PersonFamily = new PersonFamily(model.Id)
                {
                    FatherId = dto.FatherId,
                    MotherId = dto.MotherId,
                };
            }
            _context.Person.Add(model);
            await _context.SaveChangesAsync();

            if (dto.SpouseIds != null && dto.SpouseIds.IsEmpty() == false)
            {
                var personSpouse = new PersonSpouse(model.Id, dto.SpouseIds.First());
                _context.PersonSpouse.Add(personSpouse);


                var spousePerson = new PersonSpouse(dto.SpouseIds.First(), model.Id);
                _context.PersonSpouse.Add(spousePerson);
            }

            if (dto.ChildrenIds != null && dto.ChildrenIds.IsEmpty() == false)
            {
                var childId = dto.ChildrenIds.First();
                var childFamily= await _context.PersonFamily.FirstOrDefaultAsync(x => x.PersonId == childId);

                if (childFamily == null)
                {
                    var personFamily=new PersonFamily(dto.ChildrenIds.First());
                    SetParent(personFamily, model);
                    _context.PersonFamily.Add(personFamily);
                }
                else if(childFamily != null)
                {
                    SetParent(childFamily, model);
                    _context.PersonFamily.Update(childFamily);
                }

                await _context.SaveChangesAsync();
            }
            
            return ServiceResponseDTO.CreatedSuccessfully;
        }

        public async Task<ServiceResponseDTO> UpdateAsync(int personId, PersonWithFamilyDTO dto)
        {
            var model = await _context.Person.FindAsync(personId);

            if (model != null)
            {
                dto.LastName ??= "";
                model.FirstName = dto.FirsrtName;
                model.LastName = dto.LastName;
                model.GenderId = dto.GenderId;

                _context.Person.Update(model);
                await _context.SaveChangesAsync();
            }

            return ServiceResponseDTO.UpdatedSuccessfully;
        }

        public async Task<ServiceResponseDTO> DeleteAsync(int id)
        {
            var model = await _context.Person.Include(x=>x.PersonFamily).FirstOrDefaultAsync(x=>x.Id==id);
            if (model == null) return ServiceResponseDTO.UpdatedSuccessfully;

            if (model.PersonFamily != null) _context.PersonFamily.Remove(model.PersonFamily);

            var children = await _context.PersonFamily.Where(x => x.FatherId == id || x.MotherId == id).ToListAsync();
            if (children.IsEmpty() == false)
            {
                foreach (var child in children)
                {
                    if (child.MotherId > 0 && child.FatherId > 0)
                    {
                        if (child.FatherId == id) child.FatherId = null;
                        else if (child.MotherId == id) child.MotherId = null;

                        _context.PersonFamily.Update(child);
                    }

                    else _context.PersonFamily.Remove(child);
                }
            }

            if (model.PersonSpouse.IsEmpty() == false)
            {
                _context.PersonSpouse.RemoveRange(model.PersonSpouse);

                var personSpouses = await _context.PersonSpouse.Where(x => x.SpouseId == id).ToListAsync();
                _context.PersonSpouse.RemoveRange(personSpouses);
            }

            _context.Person.Remove(model);
            await _context.SaveChangesAsync();

            return ServiceResponseDTO.DeletedSuccessfully;
        }

        public async Task<ServiceResponseDTO<List<ListPersonWithFamilyDTO>>> GetAllAsync()
        {
            var result = await _context.Person.Select(PersonWithFamilyMapper.MapList).ToListAsync();
            return new ServiceResponseDTO<List<ListPersonWithFamilyDTO>>(result);
        }

        private static void SetParent(PersonFamily personFamily,Person parent)
        {
            if (parent.GenderId == Model.Enum.GenderEnum.Male) personFamily.FatherId = parent.Id;
            else if (parent.GenderId == Model.Enum.GenderEnum.Female) personFamily.MotherId = parent.Id;
        }
    }
}
