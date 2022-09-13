using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class BlueGateScript : GateScript
    {

        //private void Awake()
        //{
        //    string bluePath = "Model/SoldersPrefabs/SoldierBlue";
        //    _blueSoldier = Resources.Load<GameObject>(bluePath);
        //}

        void Start()
        {
            StartCoroutine(CreatSoldier(ColorType.Blue, _respawnDelay, _blueSoldier, _respawnPoint, _pool));
        }
    }
}
