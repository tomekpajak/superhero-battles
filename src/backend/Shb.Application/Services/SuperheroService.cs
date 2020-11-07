using System;
using System.Threading.Tasks;
using Shb.Application.Services.Abstraction;
using Shb.Domain.Repositories;
using Shb.Application.DTOs;
using Shb.Domain.Abstractions;
using Shb.Application.Specifications;
using AutoMapper;
using FluentValidation;
using Shb.Domain.Models;
using System.Collections.Generic;

namespace Shb.Application.Services
{
    internal class SuperheroService : ISuperheroService
    {
        private readonly ISuperheroRepository superheroRepository;
        private readonly IMapper mapper;
        private readonly IAppLogger<SuperheroService> logger;


        public SuperheroService(IAppLogger<SuperheroService> logger,
                                ISuperheroRepository superheroRepository,
                                IMapper mapper)
        {
            this.superheroRepository = superheroRepository ?? throw new ArgumentNullException(nameof(superheroRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SuperheroDTO> CreateAsync(SuperheroDTO model)
        {
            logger.LogTrace($"{nameof(CreateAsync)} {model}", model);

            var entity = this.mapper.Map<Superhero>(model);
            var result = await this.superheroRepository.AddAsync(entity);
            return this.mapper.Map<SuperheroDTO>(result);
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogTrace($"{nameof(DeleteAsync)} {id}", id);

            var entity = await this.superheroRepository.SingleByIdAsync(id);
            await this.superheroRepository.DeleteAsync(entity);
        }

        public async Task UpdateAsync(SuperheroDTO model)
        {
            logger.LogTrace($"{nameof(UpdateAsync)} {model}", model);

            var entity = this.mapper.Map<Superhero>(model);
            await this.superheroRepository.UpdateAsync(entity);
        }

        public async Task<SuperheroDTO> SingleByIdAsync(int id)
        {
            logger.LogTrace($"{nameof(SingleByIdAsync)} {id}", id);

            var entity = await this.superheroRepository.SingleByIdAsync(id);
            return this.mapper.Map<SuperheroDTO>(entity);
        }

        public async Task<IReadOnlyList<SuperheroDTO>> ListPaginatedAsync(int pageNumber, int pageSize)
        {
            logger.LogTrace($"{nameof(ListPaginatedAsync)}");

            var entities = await this.superheroRepository.ListAsync(new SuperheroPaginationSpecification(pageNumber, pageSize));           
            return this.mapper.Map<IReadOnlyList<SuperheroDTO>>(entities);
        }

        public async Task<SuperheroDTO> SingleRandomAsync()
        {        
            logger.LogTrace($"{nameof(SingleRandomAsync)}");

            int count = await this.superheroRepository.CountAllAsync();
            var entity = await this.superheroRepository.SingleBySpecificationAsync(new SuperheroRandomSpecification(count));

            return this.mapper.Map<SuperheroDTO>(entity);
        }

        public async Task<int> CountAllAsync()
        {
            logger.LogTrace($"{nameof(CountAllAsync)}");

            return await this.superheroRepository.CountAllAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            logger.LogTrace($"{nameof(ExistsAsync)} {id}", id);

            return await this.superheroRepository.ExistsAsync(id);
        }
    }
}
