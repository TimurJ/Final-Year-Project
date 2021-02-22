using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineScript : MonoBehaviour
{
    public static CinemachineScript Instance { get; private set; }
    private bool searchingForPlayer = false;
    private Transform Player;
    private CinemachineVirtualCamera Vcam;
    private float shakeTimer;

    void Awake()
    {
        Instance = this;
        Vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
	{
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
	}

	private void Update()
	{
		if(shakeTimer > 0)
		{
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
			{
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
		}
	}
	void FixedUpdate()
    {
        if(Player == null)
		{
			if (!searchingForPlayer)
			{
                searchingForPlayer = true;
                StartCoroutine(FindingPlayer());
			}
            return;
		}
        Vcam.Follow = Player;
    }

    IEnumerator FindingPlayer()
	{
        GameObject Result = GameObject.FindGameObjectWithTag("Player");
        if(Result == null)
		{
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FindingPlayer());
		}
		else
		{
            Player = Result.transform;
            searchingForPlayer = false;
            FixedUpdate();
            yield return null;
		}
	}
}
