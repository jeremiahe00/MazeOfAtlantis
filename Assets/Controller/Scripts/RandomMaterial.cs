using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Renderer>().material = GetRandomMaterial();
    }

    public Material GetRandomMaterial()
    {
        int x = Random.Range(0, 3);
        if (x == 0)
            return Resources.Load("BlueM") as Material;
        else if (x == 1)
            return Resources.Load("GBM") as Material;
        else if (x == 2)
            return Resources.Load("GreenM") as Material;
        else
            return Resources.Load("Dirt") as Material;
    }
}
