using Aimrank.Common.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;

namespace Aimrank.Common.Infrastructure
{
    public class EntityIdValueConverterSelector : ValueConverterSelector
    {
        private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo>
            _converters = new();
        
        public EntityIdValueConverterSelector(ValueConverterSelectorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
        {
            var baseConverters = base.Select(modelClrType, providerClrType);
            foreach (var converter in baseConverters)
            {
                yield return converter;
            }

            var underlyingModelType = UnwrapNullableType(modelClrType);
            var underlyingProviderType = UnwrapNullableType(providerClrType);

            if (underlyingProviderType is null || underlyingProviderType == typeof(Guid))
            {
                var isEntityIdValue = typeof(EntityId).IsAssignableFrom(underlyingModelType);
                if (isEntityIdValue)
                {
                    var converterType = typeof(EntityIdValueConverter<>).MakeGenericType(underlyingModelType);
                    
                    yield return _converters.GetOrAdd((underlyingModelType, typeof(Guid)), _ =>
                    {
                        return new ValueConverterInfo(
                            modelClrType: modelClrType,
                            providerClrType: typeof(Guid),
                            factory: valueConverterInfo =>
                                (ValueConverter) Activator.CreateInstance(converterType,
                                    valueConverterInfo.MappingHints));
                    });
                }
            }
        }

        private static Type UnwrapNullableType(Type type)
            => type is null ? null : Nullable.GetUnderlyingType(type) ?? type;
    }
}