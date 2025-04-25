using UnityEngine;

namespace _Project.Scripts.MenuUI.State
{
    public class CraftingState : IGameState
    {
        private GameStateContext _context;

        public CraftingState(GameStateContext ctx)
        {
            _context = ctx;
        }

        public void EnterState()
        {
            Debug.Log("Crafting Started");
            Time.timeScale = 0f;
        }

        public void ExitState()
        {
            Debug.Log("Exiting Craft");
            Time.timeScale = 1f;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _context.SetState(new GamePlayingState(_context));
            }
        }
    }

}