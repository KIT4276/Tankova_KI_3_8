namespace Ziggurat
{
    public class BlueGateScript : GateScript
    {
        void Start()
        {
            StartCoroutine(CreatSoldier(ColorType.Blue, _respawnDelay, _blueSoldier, _respawnPoint, _pool));
        }
    }
}
