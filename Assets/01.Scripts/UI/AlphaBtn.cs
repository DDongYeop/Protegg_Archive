using UnityEngine;
using UnityEngine.UI;

public class AlphaBtn : MonoBehaviour 
{
    private float AlphaThreshold = 0.1f;

    private void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }
}

// 버튼에 넣으면 되고, 
// 이 스크립트는 버튼의 이미지에 맞춰 버튼의 크기가 맞춰지는 스크립트입니다. 
