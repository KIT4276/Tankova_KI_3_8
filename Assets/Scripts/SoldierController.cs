using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public enum ColorType : byte
    {
        Red,
        Green,
        Blue
    }

    public class SoldierController : MonoBehaviour
    {
        protected Vector3 _destination; //= new Vector3 (0f,2f,0f);
        protected GameObject _enemy = null;
        private GameObject _gameManager;
        private GameObject _alertSphere = null;
        private bool _isShowHP = false;

        private float _health = 100f;
        private float _speed = 5f;
        private float _fastDamage = 3f;
        private float _strongDamage = 6f;
        private float _missProbability = 0.2f;
        private float _critProbability = 0.1f;
        private float _strongAttackProbability = 0.2f;
        private ColorType _colorType;
        private ColorType _unfriendlyColor1;
        private ColorType _unfriendlyColor2;

        //Корутина удара
        Coroutine _attack = null;

        private bool _inBattle = false;

        private void Awake()
        {
            _alertSphere = this.transform.Find("Sphere").gameObject;
            _colorType = ColorTypeDefinition();
            ReadingSettingFromConsole();
            StartCoroutine(Move());
        }

        private void Update()
        {
            if(_alertSphere.GetComponent<AttackSphereComponent>()._enemy != null) SwitchingToBattleMode();
            if (_destination == null) _destination = new Vector3(0f, 2f, 0f);
        }

        private void LateUpdate()
        {
            ReadingSettingFromConsole();
        }

        public ColorType ColorTypeDefinition()
        {
            if (this.GetComponent<RedSoldierController>() != null)
            {
                _colorType = ColorType.Red;
                _unfriendlyColor1 = ColorType.Green;
                _unfriendlyColor2 = ColorType.Blue;
            }
            if (this.GetComponent<GreenSoldierController>() != null)
            {
                _colorType = ColorType.Green;
                _unfriendlyColor1 = ColorType.Red;
                _unfriendlyColor2 = ColorType.Blue;
            }
            if (this.GetComponent<BlueSoldierController>() != null)
            {
                _colorType = ColorType.Blue;
                _unfriendlyColor1 = ColorType.Green;
                _unfriendlyColor2 = ColorType.Red;
            }

            return _colorType;
        }

        private void SwitchingToBattleMode() 
        {
            if (_enemy == null) 
            {
                _enemy = _alertSphere.GetComponent<AttackSphereComponent>()._enemy;
                ColorType enemyColor = _enemy.GetComponent<SoldierController>().ColorTypeDefinition();

                
                if ((enemyColor != this._colorType) && ((_enemy.GetComponent<BlueSoldierController>() != null) || 
                    (_enemy.GetComponent<GreenSoldierController>() != null) || (_enemy.GetComponent<RedSoldierController>() != null)))
                {
                    if (_inBattle != true)
                    {
                        Attack(_enemy);
                    }
                }
            }
        }
        private void Attack(GameObject enemy)
        {
            _inBattle = true;
            _destination = enemy.transform.position;
        }

        private void ReadingSettingFromConsole()
        {
            if (this.GetComponent<GreenSoldierController>() != null)
            {
                _health = Settings._healthGreen;
                _speed = Settings._speedGreen;
                _fastDamage = Settings._fastDamageGreen;
                _strongDamage = Settings._strongDamageGreen;
                _missProbability = Settings._missProbabilityGreen;
                _critProbability = Settings._critProbabilityGreen;
                _strongAttackProbability = Settings._strongAttackProbabilityGreen;
            }
            else if (this.GetComponent<RedSoldierController>() != null)
            {
                _health = Settings._healthRed;
                _speed = Settings._speedRed;
                _fastDamage = Settings._fastDamageRed;
                _strongDamage = Settings._strongDamageRed;
                _missProbability = Settings._missProbabilityRed;
                _critProbability = Settings._critProbabilityRed;
                _strongAttackProbability = Settings._strongAttackProbabilityRed;
            }
            else if (this.GetComponent<BlueSoldierController>() != null )
            {
                _health = Settings._healthBlue;
                _speed = Settings._speedBlue;
                _fastDamage = Settings._fastDamageBlue;
                _strongDamage = Settings._strongDamageBlue;
                _missProbability = Settings._missProbabilityBlue;
                _critProbability = Settings._critProbabilityBlue;
                _strongAttackProbability = Settings._strongAttackProbabilityBlue;
            }
        }

        private IEnumerator Move()
        {
            while (true)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                Arrival(_destination);
            }
        }

        private void Arrival(Vector3 _destination)
        {
            if (_enemy != null)
            {
                //Debug.Log("_enemy != null");
                _destination = _enemy.transform.position;
            }

            this.gameObject.transform.LookAt(_destination);
            var _distance = Vector3.Distance(this.transform.position, _destination);

            if (_distance < 2f)
            {
                if (_attack == null)
                {
                    this.gameObject.GetComponent<UnitEnvironment>().Moving(0f);
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                if (_inBattle)
                {
                    if (_attack == null)
                    {
                        _attack = StartCoroutine(AttackCoroutine());
                    }
                }
            }
            else
            {
                this.gameObject.GetComponent<UnitEnvironment>().Moving(1f);
                //this.GetComponent<Rigidbody>().velocity = this.transform.forward * _speed;

                var step = _speed * Time.deltaTime;
                //var moveTarget = Opponent != null ? Opponent.transform : _defaultMoveTarget;
                transform.position = Vector3.MoveTowards(transform.position, _destination, step);
            }
        }

        private IEnumerator AttackCoroutine()
        {
            while (_inBattle)
            {
                yield return new WaitForSecondsRealtime(1f);

                if (DefinitionChanceStrongAttack())
                {
                    this.gameObject.GetComponent<UnitEnvironment>().StartAnimation("Strong");
                    _enemy.GetComponent<SoldierController>().DealingDamage(_strongDamage);
                }
                else
                {
                    this.gameObject.GetComponent<UnitEnvironment>().StartAnimation("Fast");
                    _enemy.GetComponent<SoldierController>().DealingDamage(_fastDamage);
                }
                
                if (_enemy == null)
                {
                    this.gameObject.GetComponent<UnitEnvironment>().Moving(0f);
                    _inBattle = false;
                    yield break;
                }
            }
        }

        private void DealingDamage(float damageType)
        {
            var random = Random.Range(0f, 1f);

            if (random > _missProbability)
            {
                random = Random.Range(0f, 1f);

                if (random < _critProbability) _health -= damageType * 2;
                else _health -= damageType;
            }
            else _health -= 0;

            CheckHealth();
        }

        private void CheckHealth()
        {
            if (_health <= 0)
            {
                StopCoroutine(AttackCoroutine());
                this.gameObject.GetComponent<UnitEnvironment>().StartAnimation("Die");
            }
        }

        private bool DefinitionChanceStrongAttack()
        {
            var random = Random.Range(0f, 1f);

            if (random > _strongAttackProbability) return false;
            else return true;
        }
    }
}
