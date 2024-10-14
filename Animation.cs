using System;

namespace WindowsFormAnimation
{
    public class Animation
    {
        public int Duration { get; set; }
        public AnimationFlags Flags { get; set; }
        public string Name { get; set; }

        public event Action<Animation> AnimationStarted;
        public event Action<Animation> AnimationEnded;

        public void OnStarted() => AnimationStarted?.Invoke(this);
        public void OnEnded() => AnimationEnded?.Invoke(this);

        [Flags]
        public enum AnimationFlags
        {
            Show = 0X20000,
            Hide = 0X10000,
            Slide = 0X40000,
            Fade = 0x80000,
            Center = 0x10,
            RightToLeft = 0x2,
            LeftToRight = 0X1,
            TopToBottom = 0x4,
            BottomToTop = 0x8
        }
    }
}
