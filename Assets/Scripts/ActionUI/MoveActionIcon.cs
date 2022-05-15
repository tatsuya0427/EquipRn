using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveActionIcon : MonoBehaviour
{
    public void Set(){
        this.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(660f, -378f, 0f), 0.3f);
    }

    public void Remove(){
        this.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(636f, -750f, 0f), 0.3f);
    }
}
