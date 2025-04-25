using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance { get; private set; }

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HealthSystem healthSystem;

    [Header("Systems")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private DayTime dayTime;
    [SerializeField] private ObjectLifeCycles objectLifeCycles;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public PlayerController GetPlayer() => playerController;
    public void TakeDamage(float amount) => healthSystem.TakeDamage(amount);
    public void Eat(float value) => healthSystem.Eat(value);
    public void Heal(float value) => healthSystem.Heal(value);
    public float GetCurrentHealth() => healthSystem.GetCurrentHealth();
    public float GetCurrentHunger() => healthSystem.GetCurrentHealth();
    public float GetCurrentMind() => healthSystem.GetCurrentHunger();
    public void SetAll(float health, float hunger, float mind) => healthSystem.SetAll(health, hunger, mind);

    public void AddItem(ItemScriptableObject item, int count) =>
        inventoryManager.AddItem(item, count);
    
    public void TryTakeItemInHands(ItemScriptableObject item, int slotID, int previousSlotID)
    {
        if (item == null) return;
    
        if (playerController.itemInHands != null)
            playerController.ItIsAnotherObjectInHand();

        playerController.itemInHands = item;

        if (slotID != previousSlotID)
            playerController.TakeObjectInRightHand();
    }
    
    public bool IsNight() => dayTime != null && dayTime.DayProgress() > 0.5f;

    public void ChangePlayerSprite(Sprite sprite) =>
        playerController.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    public void BurnItemAtCampfire(CampfireManager campfire, ItemScriptableObject item)
    {
        objectLifeCycles?.BurnItem(campfire, item);
    }

}