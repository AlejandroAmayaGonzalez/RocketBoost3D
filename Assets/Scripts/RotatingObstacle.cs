using UnityEngine;
using DG.Tweening;

public class RotatingObstacle : MonoBehaviour {
    [SerializeField] private Vector3 movement = new Vector3(0,5f,0);
    [SerializeField] private float duration = 2f;

    void Start(){
        transform.DOLocalRotate(new Vector3(0,360,0), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
