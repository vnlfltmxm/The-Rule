using UnityEngine;

namespace Script.Prop
{
    public interface IInteractable
    {
        bool IsInteractable { get; set; }
        void OnInteract(IInteractor sender);
    }

    public interface IInteractableObject<T> : IInteractable 
        where T : class
    {
        void InteractObject(T type);
    }

    public interface IInteractableBloodMachine : IInteractableObject<PlayerBodyCondition> { }
    public interface IInteractableCafe : IInteractableObject<GameObject> { }    
}