using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class AttackSphereComponent : MonoBehaviour
    {
        public GameObject _enemy = null;

        private void OnTriggerEnter(Collider _collider)
        {
            if (_collider.gameObject.GetComponent<SoldierController>() != null && 
                EnemyCheck(_collider.gameObject.GetComponent<SoldierController>().ColorTypeDefinition()))
                _enemy = _collider.gameObject;
        }
        private bool EnemyCheck(ColorType colliderColorType)
        {
            if(colliderColorType != transform.parent.gameObject.GetComponent<SoldierController>().ColorTypeDefinition())
                 return true;
            else return false;
        }
    }
}
