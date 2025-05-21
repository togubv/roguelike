namespace Roguelike
{
    public interface IDamagable
    {
        bool IsAlive();
        bool TakeDamage(int damageValue);
    }
}
