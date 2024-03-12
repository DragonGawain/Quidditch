using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    // VARIABLES
    private Node myRootNode = null;



    // METHODS
    protected abstract Node PlantTheTree();

    protected void Start()
    {
        myRootNode = PlantTheTree();
    }

    private void Update()
    {
        if (myRootNode != null)
        {
            myRootNode.Execute();
        }
    }
}
