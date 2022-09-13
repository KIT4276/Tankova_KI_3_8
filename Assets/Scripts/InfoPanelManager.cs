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
            var soldiers = FindObjectsOfType<SoldierController>();
            int r = 0;
            int g = 0;
            int b = 0;
            foreach (var item in soldiers)
            {
                if (item._colorType == ColorType.Red) r++;
                if (item._colorType == ColorType.Green) g++;
                if (item._colorType == ColorType.Blue) b++;
            }

            _aliveRed.text = r.ToString();
            _aliveGreen.text = g.ToString();
            _aliveBlue.text = b.ToString();
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
