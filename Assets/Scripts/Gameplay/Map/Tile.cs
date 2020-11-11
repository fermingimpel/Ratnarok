using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] int pathIndex;
    [SerializeField] int lookAt; 
    public int GetPathIndex() {
        return pathIndex;
    }
    public int GetLookAt() {
        return lookAt;
    }
}
