using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchColorizer : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer branch;

    [SerializeField]
    SpriteRenderer node;

    // Start is called before the first frame update
    void Start()
    {
        branch.color = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
        node.color = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)); 
    }
}
