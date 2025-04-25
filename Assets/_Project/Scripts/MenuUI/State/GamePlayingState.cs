using UnityEngine;

namespace _Project.Scripts.MenuUI.State
{
    public class GamePlayingState : IGameState
    {
        private GameStateContext _context;

        public GamePlayingState(GameStateContext ctx)
        {
            _context = ctx;
        }

        public void EnterState()
        {
            Debug.Log("Entered Game Playing State");
            Time.timeScale = 1f;
            // Покажи UI игры, скрой меню
        }

        public void ExitState()
        {
            Debug.Log("Exiting Game Playing State");
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _context.SetState(new GamePausedState(_context));
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                _context.SetState(new CraftingState(_context));
            }
        }
    }

}