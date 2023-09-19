using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; set; }
        
        [SerializeField] private Transform gridSystemVisualSinglePrefab;

        private GridSystemVisualSingle[,] gridSystemVisualSingleArray;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitActionSystem!" + transform + "-" + Instance);
                Destroy(Instance);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            gridSystemVisualSingleArray = 
                new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
            
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    var gridPosition = new GridPosition(x, z);
                    var gridSystemVisualSingleTransform = 
                        Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                    gridSystemVisualSingleArray[x, z] =
                        gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        public void HideAllGridPosition()
        {
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    gridSystemVisualSingleArray[x, z].Hide();
                }
            }
            
        }

        public void ShowGridPositionList(List<GridPosition> gridPositionList)
        {
            foreach (GridPosition gridPosition in gridPositionList)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
            }
        }

        private void UpdateGridVisual()
        {
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            HideAllGridPosition();
            ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}
