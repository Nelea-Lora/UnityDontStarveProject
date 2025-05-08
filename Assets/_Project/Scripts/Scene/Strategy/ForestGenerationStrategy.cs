using UnityEngine;

namespace _Project.Scripts.Scene.Strategy
{
    public class ForestGenerationStrategy : IMapGenerationStrategy
    {
        public void GenerateMap(GenerateGround context)
        {
            for (int x = 0; x < context.FloorCount; x++)
            {
                for (int y = 0; y < context.FloorCount; y++)
                {
                    if (Random.value > 0.3f)
                        context.SpawnGround(x, y);
                    else
                        context.SpawnDarkGround(x, y);
                }
            }
        }
    }

}