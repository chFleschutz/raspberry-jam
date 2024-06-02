using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpdateCollider : MonoBehaviour
{
    public void UpdateColiderSize()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
            return;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            return;

        boxCollider.size = spriteRenderer.size;
        boxCollider.offset = Vector2.zero;
    }

    public void UpdateColliderSizeChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (!child.TryGetComponent<BoxCollider2D>(out var boxCollider))
                continue;

            if (!child.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                continue;

            boxCollider.size = spriteRenderer.size;
            boxCollider.offset = Vector2.zero;
        }
    }
}
