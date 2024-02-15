using JSWebCourse.Checks.Interfaces;
using JSWebCourse.Data;
using JSWebCourse.Models;
using JSWebCourse.Models.Dto;
using JSWebCourse.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JSWebCourse.Services
{
    public class ChapterService : IChapterService
    {
        private readonly ApplicationDbContext _db;
        public ChapterService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ChapterUnitTitles>> GetAllNames()
        {
            try
            {
                var chapters = await _db.Chapters.Include(c => c.Units).ToListAsync();

                List<ChapterUnitTitles> result = new List<ChapterUnitTitles>();
                foreach(var chapter in chapters)
                {
                    List<string> titles = new List<string>();
                    foreach(var title in chapter.Units)
                    {
                        titles.Add(title.Title);
                    }
                    result.Add(new ChapterUnitTitles { ChapterId = chapter.ChapterId, Title = chapter.Title, UnitsNames = titles});
                }
                return result;
                
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public async Task<ServiceResult> AddChapter(AddChapterDto chapterDto)
        {
            try
            {
                if(chapterDto == null)
                {
                    return ServiceResult.BadRequest;
                }

                var chapter = new Chapter()
                {
                    Title = chapterDto.Title,
                    Description = chapterDto.Description,
                };

                await _db.Chapters.AddAsync(chapter);
                await _db.SaveChangesAsync();

                return ServiceResult.Success;

            }
            catch (Exception ex)
            {
                return ServiceResult.ServerError;
            }
        }

        public async Task<IEnumerable<Chapter>> GetAllChapters()
        {
            try
            {
                var result = await _db.Chapters.Include(x => x.Units).ToListAsync();
                if(result.Count == 0)
                {
                    return null;
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GetChapterByIdResult> GetChapterById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new GetChapterByIdResult()
                    {
                        ServiceResult = ServiceResult.BadRequest,
                        Chapter = null
                    };                        
                }

                var chapter = await _db.Chapters
                    .Include(c => c.Units)
                    .FirstOrDefaultAsync(c => c.ChapterId == id);

                var result = new GetChapterDto()
                {
                    ChapterId = chapter.ChapterId,
                    Title = chapter.Title,
                    Description = chapter.Description,
                    Units = new List<ForGetChapterUnitDto>()
                };

                foreach (var unit in chapter.Units)
                {
                    result.Units.Add(new ForGetChapterUnitDto() { Id = unit.UnitId, Title = unit.Title });
                }

                return new GetChapterByIdResult()
                {
                    Chapter = result,
                    ServiceResult = ServiceResult.Success

                };
            }
            catch (Exception ex)
            {
                return new GetChapterByIdResult()
                {
                    ServiceResult = ServiceResult.BadRequest,
                    Chapter = null
                };
            }
        }

        public async Task<ChapterServiceResult> GetChapterByTitle(string title)
        {
            try
            {
                if (String.IsNullOrEmpty(title))
                {
                    return new ChapterServiceResult()
                    {
                        ServiceResult = ServiceResult.BadRequest,
                        Chapter = null
                    };
                }

                var result = await _db.Chapters
                    .Include(c => c.Units)
                    .FirstOrDefaultAsync(c => c.Title.Equals(title));

                return new ChapterServiceResult()
                {
                    Chapter = result,
                    ServiceResult = ServiceResult.Success

                };
            }
            catch (Exception ex)
            {
                return new ChapterServiceResult()
                {
                    ServiceResult = ServiceResult.BadRequest,
                    Chapter = null
                };
            }
        }

        public async Task<ServiceResult> RemoveChapterById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return ServiceResult.BadRequest;
                }
                var chapter = await _db.Chapters.FirstAsync(c => c.ChapterId == id);
                _db.Chapters.Remove(chapter);

                await _db.SaveChangesAsync();
                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                return ServiceResult.ServerError;
            }
        }

        public async Task<ServiceResult> RemoveChapter(Chapter chapter)
        {
            try
            {
                if (chapter == null)
                {
                    return ServiceResult.BadRequest;
                }

                _db.Chapters.Remove(chapter);
                await _db.SaveChangesAsync();

                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                return ServiceResult.ServerError;
            }
        }

        public async Task<ServiceResult> UpdateChapter(UpdateChapterDto chapterDto)
        {
            try
            {
                if (chapterDto == null)
                {
                    return ServiceResult.BadRequest;
                }

                var chapter = new Chapter()
                {
                    ChapterId = chapterDto.ChapterId,
                    Title = chapterDto.Title,
                    Description = chapterDto.Description
                };

                await _db.Chapters.AddAsync(chapter);
                await _db.SaveChangesAsync();

                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                return ServiceResult.ServerError;
            }
        }
    }
}
