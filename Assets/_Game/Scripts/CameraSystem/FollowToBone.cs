using Spine;
using UnityEngine;
using Spine.Unity;

namespace _Game.Scripts.CameraSystem
{
    public class FollowToBone : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField, SpineBone] private string boneName;
        [SerializeField] private Transform _followTransform;
        
        private Bone _targetBone;
        
        private void Start()
        {
            if (skeletonAnimation == null)
                skeletonAnimation = GetComponent<SkeletonAnimation>();
            
            _targetBone = skeletonAnimation.Skeleton.FindBone(boneName);
        }
        
        private void Update()
        {
            if (_targetBone == null)
                return;
            
            Vector3 bonePos = new Vector3(_targetBone.WorldX, _targetBone.WorldY, 0);
            _followTransform.position = skeletonAnimation.transform.TransformPoint(bonePos);
        }
    }
}