using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.Components
{
    public class FollowComponent : MonoBehaviour
    {
        private Transform target;

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void Update()
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, 2f * Time.deltaTime);
            }
        }
    }
}