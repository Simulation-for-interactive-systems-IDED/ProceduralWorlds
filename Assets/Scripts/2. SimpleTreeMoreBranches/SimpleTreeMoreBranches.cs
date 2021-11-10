using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTreeMoreBranches : MonoBehaviour
{
    [SerializeField]
    private GameObject branchPrefab;

    [SerializeField]
    private int totalNodes = 7;

    private void Start()
    {
        GenerateTree();
    }

    private void GenerateTree()
    {
        Queue<GameObject> frontier = new Queue<GameObject>();
        GameObject root = Instantiate(branchPrefab, transform);
        frontier.Enqueue(root);

        // Set name for debugging purpouses
        root.name = "root branch";

        int currentNode = 1;
        while (frontier.Count > 0)
        {
            if (currentNode > totalNodes) return;

            var branch = frontier.Dequeue();

            var leftBranch = CreateBranch(branch, 45); // Left
            var rightBranch = CreateBranch(branch, -45); // Right

            // Set name for debugging purpouses
            leftBranch.name = "left branch - from node " + currentNode;
            rightBranch.name = "right branch - from node " + currentNode;

            frontier.Enqueue(leftBranch);
            frontier.Enqueue(rightBranch);

            ++currentNode;
        }
    }

    private GameObject CreateBranch(GameObject prevBranch, float relativeAngle)
    {
        GameObject newBranch = Instantiate(branchPrefab, transform);

        newBranch.transform.position = prevBranch.transform.position + prevBranch.transform.up;
        Quaternion rotation = prevBranch.transform.rotation;

        newBranch.transform.rotation = rotation * Quaternion.Euler(0f, 0f, relativeAngle);

        return newBranch;
    }
}
