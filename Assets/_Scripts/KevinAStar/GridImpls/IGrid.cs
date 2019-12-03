using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrid {

    void Create(int xSize, int ySize);
    void Clear();

    Node[,] GetGrid();

    Node GetNodeFromGrid(int x, int y);
    List<Node> GetNeighbors(Node node);

	int GetHCost(Node node, Node target);

    int GetDistanceBetween(Node current, Node neighbor);

}
