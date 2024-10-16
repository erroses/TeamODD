using System.Collections;
using System.Collections.Generic;

using GameJam.Project;

using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    AudioManager audioManager;
    private bool isPaused = true;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        audioManager.AudioPause(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �ð� �����ϰ� ����� ��ġ�� �Ͻ����� ���¿� ���� ����
            audioManager.AudioPause(isPaused); // ������� ���� �Ǵ� �簳
            isPaused = !isPaused;
        }
    }
}
