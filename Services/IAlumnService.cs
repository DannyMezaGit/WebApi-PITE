using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IAlumnService
    {
        Task<ICollection<AlumnDTO>> GetAllAlumnsAsync();
        Task<AlumnDTO> GetAlumnByIdAsync(long id);
        Task<AlumnDTO> AddAlumnAsync(AlumnDTO newAlumn);
        Task<AlumnDTO> UpdateAlumnAsync(AlumnDTO alumnUpdated);
        Task<bool> DeleteAlumnAsync(long id);

    }
}