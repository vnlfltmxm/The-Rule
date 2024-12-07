namespace Script.Prop
{
    public interface IUsable
    {
        bool IsUsable { get; set; }
        void Use();
    }
}