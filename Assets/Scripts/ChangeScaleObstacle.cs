using UnityEngine;
using DG.Tweening;

public class ChangeScaleObstacle : MonoBehaviour{

    [SerializeField] private Vector3 scale = new Vector3(1,1.5f,1);
    [SerializeField] private float duration = 2f;
    [SerializeField] private float delay = 0f;

    void Start(){
        transform.DOScale(scale, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(delay);      
    }
}
