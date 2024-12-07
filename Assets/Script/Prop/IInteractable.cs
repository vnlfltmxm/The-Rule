namespace Script.Prop
{
    public interface IInteractable
    {
        bool IsInteractable { get; set; }
        void Interact();
    }
}