using System;
using System.Runtime.InteropServices;

namespace WindowsFormAnimation
{
    public class Animator
    {
        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        public event Action<Animation> AnimationStarted;
        public event Action<Animation> AnimationEnded;

        public void Play(IntPtr hwnd, Animation animation)
        {
            OnStarted(animation); animation.OnStarted();
            AnimateWindow(hwnd, animation.Duration, (int)animation.Flags);
            animation.OnEnded(); OnEnded(animation);
        }
        
        protected virtual void OnStarted(Animation animation) => AnimationStarted?.Invoke(animation);
        protected virtual void OnEnded(Animation animation) => AnimationEnded?.Invoke(animation);
    }
}
