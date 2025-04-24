namespace _Project.Scripts.Memento.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}