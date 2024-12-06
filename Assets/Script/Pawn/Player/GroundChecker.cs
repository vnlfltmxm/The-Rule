using UnityEngine;

namespace Script.Pawn.Player
{
    public class GroundChecker : MonoBehaviour
    {
        [Header("Boxcast Property")]
        [SerializeField] private Vector3 boxSize;
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask groundLayer;

        [Header("Debug")]
        [SerializeField] private bool drawGizmo;

        public Vector3 GroundNormalVector
        {
            get;
            set;
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmo) return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
        }

        public bool IsGrounded()
        {
            return Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, groundLayer);
        }

    }
}