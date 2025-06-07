using Script.Prop;
using UnityEngine;

namespace Script.Prop
{
    public interface IInteractor
    {
        public GameObject GameObject { get; }
        public void Interact();
    }
}