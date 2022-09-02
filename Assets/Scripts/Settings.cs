using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat
{
    public class Settings : MonoBehaviour
    {
        #region "Variable block"
        
        //Red units
        public static float _healthRed = 6f;
        public static float _speedRed = 3f;
        public static float _fastDamageRed = 3f;
        public static float _strongDamageRed = 6f;
        public static float _missProbabilityRed = 0.2f;
        public static float _critProbabilityRed = 0.1f;
        public static float _strongAttackProbabilityRed = 0.2f;

        //Green units
        public static float _healthGreen = 6f;
        public static float _speedGreen = 3f;
        public static float _fastDamageGreen = 3f;
        public static float _strongDamageGreen = 6f;
        public static float _missProbabilityGreen = 0.2f;
        public static float _critProbabilityGreen = 0.1f;
        public static float _strongAttackProbabilityGreen = 0.2f;

        //Blue units
        public static float _healthBlue = 6f;
        public static float _speedBlue = 3f;
        public static float _fastDamageBlue = 3f;
        public static float _strongDamageBlue = 6f;
        public static float _missProbabilityBlue = 0.2f;
        public static float _critProbabilityBlue = 0.1f;
        public static float _strongAttackProbabilityBlue = 0.5f;
        #endregion

        #region "Sliders block"
        
        //Red units
        [SerializeField]
        Slider _redHealthSlider;
        [SerializeField]
        Slider _redSpeedSlider;
        [SerializeField]
        Slider _redFastAttackDamageSlider;
        [SerializeField]
        Slider _redStrongAttackDamageSlider;
        [SerializeField]
        Slider _redMissSlider;
        [SerializeField]
        Slider _redCritSlider;
        [SerializeField]
        Slider _redStrongAttackSlider;

        //Green units
        [SerializeField]
        Slider _greenHealthSlider;
        [SerializeField]
        Slider _greenSpeedSlider;
        [SerializeField]
        Slider _greenFastAttackDamageSlider;
        [SerializeField]
        Slider _greenStrongAttackDamageSlider;
        [SerializeField]
        Slider _greenMissSlider;
        [SerializeField]
        Slider _greenCritSlider;
        [SerializeField]
        Slider _greenStrongAttackSlider;

        //Blue units
        [SerializeField]
        Slider _blueHealthSlider;
        [SerializeField]
        Slider _blueSpeedSlider;
        [SerializeField]
        Slider _blueFastAttackDamageSlider;
        [SerializeField]
        Slider _blueStrongAttackDamageSlider;
        [SerializeField]
        Slider _blueMissSlider;
        [SerializeField]
        Slider _blueCritSlider;
        [SerializeField]
        Slider _blueStrongAttackSlider;
        #endregion

        #region "Block for changing the red units parameters"
        public void SetHealthRed()
        {
            _healthRed = _redHealthSlider.value;
        }

        public void SetSpeedRed()
        {
            _speedRed = _redSpeedSlider.value;
        }

        public void SetFastAttackDamageRed()
        {
            _fastDamageRed = _redFastAttackDamageSlider.value;
        }

        public void SetStrongAttackDamageRed()
        {
            _strongDamageRed = _redStrongAttackDamageSlider.value;
        }
        public void SetMissRed()
        {
            _missProbabilityRed = _redMissSlider.value;
        }
        public void SetCritRed()
        {
            _critProbabilityRed = _redCritSlider.value;
        }
        public void SetStrongAttackRed()
        {
            _strongAttackProbabilityRed = _redStrongAttackSlider.value;
        }
        #endregion

        #region "Block for changing the green units parameters"
        public void SetHealthGreen()
        {
            _healthGreen = _greenHealthSlider.value;
        }

        public void SetSpeedreen()
        {
            _speedGreen = _greenSpeedSlider.value;
        }

        public void SetFastAttackDamageGreen()
        {
            _fastDamageGreen = _greenFastAttackDamageSlider.value;
        }

        public void SetStrongAttackDamageGreen()
        {
            _strongDamageGreen = _greenStrongAttackDamageSlider.value;
        }
        public void SetMissGreen()
        {
            _missProbabilityGreen = _greenMissSlider.value;
        }
        public void SetCritGreen()
        {
            _critProbabilityGreen = _greenCritSlider.value;
        }
        public void SetStrongAttackGreen()
        {
            _strongAttackProbabilityGreen = _greenStrongAttackSlider.value;
        }

        #endregion

        #region "Block for changing the blue units parameters"
        public void SetHealthBlue()
        {
            _healthBlue = _blueHealthSlider.value;
        }

        public void SetSpeedBlue()
        {
            _speedBlue = _blueSpeedSlider.value;
        }

        public void SetFastAttackDamageBlue()
        {
            _fastDamageBlue = _blueFastAttackDamageSlider.value;
        }

        public void SetStrongAttackDamageBlue()
        {
            _strongDamageBlue = _blueStrongAttackDamageSlider.value;
        }
        public void SetMissBlue()
        {
            _missProbabilityBlue = _blueMissSlider.value;
        }
        public void SetCritBlue()
        {
            _critProbabilityBlue = _blueCritSlider.value;
        }
        public void SetStrongAttackBlue()
        {
            _strongAttackProbabilityBlue = _blueStrongAttackSlider.value;
        }
        #endregion
    }
}
