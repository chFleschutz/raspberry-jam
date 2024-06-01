using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

    public GameObject dialogRoot;

    protected bool _isOpened = false;

    public void Close()
    {
        if (!_isOpened)
            return;
        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = true;
        animator.CrossFade("dialog_hide_stone", 0);
        _isOpened = false;
    }

    public void CloseComplete()
    {
        if (dialogRoot)
            dialogRoot.SetActive(false);
        else
            gameObject.SetActive(false);
        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
        animator.StopPlayback();
    }
    virtual public void Open()
    {
        if (_isOpened)
            return;
        if (dialogRoot)
            dialogRoot.SetActive(true);
        else
            gameObject.SetActive(true);
        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = true;
        animator.CrossFade("dialog_open_stone", 0);
        _isOpened = true;
    }

    //오픈이 끝나면
    virtual public void OpenComplete()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.StopPlayback();
        animator.enabled = false;
    }
}
