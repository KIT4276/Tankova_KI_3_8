namespace Ziggurat
{
    public class GreenGateScript : GateScript
    {
        void Start()
        {
            StartCoroutine(CreatSoldier(ColorType.Green, _respawnDelay, _greenSoldier, _respawnPoint, _pool));
        }
    }
}
