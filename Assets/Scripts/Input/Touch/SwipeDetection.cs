using UnityEngine;

namespace Conffi.Input
{
    public sealed class SwipeDetection : MonoBehaviour
    {
        [SerializeField] private float minimuDistance = .2f;
        [SerializeField] private float maxTime = 1f;
        [SerializeField] private float directionThreshold = 0.9f;
        
        private TouchInput touchInput;

        private Vector2 startPosition;
        private float startTime;
        
        private Vector2 endPosition;
        private float endTime;
        
        private void Awake()
        {
            touchInput = TouchInput.Instance;
        }

        private void OnEnable()
        {
            touchInput.OnStartTouch += SwipeStart;
            touchInput.OnEndTouch += SwipeEnd;
        }
        
        private void OnDisable()
        {
            touchInput.OnStartTouch -= SwipeStart;
            touchInput.OnEndTouch -= SwipeEnd;
        }

        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
        }
        
        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;
            
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimuDistance && endTime - startTime <= maxTime)
            {
                Vector3 dir = endPosition - startPosition;
                Vector2 dir2D = new Vector2(dir.x, dir.y).normalized;
                
                SwipeDirection(dir2D);
            }
        }

        private void SwipeDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            {
                Debug.Log("up");
            }

            if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            {
                Debug.Log("down");
            }
            
            if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            {
                Debug.Log("left");
            }

            if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            {
                Debug.Log("right");
            }
        }
    }
}