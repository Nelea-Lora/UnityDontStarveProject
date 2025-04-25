namespace _Project.Scripts.Player.Observer
{
    public interface IHealthSubject
    {
        void Attach(IHealthObserver observer);
        void Detach(IHealthObserver observer);
        void NotifyObservers();
    }
}