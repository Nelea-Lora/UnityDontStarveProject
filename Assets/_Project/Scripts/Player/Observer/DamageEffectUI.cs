namespace _Project.Scripts.Player.Observer
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class DamageEffectUI : MonoBehaviour, IHealthObserver
    {
        [SerializeField] private Image damageImage;
        [SerializeField] private HealthSystem healthSystem;

        public Color flashColor = new Color(1, 0, 0, 0.5f); // красный с полупрозрачностью
        public float flashDuration = 0.5f; // длительность вспышки

        private Coroutine flashCoroutine;
        private float lastHealth;

        void Start()
        {
            if (healthSystem != null)
                healthSystem.Attach(this);

            if (damageImage != null)
                damageImage.color = new Color(1, 0, 0, 0); // полностью прозрачный старт

            lastHealth = healthSystem.GetCurrentHealth();
        }

        public void OnHealthChanged(float health, float hunger, float mind)
        {
            if (health < lastHealth) // Урон только если здоровье уменьшилось
            {
                if (flashCoroutine != null)
                    StopCoroutine(flashCoroutine);

                flashCoroutine = StartCoroutine(FlashDamageEffect());
            }

            lastHealth = health; // Обновляем для следующей проверки
        }


        private IEnumerator FlashDamageEffect()
        {
            damageImage.color = flashColor;

            float elapsedTime = 0f;
            Color startColor = flashColor;
            Color endColor = new Color(flashColor.r, flashColor.g, flashColor.b, 0);

            while (elapsedTime < flashDuration)
            {
                damageImage.color = Color.Lerp(startColor, endColor, elapsedTime / flashDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            damageImage.color = endColor;
        }

        private void OnDestroy()
        {
            if (healthSystem != null)
                healthSystem.Detach(this);
        }
    }

}