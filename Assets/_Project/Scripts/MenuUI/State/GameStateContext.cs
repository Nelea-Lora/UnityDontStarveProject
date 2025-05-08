using UnityEngine;

namespace _Project.Scripts.MenuUI.State
{
    public class GameStateContext : MonoBehaviour
    {
        private IGameState _currentState;

        public void SetState(IGameState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
        void Start()
        {
            SetState(new GamePlayingState(this));
        }

        void Update()
        {
            _currentState?.HandleInput();
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