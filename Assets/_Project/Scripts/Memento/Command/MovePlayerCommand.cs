using UnityEngine;

namespace _Project.Scripts.Memento.Command
{
    public class MovePlayerCommand : ICommand
    {
        private PlayerController player;
        private Vector3 delta;
        private GameMemento _state;

        public MovePlayerCommand(PlayerController player, Vector3 delta)
        {
            this.player = player;
            this.delta = delta;
        }

        public void Execute()
        {
            // Сохраняем состояние перед действием
            _state = player.SaveState();
        
            // Действие
            player.transform.position += delta;
        }

        public void Undo()
        {
            // Откат действия
            player.RestoreState(_state);
        }
    }

}