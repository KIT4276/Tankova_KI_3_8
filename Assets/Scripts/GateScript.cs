using System.Collections;
using UnityEngine;

namespace Ziggurat
{
    public class GateScript : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _respawnPoint;
        [SerializeField]
        protected float _respawnDelay = 6;
        [SerializeField, Tooltip("Parent of soldiers of the same color")]
        protected Transform _pool;

        protected GameObject _greenSoldier;
        protected GameObject _redSoldier;
        protected GameObject _blueSoldier;

        private void Awake()
        {
            string path = "Model/SoldersPrefabs/Soldier";

            _redSoldier = Resources.Load<GameObject>(path);
            _greenSoldier = Resources.Load<GameObject>(path);
            _blueSoldier = Resources.Load<GameObject>(path);
        }

        protected IEnumerator CreatSoldier(ColorType сolorType, float respawnDelay, GameObject soldier, GameObject respawnPoint, Transform pool)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(respawnDelay);
                var newSolder = Instantiate(soldier, respawnPoint.transform.position, Quaternion.identity, pool);
                newSolder.GetComponent<SoldierController>()._colorType = сolorType;
            }
        }
    }
}
