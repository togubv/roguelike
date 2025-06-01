namespace Roguelike
{
    public interface IPoolable
    {
        string GetObjectId { get; }

        void Init(MobFactory mobFactory, string objectId);
        void Refresh();
        void Despawn();
    }
}
