using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogInit : Dialog
{

    override public void OpenComplete()
    {
        base.OpenComplete();
    }

    override public void Open()
    {
        if (_isOpened)
            return;
        base.Open();
    }
}
