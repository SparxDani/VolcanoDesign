using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UndergroundVolume : MonoBehaviour
{
    [Header("Depth Parameters")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float undergroundDepth = 10f;

    [Header("PP Volume")]
    [SerializeField] private Volume PPVolume;

    [Header("PP Profiles")]
    [SerializeField] private VolumeProfile surfacePP;
    [SerializeField] private VolumeProfile undergroundPP;

    [SerializeField] private float transitionSpeed = 2f;

    private VolumeProfile currentProfile;
    private float targetWeight = 0f;

    void Start()
    {
        currentProfile = surfacePP;
        PPVolume.profile = currentProfile;
        PPVolume.weight = 1f;
    }

    void Update()
    {
        bool isUnderground = mainCamera.position.y < undergroundDepth;

        VolumeProfile desiredProfile = isUnderground ? undergroundPP : surfacePP;
        targetWeight = isUnderground ? 1f : 1f;

        if (PPVolume.profile != desiredProfile)
        {
            PPVolume.profile = desiredProfile;
            PPVolume.weight = 0f;
            currentProfile = desiredProfile;
        }

        PPVolume.weight = Mathf.MoveTowards(PPVolume.weight, targetWeight, transitionSpeed * Time.deltaTime);
    }
}
