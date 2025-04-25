using UnityEngine;

namespace _Project.Scripts.MenuUI.State
{
    public class GameStateContext : MonoBehaviour
    {
        private IGameState currentState;

        public void SetState(IGameState newState)
        {
            currentState?.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
        void Start()
        {
            SetState(new GamePlayingState(this));
        }

        void Update()
        {
            currentState?.HandleInput();
        }
        
        public void PauseGameFromUI()
        {
            SetState(new GamePausedState(this));
        }

        public void ResumeGameFromUI()
        {
            SetState(new GamePlayingState(this));
        }

    }

}