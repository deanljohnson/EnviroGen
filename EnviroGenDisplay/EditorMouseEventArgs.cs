using System;

namespace EnviroGenDisplay
{
    public enum EditorMouseButton
    {
        Left,
        Right,
        None
    }

    public enum EditorMouseButtonState
    {
        Up,
        Down,
        None
    }

    public class EditorMouseEventArgs : EventArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public EditorMouseButton Button { get; private set; }
        public EditorMouseButtonState ButtonState { get; private set; }

        public EditorMouseEventArgs(double x, double y, EditorMouseButton button, EditorMouseButtonState buttonState)
        {
            X = x;
            Y = y;
            Button = button;
            ButtonState = buttonState;
        }
    }
}
