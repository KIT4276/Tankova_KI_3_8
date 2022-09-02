using System.Collections;
using System.Collections.Generic;
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

        protected IEnumerator CreatSoldier(float respawnDelay, GameObject soldier, GameObject respawnPoint, Transform pool)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(respawnDelay);
                Instantiate(soldier, respawnPoint.transform.position, Quaternion.identity, pool);
            }
        }
    }
}
