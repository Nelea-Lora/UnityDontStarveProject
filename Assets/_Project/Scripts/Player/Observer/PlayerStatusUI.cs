namespace _Project.Scripts.Player.Observer
{
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerStatusUI : MonoBehaviour, IHealthObserver
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image hungerBar;
        [SerializeField] private Image mindBar;
        [SerializeField] private HealthSystem healthSystem;

        void Start()
        {
            if (healthSystem != null)
                healthSystem.Attach(this); // Подписываемся на изменения
        }

        public void OnHealthChanged(float health, float hunger, float mind)
        {
            healthBar.fillAmount = health;
            hungerBar.fillAmount = hunger;
            mindBar.fillAmount = mind;
        }

        private void OnDestroy()
        {
            if (healthSystem != null)
                healthSystem.Detach(this); // Отписываемся при удалении объекта
        }
    }

}