using System;

namespace Aimrank.Common.Domain
{
    public class EntityIdMustBeValidRule : IBusinessRule
    {
        private readonly Guid _value;

        public EntityIdMustBeValidRule(Guid value)
        {
            _value = value;
        }

        public string Message => "Invalid entity id";
        public string Code => "invalid_entity_id";

        public bool IsBroken() => _value == Guid.Empty;
    }
}