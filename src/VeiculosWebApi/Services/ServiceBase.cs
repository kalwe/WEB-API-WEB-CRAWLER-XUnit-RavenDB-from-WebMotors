﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculosWebApi.Interfaces.Repositories;
using VeiculosWebApi.Interfaces.Services;

namespace VeiculosWebApi.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity>, ISwitchActiveStatusService<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;
        private readonly ISwitchActiveStatusService<TEntity> _switchActiveStatus;

        IList<TEntity> Entities;

        // Default Constructor
        public ServiceBase(IRepositoryBase<TEntity> repository,
                    ISwitchActiveStatusService<TEntity> switchActiveStatus)
        {
            _repository = repository;
            _switchActiveStatus = switchActiveStatus;

            Entities = new List<TEntity>();
        }

        public TEntity SetActiveStatusFalse(TEntity entity)
        {
            _switchActiveStatus.SetActiveStatusFalse(entity);
            return entity;
        }

        public TEntity SetActiveStatusTrue(TEntity entity)
        {
            _switchActiveStatus.SetActiveStatusTrue(entity);
            return entity;
        }

        // Add entity for commit
        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        // Execute the commit in database
        public async Task CommitAsync()
        {
            if (Entities.Count > 0)
            {
                do
                {
                    try
                    {
                        await _repository.AddOrUpdateAsync(Entities.FirstOrDefault());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Service error: method Commit() try execute repository AddOrUpdateAsync()");
                        // return ServiceResult();
                    }

                    Entities.Remove(Entities.FirstOrDefault());

                } while (Entities.Count > 0);
            }
        }

        public async Task AddUpdateAsync(TEntity entity)
        {
            await _repository.AddOrUpdateAsync(entity);
        }

        // Find entity by id async
        public async Task<TEntity> FindAsync(string id)
        {
            return await _repository.FindAsync(id);
        }

        // List
        public async Task<IEnumerable<TEntity>> ListAsync(int size)
        {
            return await _repository.ListAsync(size);
        }

        // ListAll
        public async Task<IEnumerable<TEntity>> ListAllAsync()
        {
            return await _repository.ListAllAsync();
        }

        // Delete
        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}