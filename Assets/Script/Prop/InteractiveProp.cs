namespace Script.Prop
{
    public abstract class InteractiveProp : Prop, IInteractable
    {
        public bool IsInteractable { get; set; } = true;
        public abstract void Interact();
    }
}