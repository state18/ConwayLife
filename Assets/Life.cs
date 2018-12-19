using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

    public int boardWidth;
    public float updateFreq;
    public float initAliveChance;
    public Cell cellPrefab;

    private Cell[,] cells;


    private float timeSinceLastUpdate;

	// Use this for initialization
	void Start () {

        Camera.main.orthographicSize = boardWidth / 2;
        // Camera.main.orthographicSize = Screen.height / 32 / 2;
        Camera.main.transform.position = new Vector3(boardWidth / 2, boardWidth / 2, -10);

        cells = new Cell[boardWidth, boardWidth];

        for (int i = 0; i < boardWidth; i++) {
            for (int j = 0; j < boardWidth; j++) {
                cells[i, j] = Instantiate(cellPrefab, new Vector3(i, j, 0), cellPrefab.transform.rotation);
                cells[i, j].IsAlive = Random.Range(0f, 1f) < initAliveChance;
            }
        }
	}

    // Update is called once per frame
    void Update() {

        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate < updateFreq)
            return;

        timeSinceLastUpdate = 0;

        bool[,] newStatus = new bool[boardWidth, boardWidth];

        for (int i = 0; i < boardWidth; i++) {
            for (int j = 0; j < boardWidth; j++) {

                Cell currCell = cells[i, j];

                int numNeighbors = 0;

                if (i > 0 && cells[i - 1, j].IsAlive)
                    numNeighbors++;

                if (i < boardWidth - 1 && cells[i + 1, j].IsAlive)
                    numNeighbors++;

                if (j > 0 && cells[i, j - 1].IsAlive)
                    numNeighbors++;

                if (j < boardWidth - 1 && cells[i, j + 1].IsAlive)
                    numNeighbors++;

                // Diagonals

                if (i > 0 && j > 0 && cells[i - 1, j - 1].IsAlive)
                    numNeighbors++;

                if (i > 0 && j < boardWidth - 1 && cells[i - 1, j + 1].IsAlive)
                    numNeighbors++;

                if (i < boardWidth - 1 && j > 0 && cells[i + 1, j - 1].IsAlive)
                    numNeighbors++;

                if (i < boardWidth - 1 && j < boardWidth - 1 && cells[i + 1, j + 1].IsAlive)
                    numNeighbors++;

                // Get new status.
                if (currCell.IsAlive)
                    newStatus[i, j] = numNeighbors == 2 || numNeighbors == 3;
                else
                    newStatus[i, j] = numNeighbors == 3;

            }
        }

        // Update statuses.
        for (int i = 0; i < boardWidth; i++) {
            for (int j = 0; j < boardWidth; j++) {
                cells[i, j].IsAlive = newStatus[i, j];
            }
        }
    }
}
