namespace Portfoliosis.Core.Entities;

public class EntityBase
{
    
    protected EntityBase(Guid id) => Id = id;

    protected EntityBase()
    {
    }

    public Guid Id { get; private init; }

    public static bool operator ==(EntityBase? first, EntityBase? second) =>
        first is not null && second is not null && first.Equals(second);

    public static bool operator !=(EntityBase? first, EntityBase? second) =>
        !(first == second);

    public bool Equals(EntityBase? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not EntityBase entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}
