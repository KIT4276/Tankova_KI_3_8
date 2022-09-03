using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class RedGateScript : GateScript
    {

        private void Awake()
        {
            string redPath = "Model/SoldersPrefabs/SoldierRed";
            _redSoldier = Resources.Load<GameObject>(redPath);
        }
        void Start()
        {
            StartCoroutine(CreatSoldier(_respawnDelay, _redSoldier, _respawnPoint, _pool));
        }
    }
}
