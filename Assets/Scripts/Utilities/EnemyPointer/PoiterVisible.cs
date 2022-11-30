using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiterVisible : MonoBehaviour
{
    private Pointer pointer;

    private void Start() {
        pointer = GetComponentInParent<Pointer>();
        if (pointer is null || !pointer.enabled) this.gameObject.SetActive(false);
    }
}
