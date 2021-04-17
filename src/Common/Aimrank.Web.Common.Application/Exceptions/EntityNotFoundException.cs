namespace Aimrank.Web.Common.Application.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public override string Code => "entity_not_found";
        
        public EntityNotFoundException() : base("Requested entity does not exist")
        {
        }
    }
}