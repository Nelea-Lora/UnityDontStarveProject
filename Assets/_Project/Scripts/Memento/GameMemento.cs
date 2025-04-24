namespace _Project.Scripts.Memento
{
    public class GameMemento
    {
        public GameState State { get; private set; }

        public GameMemento(GameState state)
        {
            State = state;
        }
    }

}