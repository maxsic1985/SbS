using UnityEngine;



namespace MSuhinin.Clock
{
    public sealed class TransformView : BaseView
    {
        [SerializeField] private Transform _transform;

        public Transform Transform => _transform;
    }
}