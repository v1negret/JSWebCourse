using JSWebCourse.Models;
using JSWebCourse.Models.Dto;

namespace JSWebCourse.Services.Interfaces
{
    public interface IUnitService
    {
        public Task<GetAllUnitsResult> GetAllWithDetails();
        public Task<IEnumerable<Unit>> GetUnitsByChapter(int chapterId);
        public Task<UnitServiceResult> GetUnitById(int id);
        public Task<UnitServiceResult> GetUnitByTitle(string title);
        public Task<ServiceResult> UpdateUnit(UpdateUnitDto unit);
        public Task<ServiceResult> RemoveUnit(AddUnitDto unit);
        public Task<ServiceResult> RemoveUnitById(int unitId);
        public Task<AddUnitResult> AddUnitToChapter(AddUnitDto unit);

    }
}
