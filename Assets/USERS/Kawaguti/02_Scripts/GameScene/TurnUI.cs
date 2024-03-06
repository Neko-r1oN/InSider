using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる


public class TurnUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //StartCoroutine(PanelAnim());
    }
    
    public IEnumerator PanelAnim()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);

        yield return new WaitForSeconds(0.5f);

        yield return transform.DORotate(new Vector3(0, 0, 0), 1.0f).WaitForCompletion();

        transform.DORotate(new Vector3(90, 0, 0), 0.7f).OnComplete(SetActive);
    }
    
    private void SetActive()
    {
        this.gameObject.SetActive(false);
    }
}
