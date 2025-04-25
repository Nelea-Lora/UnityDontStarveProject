using _Project.Scripts.Memento.Command;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class GameActionController : MonoBehaviour
    {
        private CommandManager commandManager = new CommandManager();
        private PlayerController player;

        void Start()
        {
            player = GameFacade.Instance.GetPlayer();
        }

        void Update()
        {
            Vector3 delta = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) delta = Vector3.up;
            if (Input.GetKey(KeyCode.S)) delta = Vector3.down;
            if (Input.GetKey(KeyCode.A)) delta = Vector3.left;
            if (Input.GetKey(KeyCode.D)) delta = Vector3.right;

            if (delta != Vector3.zero)
            {
                var moveCommand = new MovePlayerCommand(player, delta);
                commandManager.ExecuteCommand(moveCommand);
            }

            // Откат действия
            if (Input.GetKeyDown(KeyCode.Z))
            {
                commandManager.UndoLastCommand();
            }
        }
    }


}