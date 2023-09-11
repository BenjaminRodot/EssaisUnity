using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite pressedImage;

    [SerializeField] private KeyCode keyToPress;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        /*
        if(Input.GetKeyDown(keyToPress))
        {
            _spriteRenderer.sprite = pressedImage;
        }
        if(Input.GetKeyUp(keyToPress)) 
        {
            _spriteRenderer.sprite = defaultImage;
        }*/

    }
}
