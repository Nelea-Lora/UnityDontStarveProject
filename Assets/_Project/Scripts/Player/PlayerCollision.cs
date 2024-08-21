using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject _stonePrefab;
    private int _stoneCount;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StoneController stone))
        {
            _stoneCount++;
            print("_stoneCount"+_stoneCount);
            Destroy(stone.gameObject);
        }
    }
}
