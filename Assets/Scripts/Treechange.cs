using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treechange : MonoBehaviour
{
    public GameManager manager;
    public Sprite[] sprites;
    bool chk;

    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void LateUpdate()
    {
        TreeChange(manager.month);
    }
    void TreeChange(int m)
    {
        spriteRenderer.sprite = sprites[m];
    }
}
