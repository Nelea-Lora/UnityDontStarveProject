using UnityEngine;

namespace _Project.Scripts.MenuUI.State
{
    public class GamePausedState : IGameState
    {
        private GameStateContext context;

        public GamePausedState(GameStateContext ctx)
        {
            context = ctx;
        }

        public void EnterState()
        {
            Time.timeScale = 0f;
            PauseMenu.Instance.pauseScreen.SetActive(true);
            PauseMenu.Instance.gameScreen.SetActive(false);
        }

        public void ExitState()
        {
            Time.timeScale = 1f;
            PauseMenu.Instance.pauseScreen.SetActive(false);
            PauseMenu.Instance.gameScreen.SetActive(true);
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                context.SetState(new GamePlayingState(context));
            }
        }
    }
    
}