using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class BlueGateScript : GateScript
    {

        private void Awake()
        {
            string bluePath = "Model/SoldersPresabs/SoldierBlue";
            _blueSoldier = Resources.Load<GameObject>(bluePath);
        }

        void Start()
        {
            StartCoroutine(CreatSoldier(_respawnDelay, _blueSoldier, _respawnPoint, _pool));
        }
    }
}
