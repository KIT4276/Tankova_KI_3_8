namespace Ziggurat
{
    public class RedGateScript : GateScript
    {
        void Start()
        {
            StartCoroutine(CreatSoldier(ColorType.Red, _respawnDelay, _redSoldier, _respawnPoint, _pool));
        }
    }
}
