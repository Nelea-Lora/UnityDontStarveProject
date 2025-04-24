using UnityEngine;

namespace _Project.Scripts.Memento
{
    public class GameSaveManager : MonoBehaviour
    {
        private GameCaretaker caretaker = new GameCaretaker();

        public void SaveGame()
        {
            var player = GameFacade.Instance.GetPlayer();
            caretaker.Save(player.SaveState());
            Debug.Log("Game Saved!");
        }

        public void LoadLastSave()
        {
            var memento = caretaker.LoadLast();
            if (memento != null)
            {
                GameFacade.Instance.GetPlayer().RestoreState(memento);
                Debug.Log("Game Loaded!");
            }
            else
            {
                Debug.Log("No save found.");
            }
        }
    }

}