using Aimrank.Web.Common.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Aimrank.Web.Common.Infrastructure
{
    public class EntityIdValueConverter<TEntityId> : ValueConverter<TEntityId, Guid>
        where TEntityId : EntityId
    {
        public EntityIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value, value => Create(value), mappingHints)
        {
        }

        private static TEntityId Create(Guid id) => Activator.CreateInstance(typeof(TEntityId), id) as TEntityId;
    }
}