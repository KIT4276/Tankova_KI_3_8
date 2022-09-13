using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ziggurat
{
    public enum ColorType : byte
    {
        Red,
        Green,
        Blue
    }

    public class SoldierController : MonoBehaviour, IPointerClickHandler
    {
        protected Vector3 _destination = new Vector3(0,2,0);
        protected GameObject _enemy = null;
        //private GameObject _gameManager;
        private GameObject _alertSphere = null;
        private bool _isShowHP = false;

        protected float _maxHealth = 15f;
        protected float _health;
        private float _speed = 5f;
        private float _fastDamage = 3f;
        private float _strongDamage = 6f;
        private float _missProbability = 0.2f;
        private float _critProbability = 0.1f;
        private float _strongAttackProbability = 0.2f;
        public ColorType _colorType;
        private ColorType _unfriendlyColor1;
        private ColorType _unfriendlyColor2;

        [SerializeField] 
        private Canvas _healthCanvas;
        [SerializeField] 
        private Slider _healthSlider;

        [Space, SerializeField]
        public MeshRenderer _meshRenderer;
        [SerializeField]
        private SkinnedMeshRenderer _polySurface1;
        [SerializeField]
        private Material _highlightMaterial;

        [Space, Tooltip("Shield materials")]
        public Material _redMaterial;
        public Material _greenMaterial;
        public Material _blueMaterial;
        public SoldierController _selectedSold;

        Coroutine _attack = null;

        private bool _inBattle = false;

        private void Awake()
        {
            _alertSphere = this.transform.Find("Sphere").gameObject;
            ColorTypeDefinition();
            ReadingSettingFromConsole();
            _health = _maxHealth;
            StartCoroutine(Move());
        }
        private void Start()
        {
            switch (_colorType)
            {
                case ColorType.Red:
                    _meshRenderer.material = _redMaterial;
                    break;
                case ColorType.Green:
                    _meshRenderer.material = _greenMaterial;
                    break;
                case ColorType.Blue:
                    _meshRenderer.material = _blueMaterial;
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if(_alertSphere.GetComponent<AttackSphereComponent>()._enemy != null) SwitchingToBattleMode();
            if (_destination == null) _destination = new Vector3(0f, 2f, 0f);
        }

        private void LateUpdate()
        {
            _isShowHP = InfoPanelManager.Self.IsShowingHealth();
            if (_isShowHP) ShowHealth();
            ReadingSettingFromConsole();
            CheckHealth();
        }

        public void ColorTypeDefinition()
        {
            switch (_colorType)
            {
                case ColorType.Red:
                    _unfriendlyColor1 = ColorType.Green;
                    _unfriendlyColor2 = ColorType.Blue;
                    break;
                case ColorType.Green:
                    _unfriendlyColor1 = ColorType.Red;
                    _unfriendlyColor2 = ColorType.Blue;
                    break;
                case ColorType.Blue:
                    _unfriendlyColor1 = ColorType.Green;
                    _unfriendlyColor2 = ColorType.Red;
                    break;
                default:
                    break;
            }
        }

        private void SwitchingToBattleMode() 
        {
            if (_enemy == null) 
            {
                _enemy = _alertSphere.GetComponent<AttackSphereComponent>()._enemy;
                ColorType enemyColor = _enemy.GetComponent<SoldierController>()._colorType;

                
                if ((enemyColor != this._colorType) && ((_enemy.GetComponent<SoldierController>()._colorType == ColorType.Blue) || 
                    (_enemy.GetComponent<SoldierController>()._colorType == ColorType.Green) || (_enemy.GetComponent<SoldierController>()._colorType == ColorType.Red)))
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
            if (this.GetComponent<SoldierController>()._colorType == ColorType.Red)
            {
                _maxHealth = Settings._maxHealthRed;
                _speed = Settings._speedRed;
                _fastDamage = Settings._fastDamageRed;
                _strongDamage = Settings._strongDamageRed;
                _missProbability = Settings._missProbabilityRed;
                _critProbability = Settings._critProbabilityRed;
                _strongAttackProbability = Settings._strongAttackProbabilityRed;
            }

            else if(this.GetComponent<SoldierController>()._colorType == ColorType.Green)
            {
                _maxHealth = Settings._maxHealthGreen;
                _speed = Settings._speedGreen;
                _fastDamage = Settings._fastDamageGreen;
                _strongDamage = Settings._strongDamageGreen;
                _missProbability = Settings._missProbabilityGreen;
                _critProbability = Settings._critProbabilityGreen;
                _strongAttackProbability = Settings._strongAttackProbabilityGreen;
            }
             
            else if (this.GetComponent<SoldierController>()._colorType == ColorType.Blue)
            {
                _maxHealth = Settings._maxHealthBlue;
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
            if (_enemy != null) _destination = _enemy.transform.position;

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

                var step = _speed * Time.deltaTime;
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

            CheckHealth();
        }

        private void CheckHealth()
        {
            if (_health <= 0)
            {
                StopCoroutine(AttackCoroutine());
                Die();
            }
        }

        public void KillThemAll()
        {
            StopAllCoroutines();
            Die();
        }

        private void Die()
        {
            this.gameObject.GetComponent<UnitEnvironment>().StartAnimation("Die");

            if (this.GetComponent<SoldierController>()._colorType == ColorType.Red) InfoPanelManager.Self.SetKilledeRed();
            if (this.GetComponent<SoldierController>()._colorType == ColorType.Green) InfoPanelManager.Self.SetKilledeGreen();
            if (this.GetComponent<SoldierController>()._colorType == ColorType.Blue) InfoPanelManager.Self.SetKilledeBlue();
        }

        private bool DefinitionChanceStrongAttack()
        {
            var random = Random.Range(0f, 1f);

            if (random > _strongAttackProbability) return false;
            else return true;
        }

        public void ShowHealth()
        {
            if (_isShowHP)
            {
                _healthCanvas.transform.LookAt(Camera.main.transform.position);
                _healthCanvas.gameObject.SetActive(true);
                _healthSlider.value = _health/_maxHealth;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _selectedSold = this;
            _polySurface1.material = _highlightMaterial;
        }
    }
}
