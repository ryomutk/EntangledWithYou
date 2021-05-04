﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WorldFadeMotion : EnterExitModule
{

    public static WorldFadeMotion instance = null;

    protected override void Awake()
    {
        base.Awake();
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this as WorldFadeMotion;
        }
        else
        {
            Debug.LogWarning(this.GetType() + "is singleton! now been killed");
            Destroy(this.gameObject);
        }
    }

    [SerializeField] float inDuration = 1;
    [SerializeField] float outDuration = 1;
    [SerializeField] Ease inEase = Ease.InExpo;
    [SerializeField] Ease outEase = Ease.OutExpo;
    [SerializeField] Color defaultColor = Color.clear;
    Image panel;

    protected override void Initialize()
    {
        panel = GetComponentInChildren<Image>();
        panel.color = defaultColor;
    }

    public override Sequence Enter(float timescale = 1, bool activate = true)
    {
        var ENTER = base.EnterInitiailzer(timescale, activate);

        ENTER.Append(panel.DOFade(1, inDuration).SetEase(inEase));

        if (activate)
        {
            ENTER.Play();
        }

        return ENTER;
    }


    public override Sequence Exit(float timescale = 1, bool activate = true)
    {
        var EXIT = base.ExitInitializer(timescale, activate);
        EXIT.Append(panel.DOFade(0, outDuration).SetEase(outEase));

        if (activate)
        {
            EXIT.Play();
        }

        return EXIT;
    }












    void OnDestroy()
    {
        instance = null;
    }
}