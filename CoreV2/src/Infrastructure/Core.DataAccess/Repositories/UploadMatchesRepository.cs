﻿using Core.DataAccess.Persistence;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Repositories;

public class UploadMatchesRepository(DatabaseContext context) : IUploadMatchesRepository
{
    private readonly DatabaseContext context = context;
    private readonly DbSet<ItemMatch> dbSet = context.Set<ItemMatch>();

    public async Task AddAsync(ItemMatch uploadMatch)
    {
        this.dbSet.Add(uploadMatch);
        await this.context.SaveChangesAsync();
    }
}
