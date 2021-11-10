using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTree : MonoBehaviour
{
    [SerializeField]
    private GameObject branchPrefab;

    [SerializeField]
    private int recursiveDepth = 20;
    private int currentDepth = 0;

    // Start is called before the first frame update
    private void Start()
    {
        GenerateBranch(null);
    }

    private void GenerateBranch(GameObject prevBranch)
    {
        if (currentDepth > recursiveDepth) return;
        ++currentDepth;

        GameObject branch = Instantiate(branchPrefab, transform);
        if (prevBranch != null)
        {
            branch.transform.position = prevBranch.transform.position + prevBranch.transform.up;
            Quaternion rotation = prevBranch.transform.rotation;

            rotation *= Quaternion.Euler(0f, 0f, 45f);
            branch.transform.rotation = rotation;
        }

        GenerateBranch(branch);
    }
}
