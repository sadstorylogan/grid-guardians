using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public class MoveAction : BaseAction
    {
        private Vector3 targetPosition;
        
        public event EventHandler OnStartMoving;
        public event EventHandler OnStopMoving;

        
        [SerializeField] private int maxMoveDistance = 4;
    
        protected override void Awake()
        {
            base.Awake();
            targetPosition = transform.position;
        }

        private void Update()
        {
            if (!isActive)
            {
                return;
            }
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
        
            float stoppingDistance = .1f;
            if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                float moveSpeed = 4f;
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            }
            else
            {
                OnStopMoving?.Invoke(this, EventArgs.Empty);
                ActionComplete();
            }
        
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
           
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            OnStartMoving?.Invoke(this, EventArgs.Empty);
            
            ActionStart(onActionComplete);
        }
        
        public override List<GridPosition> GetValidActionGridPositionList()
        {
            var validGridPositionList = new List<GridPosition>();

            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    if (unitGridPosition == testGridPosition)
                    {
                        // Same Grid Position where the unit is already at
                        continue;
                    }

                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        // Grid Position already occupied with another Unit
                        continue;
                    }

                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }

        public override string GetActionName()
        {
            return "Move";
        }
        
        public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
        {
            int targetCountAtGridPosition = unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);

            return new EnemyAIAction
            {
                gridPosition = gridPosition,
                actionValue = targetCountAtGridPosition * 10,
            };
        }

    }
}
