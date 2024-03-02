using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AnimatorGachapon : MonoBehaviour
{
    [SerializeField] private GameObject _gachapon;
    private Script_Gachapon gachapon;
    private void Start()
    {
        gachapon = _gachapon.GetComponent<Script_Gachapon>();
    }
    public void End()
    {
        gachapon.EndAnimation();
    }
}
