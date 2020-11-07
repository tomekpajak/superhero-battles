using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shb.Application.DTOs;
using Shb.Domain.Specifications.Abstraction;

namespace Shb.Application.Services.Abstraction
{
    public interface ISuperheroService
    {
        Task<SuperheroDTO> CreateAsync(SuperheroDTO model);
        Task UpdateAsync(SuperheroDTO model);
        Task DeleteAsync(int id);        
        Task<SuperheroDTO> SingleRandomAsync();        
        Task<SuperheroDTO> SingleByIdAsync(int id);
        Task<IReadOnlyList<SuperheroDTO>> ListPaginatedAsync(int pageNumber, int pageSize);
        Task<int> CountAllAsync();
        Task<bool> ExistsAsync(int id);        
    }
}





