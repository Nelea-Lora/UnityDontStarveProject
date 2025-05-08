using UnityEngine;

namespace _Project.Scripts.MenuUI
{
    public class SettingsMenu : MonoBehaviour
    {
        public GameObject startScreen;
        public GameObject settingsScreen;
        
        private void Awake()
        {
            startScreen.SetActive(true);
            settingsScreen.SetActive(false);
        }
        
        public void ExitEditSettings()
        {
            startScreen.SetActive(true);
            settingsScreen.SetActive(false);
        }
        public void EditSettings()
        {
            startScreen.SetActive(false);
            settingsScreen.SetActive(true);
        }
    }
}