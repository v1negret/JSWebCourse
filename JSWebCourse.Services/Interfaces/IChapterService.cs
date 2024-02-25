using JSWebCourse.Models;
using JSWebCourse.Models.Dto;

namespace JSWebCourse.Services.Interfaces
{
    public interface IChapterService
    {
        
        public Task<IEnumerable<ChapterUnitTitles>?> GetAllNames();
        public Task<IEnumerable<Chapter>> GetAllChapters();
        public Task<GetChapterByIdResult> GetChapterById(int id);
        public Task<ChapterServiceResult> GetChapterByTitle(string title);
        public Task<ServiceResult> RemoveChapterById(int id);
        public Task<ServiceResult> RemoveChapter(Chapter chapter);
        public Task<ServiceResult> AddChapter(AddChapterDto chapter);
        public Task<ServiceResult> UpdateChapter(UpdateChapterDto chapter);
    }
}
