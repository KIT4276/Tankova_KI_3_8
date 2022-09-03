using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat
{
    public class InfoPanelManager : MonoBehaviour
    {
        [SerializeField]
        private Text _aliveRed;
        [SerializeField]
        private Text _aliveGreen;
        [SerializeField]
        private Text _aliveBlue;

        [Space, SerializeField]
        private Text _killedRed;
        [SerializeField]
        private Text _killedGreen;
        [SerializeField]
        private Text _killedBlue;

        private int killedRed;
        private int killedGreen;
        private int killedBlue;

        private bool _isShowingHealth = false;

        public static InfoPanelManager Self;

        private void Start()
        {
            Self = this;
        }

        private void LateUpdate()
        {
            FindAllAlive();
            SetAllKilled();
        }

        public bool IsShowingHealth() => _isShowingHealth;

        public void ToShowHP()
        {
            if (!_isShowingHealth) _isShowingHealth = true;
            else _isShowingHealth = false;
        }

        private void FindAllAlive()
        {
            _aliveRed.text = FindObjectsOfType<RedSoldierController>().Length.ToString();
            _aliveGreen.text = FindObjectsOfType<GreenSoldierController>().Length.ToString();
            _aliveBlue.text = FindObjectsOfType<BlueSoldierController>().Length.ToString();
        }

        #region Dead entry block
        public void SetKilledeRed()
        {
            killedRed++;
        }

        public void SetKilledeGreen()
        {
            killedGreen++;
        }

        public void SetKilledeBlue()
        {
            killedBlue++;
        }
        #endregion

        private void SetAllKilled()
        {
            _killedRed.text = killedRed.ToString();
            _killedGreen.text = killedGreen.ToString();
            _killedBlue.text = killedBlue.ToString();
        }

        public void ClearTheDead()
        {
            killedRed = 0;
            killedGreen = 0;
            killedBlue = 0;
        }

        public void KillThemAll()
        {
            var r = FindObjectsOfType<SoldierController>();
            foreach (var item in r)
            {
                item.KillThemAll();
            }
        }
    }
}
