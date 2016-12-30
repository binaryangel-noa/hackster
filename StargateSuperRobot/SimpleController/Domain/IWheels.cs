namespace SimpleController.Domain
{
    internal interface IWheels
    {
        bool IsMovingForward { get; }

        void Init();

        void MoveBackwards();

        void MoveForward();

        void Stop();

        void TurnLeft();

        void TurnRight();
    }
}