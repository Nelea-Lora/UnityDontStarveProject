using UnityEngine;

namespace _Project.Scripts.Memento
{
    public class GameSaveManager : MonoBehaviour
    {
        private GameCaretaker _caretaker = new GameCaretaker();

        public void SaveGame()
        {
            var player = GameFacade.Instance.GetPlayer();
            _caretaker.Save(player.SaveState());
            Debug.Log("Game Saved!");
        }

        public void LoadLastSave()
        {
            var memento = _caretaker.LoadLast();
            var player = GameFacade.Instance.GetPlayer();
            if (memento != null)
            {
                player.RestoreState(memento);
                Debug.Log("Game Loaded!");
            }
            else
            {
                Debug.Log("No save found.");
            }
        }
    }

}