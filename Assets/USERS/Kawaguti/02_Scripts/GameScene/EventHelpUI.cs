using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる
public class EventHelpUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        StartCoroutine(PanelAnim());
    }

    public IEnumerator PanelAnim()
    {
        yield return new WaitForSeconds(1.5f);

        yield return transform.DORotate(new Vector3(0, 0, 0), 3.0f).WaitForCompletion();
        yield return new WaitForSeconds(1.5f);
        transform.DORotate(new Vector3(90, 0, 0), 0.7f);
    }
}

