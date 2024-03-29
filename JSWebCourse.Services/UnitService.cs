﻿using JSWebCourse.Checks.Interfaces;
using JSWebCourse.Data;
using JSWebCourse.Models;
using JSWebCourse.Models.Dto;
using JSWebCourse.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace JSWebCourse.Services
{
    public class UnitService : IUnitService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHtmlValidator _htmlValidator;
        public UnitService(ApplicationDbContext db, IHtmlValidator htmlValidator)
        {
            _db = db;       
            _htmlValidator = htmlValidator;
        }

        public async Task<GetAllUnitsResult> GetAllWithDetails()
        {
            try
            {
                var units = await _db.Units.ToListAsync();
                return new GetAllUnitsResult()
                {
                    Units = units,
                    Result = ServiceResult.Success
                };
            }
            catch (Exception ex)
            {
                return new GetAllUnitsResult()
                {
                    Units = null,
                    Result = ServiceResult.ServerError
                };
            }
        }

        public async Task<IEnumerable<Unit>> GetUnitsByChapter(int chapterId)
        {
            try
            {
                var result = await _db.Units.Where(u => u.ChapterId == chapterId).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AddUnitResult> AddUnitToChapter(AddUnitDto unitDto)
        {
            try
            {
                if(string.IsNullOrEmpty(unitDto.Title) || string.IsNullOrEmpty(unitDto.Description) || string.IsNullOrEmpty(unitDto.HtmlString))
                {
                    return new AddUnitResult() { Result = AddUnitServiceResult.BadRequest };
                }

                var htmlValidate = _htmlValidator.Validate(unitDto.HtmlString);

                if(htmlValidate.Result == false)
                {
                    return new AddUnitResult() { Result = AddUnitServiceResult.HtmlNotValid, Errors = htmlValidate.Errors};
                }

                var unit = new Unit()
                {
                    Title = unitDto.Title,
                    Description = unitDto.Description,
                    ChapterId = unitDto.ChapterId,
                    HtmlCode = unitDto.HtmlString
                };

                await _db.Units.AddAsync(unit);
                await _db.SaveChangesAsync();

                return new AddUnitResult() { Result = AddUnitServiceResult.Success };
            }
            catch(Exception ex)
            {
                return new AddUnitResult() { Result = AddUnitServiceResult.ServerError };
            }
        }

        public async Task<UnitServiceResult> GetUnitById(int id)
        {
            try
            {
                if(id == 0) 
                {
                    return new UnitServiceResult() 
                    {
                        Unit = null,
                        ServiceResult = ServiceResult.BadRequest
                    };
                }

                var result = await _db.Units.FirstAsync(u => u.UnitId == id);
                return new UnitServiceResult()
                {
                    Unit = result,
                    ServiceResult = ServiceResult.Success
                };
            }
            catch(Exception ex)
            {
                return new UnitServiceResult()
                {
                    Unit = null,
                    ServiceResult = ServiceResult.ServerError
                };
            }
        }

        public async Task<UnitServiceResult> GetUnitByTitle(string title)
        {
            try
            {
                if (String.IsNullOrEmpty(title))
                {
                    return new UnitServiceResult()
                    {
                        Unit = null,
                        ServiceResult = ServiceResult.BadRequest
                    };
                }

                var result = await _db.Units.FirstAsync(u => u.Title == title);
                return new UnitServiceResult()
                {
                    Unit = result,
                    ServiceResult = ServiceResult.Success
                };
            }
            catch (Exception ex)
            {
                var exData = JsonSerializer.Serialize(ex.Data);
                return new UnitServiceResult()
                {
                    Unit = null,
                    ServiceResult = ServiceResult.ServerError
                };
            }
        }

        public async Task<ServiceResult> RemoveUnit(AddUnitDto unitDto)
        {
            try
            {
                if(unitDto == null)
                {
                    return ServiceResult.BadRequest;
                }
                var unit = new Unit()
                {
                    Title = unitDto.Title,
                    Description = unitDto.Description,
                    HtmlCode = unitDto.HtmlString,
                };
                _db.Units.Remove(unit);
                await _db.SaveChangesAsync();

                return ServiceResult.Success;
            }
            catch(Exception ex)
            {

                return ServiceResult.ServerError;
            }
        }

        public async Task<ServiceResult> RemoveUnitById(int unitId)
        {
            try
            {

                var unit = await _db.Units.FirstOrDefaultAsync(u =>
                    u.UnitId == unitId);
                if (unit == null)
                {
                    return ServiceResult.BadRequest;
                }

                _db.Units.Remove(unit);
                await _db.SaveChangesAsync();

                return ServiceResult.Success;
            }
            catch(Exception ex)
            {
                return ServiceResult.ServerError;
            }
        }

        public async Task<ServiceResult> UpdateUnit(UpdateUnitDto unitDto)
        {
            try
            {
                if(String.IsNullOrEmpty(unitDto.Title) || String.IsNullOrEmpty(unitDto.Description)) 
                {
                    return ServiceResult.BadRequest;
                }
                var unit = new Unit()
                {
                    ChapterId = unitDto.ChapterId,
                    UnitId = unitDto.UnitId,
                    Title = unitDto.Title,
                    Description = unitDto.Description,
                    HtmlCode = unitDto.HtmlCode
                };
                _db.Units.Update(unit);
                await _db.SaveChangesAsync();

                return ServiceResult.Success;
            }
            catch(Exception ex)
            {
                return ServiceResult.ServerError;
            }
        }
    }
}
