﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.DDD;

namespace Shared.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) 
        : SaveChangesInterceptor
    {
        public override int SavedChanges(
            SaveChangesCompletedEventData eventData, 
            int result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, 
            int result, 
            CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context, cancellationToken);
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvents(DbContext? context, 
            CancellationToken cancellationToken = default)
        {
            if (context == null) return;

            var aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(a => a.Entity.DomainEvents.Any())
                .Select(a => a.Entity);

            var domainEvents = aggregates
                .SelectMany(a => a.DomainEvents)
                .ToList();

            aggregates.ToList().ForEach(a => a.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
