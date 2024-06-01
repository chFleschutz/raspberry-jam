using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    private Button yourButton;
    public GameObject buttonRoot;

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Animator animator = buttonRoot.GetComponent<Animator>();
        animator.enabled = true;

        animator.CrossFade("ButtonPressed", 0);
        //newScale = transform.localScale - new Vector3(0.2F, 0.2F, 0);
        //transform.localScale = Vector3.Lerp (transform.localScale, newScale, speed * Time.deltaTime);
    }
}
