using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    // �^�C�}�[���~�܂��Ă邩�ǂ���
    bool isTimerStop;
    // �b��
    private float seconds;
    //�O��update�̎��̕b��
    private float oldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        // 40�b�ɐݒ�
        seconds = 40f;
        oldSeconds = 40f;
        isTimerStop = false;
    }

    public float Seconds()
    {
        return seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (EditorManager.Instance.useServer)
        {
            if (ClientManager.Instance.isGetNotice == false)
            {// �Q�[���J�n�ʒm����M���Ă��Ȃ��ꍇ
                return;
            }
        }

        if (isTimerStop == false)
        {
            seconds -= Time.deltaTime;

            if (seconds <= 0)
            {
                seconds = 0;

                TimerStop();
            }
        }

        oldSeconds = seconds;
    }

    public void TimerStop()
    {
        isTimerStop = true;

        Debug.Log("�I��");
    }
}
