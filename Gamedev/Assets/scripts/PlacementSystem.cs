using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();

        mousePosition.y = 0f;

        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        Vector3 worldPosition = grid.CellToWorld(gridPosition);

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = worldPosition;

    }
}
