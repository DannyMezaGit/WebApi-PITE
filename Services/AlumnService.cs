using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public class AlumnService: IAlumnService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public AlumnService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<AlumnDTO>> GetAllAlumnsAsync()
        {
            var list = await _dbContext.Alumns.ToListAsync();

            return _mapper.Map<ICollection<AlumnDTO>>(list);
        }

        public async Task<AlumnDTO> GetAlumnByIdAsync(long id)
        {
            var alumn = await _dbContext.Alumns.Where(x => x.AlumnId == id).FirstOrDefaultAsync();

            return _mapper.Map<AlumnDTO>(alumn);
        }

        public async Task<AlumnDTO> AddAlumnAsync(AlumnDTO newAlumn)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var alumn = _mapper.Map<Alumn>(newAlumn);
                bool alumnExist = await _dbContext.Alumns.AnyAsync(x => x == alumn);

                if (alumnExist)
                {
                    throw new Exception("El Alumno ya existe");
                }

                _dbContext.Add(alumn);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                newAlumn = null;
                return _mapper.Map<AlumnDTO>(alumn);

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al intentar guardar un alumno nuevo", ex);
            }
        }

        public async Task<AlumnDTO> UpdateAlumnAsync(AlumnDTO alumnUpdated)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {

                var alumn = await _dbContext.Alumns.Where(x => x.AlumnId == alumnUpdated.AlumnId).AsNoTracking().FirstOrDefaultAsync();
                    alumn = _mapper.Map<Alumn>(alumnUpdated);
                
                _dbContext.Entry(alumn).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<AlumnDTO>(alumn);

            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                throw new Exception("Error al intentar editar un alumno", ex);
            }
        }

        public async Task<bool> DeleteAlumnAsync(long id)
        {
             var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var alumn = await _dbContext.Alumns.Where(x => x.AlumnId == id).FirstOrDefaultAsync();
                if (alumn == null)
                {
                    throw new Exception("El Alumno no existe");
                }
                _dbContext.Alumns.Remove(alumn);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;

            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                throw new Exception("Error al intentar eliminar un alumno", ex);
            }
        }

    }
}