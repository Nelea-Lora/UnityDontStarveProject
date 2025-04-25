namespace _Project.Scripts.Player.Observer
{
    public interface IHealthObserver
    {
        void OnHealthChanged(float health, float hunger, float mind);
    }
}