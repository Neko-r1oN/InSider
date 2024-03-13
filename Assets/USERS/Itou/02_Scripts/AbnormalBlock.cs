using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    void Update()
    {

        if (this.transform.position.y <= 0.0f)
        {
            // transform‚ðŽæ“¾
            Transform myTransform = this.transform;

            // À•W‚ðŽæ“¾
            Vector3 pos = myTransform.position;
            pos.y += 0.005f;  // yÀ•W‚Ö0.005‰ÁŽZ

            myTransform.position = pos;  // À•W‚ðÝ’è
        }
    }
}
