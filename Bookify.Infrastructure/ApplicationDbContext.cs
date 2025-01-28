﻿using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await PublishDomainEventsAsync();
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occured. ",ex);
        }

    }
    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(e => e.Entity)
            .SelectMany(entity => {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;


            }).ToList();
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
