namespace _Project.Scripts.MenuUI.State
{
    public interface IGameState
    {
        void EnterState();
        void ExitState();
        void HandleInput();
    }

}