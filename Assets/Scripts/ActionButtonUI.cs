using System.Collections;
using System.Collections.Generic;
using Actions;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;

    public void SetBaseAction(BaseAction baseAction)
    {
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        
        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }
    
}
