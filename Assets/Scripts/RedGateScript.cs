using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
