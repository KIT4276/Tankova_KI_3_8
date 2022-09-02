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
            {
                _enemy = _collider.gameObject;
                //Debug.Log("враг - " + _collider.gameObject.name);
            }
        }
        private bool EnemyCheck(ColorType colliderColorType)
        {
            
            //if (((colorType == ColorType.Red) || (colorType == ColorType.Red) || (colorType == ColorType.Red)) &&
                if(colliderColorType != transform.parent.gameObject.GetComponent<SoldierController>().ColorTypeDefinition())
            {
                //Debug.Log("Enemy Check true");
                return true;
            }
            else
            {
                //Debug.Log("Enemy Check false");
                return false;
            }
        }
    }
}
