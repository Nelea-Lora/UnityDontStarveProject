using _Project.Scripts.Memento.Command;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class GameActionController : MonoBehaviour
    {
        private CommandManager commandManager = new CommandManager();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var move = new MovePlayerCommand(GameFacade.Instance.GetPlayer(), Vector3.left);
                commandManager.ExecuteCommand(move);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var move = new MovePlayerCommand(GameFacade.Instance.GetPlayer(), Vector3.right);
                commandManager.ExecuteCommand(move);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                commandManager.UndoLastCommand();
            }
        }
    }

}