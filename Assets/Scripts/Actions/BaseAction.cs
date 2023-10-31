using System;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit unit;
        protected bool isActive;
        protected Action onActionComplete;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }
        
        public abstract string GetActionName();
    }
}
