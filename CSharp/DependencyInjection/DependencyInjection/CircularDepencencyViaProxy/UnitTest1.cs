using System.Reflection.Metadata.Ecma335;
using NUnit.Framework;

namespace CircularDepencencyViaProxy
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            //this doesn't compile
            //var window = new Window(windowManager);
            //var windowManager = new WindowManager(window);
        }

        [Test]
        public void Test2()
        {
            var windowProxy = new WindowProxy();
            var windowManager = new WindowManager(windowProxy);
            var window = new Window(windowManager);
            windowProxy.Inner = window;

            Assert.AreEqual(123, windowManager.GetWindowColor());
            Assert.AreEqual(321, window.GetManagerStatus());
        }
    }

    internal interface IWindowManager
    {
        public int GetWindowColor();
        int GetStatus();
    }

    internal interface IWindow
    {
        public int GetColor();
    }

    internal record WindowManager(IWindow Window) : IWindowManager
    {
        public int GetWindowColor()
        {
            return Window.GetColor();
        }

        public int GetStatus()
        {
            return 321;
        }
    }

    internal record Window(IWindowManager WindowManager) : IWindow
    {
        public int GetColor()
        {
            return 123;
        }
        public int GetManagerStatus()
        {
            return WindowManager.GetStatus();
        }
    }

    /// <summary>
    /// This is purely infrastructural code.
    /// </summary>
    public class WindowProxy : IWindow
    {
        internal IWindow Inner { get; set; }

        public int GetColor() => Inner.GetColor();
    }
}