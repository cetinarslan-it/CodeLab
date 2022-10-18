using System;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class InsuranceRepository : IRepository<Insurance>, IDisposable
{
    private InsuranceDbContext context;

    public InsuranceRepository(InsuranceDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Insurance> GetAll()
    {
        return context.Insurances.ToList();
    }

    public Insurance? GetById(int insuranceId)
    {
        return context.Insurances.Find(insuranceId);
    }

    public void Insert(Insurance insurance)
    {
        context.Insurances.Add(insurance);
    }

    public void Delete(int insuranceId)
    {
        Insurance? insurance = context.Insurances.Find(insuranceId);
        if (insurance != null)
            context.Insurances.Remove(insurance);
    }

    public void Update(Insurance insurance)
    {
        context.Entry(insurance).State = EntityState.Modified;
    }

    public void Save()
    {
        context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}