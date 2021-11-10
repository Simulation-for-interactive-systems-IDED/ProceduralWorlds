using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRandom : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject branchPrefab;

    [Header("Creation parameters")]
    [SerializeField]
    private int totalLevels = 3;

    [SerializeField]
    private int rootLength = 4;

    [SerializeField]
    [Range(0, 1)]
    private float reductionPerLevel = 0.1f;

    private float currentLength = -1;
    private int currentLevel = 1;
    private Queue<GameObject> frontier = new Queue<GameObject>();

    private void Start()
    {
        // Create the root branch
        GameObject root = Instantiate(branchPrefab, transform);
        SetBranchLength(root, rootLength);

        // Set name for debugging purposes
        root.name = "root branch";

        // Initialize fields
        currentLength = rootLength;
        frontier.Enqueue(root);

        GenerateTree();
    }

    private void GenerateTree()
    {
        if (currentLevel > totalLevels) return;
        ++currentLevel;

        // Track all nodes created in this level
        List<GameObject> levelNodes = new List<GameObject>();

        // Calculate the new length
        currentLength -= currentLength * reductionPerLevel;

        while (frontier.Count > 0)
        {
            var branch = frontier.Dequeue();

            var leftBranch = CreateBranch(branch, Random.Range(0, 45)); // Left
            var rightBranch = CreateBranch(branch, Random.Range(0, -45)); // Right

            // Set branch length
            SetBranchLength(leftBranch, currentLength);
            SetBranchLength(rightBranch, currentLength);

            // Set name for debugging purpouses
            leftBranch.name = "left branch - level " + currentLevel;
            rightBranch.name = "right branch - level " + currentLevel;

            levelNodes.Add(leftBranch);
            levelNodes.Add(rightBranch);
        }

        foreach (var newNode in levelNodes)
        {
            frontier.Enqueue(newNode);
        }

        GenerateTree();
    }

    private GameObject CreateBranch(GameObject prevBranch, float relativeAngle)
    {
        GameObject newBranch = Instantiate(branchPrefab, transform);

        newBranch.transform.position = prevBranch.transform.position + prevBranch.transform.up * GetBranchLength(prevBranch);
        Quaternion rotation = prevBranch.transform.rotation;

        newBranch.transform.rotation = rotation * Quaternion.Euler(0f, 0f, relativeAngle);

        return newBranch;
    }

    private void SetBranchLength(GameObject branch, float lenght)
    {
        Transform line = branch.transform.GetChild(0);
        Transform circle = branch.transform.GetChild(1);
        line.localScale = new Vector3(line.localScale.x, lenght, line.localScale.z);
        line.localPosition = new Vector3(line.localPosition.x, lenght / 2, line.localPosition.z);
        circle.localPosition = new Vector3(circle.localPosition.x, lenght, circle.localPosition.z);
    }

    private float GetBranchLength(GameObject branch)
    {
        Transform line = branch.transform.GetChild(0);
        return line.localScale.y;
    }

}
