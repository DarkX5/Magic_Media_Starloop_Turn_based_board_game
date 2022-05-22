using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer[] characters = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        int idx = Random.Range(0, characters.Length);
        for(int i = 0; i < characters.Length; i += 1) {
            if (i == idx) {
                characters[i].gameObject.SetActive(true);
            } else {
                // better to let GC clean whatever objects aren't used
                Destroy(characters[i].gameObject);
                // maybe better performance at the cost of extra space
                // characters[i].gameObject.SetActive(false);
            }
        }
    }
}
