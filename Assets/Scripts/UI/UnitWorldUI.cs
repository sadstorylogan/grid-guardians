using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class UnitWorldUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI actionPointsText;
        [SerializeField] private Unit unit;
        [SerializeField] private Image healthBarImage;
        [SerializeField] private HealthSystem healthSystem;

        private void Start()
        {
            Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
            healthSystem.OnDamaged += HealthSystem_OnDamaged;

            UpdateActionPointsText();
            UpdateHealthBar();
        }

        private void UpdateActionPointsText()
        {
            actionPointsText.text = unit.GetActionPoints().ToString();
        }

        private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
        {
            UpdateActionPointsText();
        }

        private void UpdateHealthBar()
        {
            healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
        }

        private void HealthSystem_OnDamaged(object sender, EventArgs e)
        {
            UpdateHealthBar();
        }
    }
}
