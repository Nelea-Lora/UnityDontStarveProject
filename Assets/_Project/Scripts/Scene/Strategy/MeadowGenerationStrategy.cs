namespace _Project.Scripts.Scene.Strategy
{
    public class MeadowGenerationStrategy: IMapGenerationStrategy
    {
        public void GenerateMap(GenerateGround context)
        {
            for (int x = 0; x < context.FloorCount; x++)
            {
                for (int y = 0; y < context.FloorCount; y++)
                {
                    context.SpawnGround(x, y);
                }
            }
        }
    }
}