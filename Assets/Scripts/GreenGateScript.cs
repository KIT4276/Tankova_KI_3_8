using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class GreenGateScript : GateScript
    {

        private void Awake()
        {
            string greenPath = "Model/SoldersPresabs/SoldierGreen";
            _greenSoldier = Resources.Load<GameObject>(greenPath);
        }

        void Start()
        {
            StartCoroutine(CreatSoldier(_respawnDelay, _greenSoldier, _respawnPoint, _pool));
        }
    }
}
