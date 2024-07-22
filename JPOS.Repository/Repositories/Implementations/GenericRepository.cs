﻿using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public readonly JPOS_ProjectContext _context;
    public static GenericRepository<T> _instance;

    public GenericRepository()
    {
        _context = new JPOS_ProjectContext();
    }

    public static GenericRepository<T> Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GenericRepository<T>();
            }
            return _instance;
        }
    }


    public async Task<List<T>> GetAllAsync()
    {
        try
        {
            return await _context.Set<T>().ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<T> GetByIdAsync(object id)
    {
        return await _context.FindAsync<T>(id);
    }

    public async Task<bool> InsertAsync(T entity)
    {
        await _context.AddAsync<T>(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            _context.Update<T>(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            var entityID = await GetById(id);

            if (entityID != null)
            {
                _context.Remove<T>(entityID);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                Console.WriteLine("Not found for deletion");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while deleting: " + ex.Message);
            return false;
        }
    }
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entityID = await GetById(id);

            if (entityID != null)
            {
                _context.Remove<T>(entityID);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                Console.WriteLine("Not found for deletion");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while deleting: " + ex.Message);
            return false;
        }
    }

    public async Task<T> GetById(string id)
    {
        return await _context.FindAsync<T>(id);
    }

    public async Task<T> GetById(int id)
    {
        return await _context.FindAsync<T>(id);
    }

    public void Detach(T entity)
    {
        var entry = _context.Entry(entity);
        if (entry != null)
        {
            entry.State = EntityState.Detached;
        }
    }
}
