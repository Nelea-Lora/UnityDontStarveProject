namespace _Project.Scripts.Scene.Strategy
{
    public class DesertGenerationStrategy : IMapGenerationStrategy
    {
        public void GenerateMap(GenerateGround context)
        {
            for (int x = 0; x < context.FloorCount; x++)
            {
                for (int y = 0; y < context.FloorCount; y++)
                {
                    if ((x + y) % 2 == 0)
                        context.SpawnGround(x, y);
                    else
                        context.SpawnDarkGround(x, y);
                }
            }
        }
    }
    
}