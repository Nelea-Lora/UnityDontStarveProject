using Vector3 = UnityEngine.Vector3;

namespace _Project.Scripts.Memento
{
    [System.Serializable]
    public class GameState
    {
        public Vector3 PlayerPosition;
        public float Health;
        public float Hunger;
        public float Mind;
        public ItemScriptableObject ItemInHands;
    }

}