using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class LevelGrid : MonoBehaviour
    {
        public static LevelGrid Instance { get; private set; }
        public event EventHandler OnAnyUnitMovedGridPosition;



        private GridSystem<GridObject> gridSystem;

        [SerializeField] private Transform gridDebugObjectPrefab;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitActionSystem!" + transform + "-" + Instance);
                Destroy(Instance);
                return;
            }
            Instance = this;

            gridSystem = new GridSystem<GridObject>(10, 10, 2f, 
                (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
            // gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }

        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)                        
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
        }

        public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnitList();
        }

        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveUnit(unit);
        }

        public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            RemoveUnitAtGridPosition(fromGridPosition, unit);

            AddUnitAtGridPosition(toGridPosition, unit);
            
            OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
        }

        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
        public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
        public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

        public int GetWidth() => gridSystem.GetWidth();
        public int GetHeight() => gridSystem.GetHeight();

        public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.HasAnyUnit();
        }
        
        public Unit GetUnitAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnit();
        }
        
    }
}
