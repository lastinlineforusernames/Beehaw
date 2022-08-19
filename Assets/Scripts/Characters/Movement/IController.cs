namespace Beehaw.Character
{
    public interface IController
    {
        float GetHorizontalInput();
        float GetVerticalInput();
        bool IsJumpButtonPressed();
        bool IsJumpButtonReleased();
        bool ShouldFirePrimary();
        bool ShouldFireSecondary();
    }
}