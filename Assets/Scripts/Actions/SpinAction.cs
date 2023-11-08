using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class SpinAction : BaseAction
    {
        private float totalSpinAmount;
        
    
        // Update is called once per frame
        private void Update()
        {
            if (!isActive)
            {
                return;
            }
            float spinAddAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

            totalSpinAmount += spinAddAmount;
            if (totalSpinAmount >= 360f)
            {
                ActionComplete();
            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            totalSpinAmount = 0f;
            ActionStart(onActionComplete);
        }
        
        public override string GetActionName()
        {
            return "Spin";
        }

        public override List<GridPosition> GetValidActionGridPositionList()
        {
            var unitGridPosition = unit.GetGridPosition();

            return new List<GridPosition>
            {
                unitGridPosition
            };
        }

        public override int GetActionPointsCost()
        {
            return 2;
        }
    }
}
