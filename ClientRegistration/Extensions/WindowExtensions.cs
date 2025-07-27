namespace ClientRegistration.Extensions
{
    public static class WindowExtensions
    {
        public static void MaximizeWindow(this Window window)
        {
            window.Width = 1920;
            window.Height = 1080;
            window.X = 0;
            window.Y = 0;
            window.MaximumWidth = double.PositiveInfinity;
            window.MaximumHeight = double.PositiveInfinity;
        }

        public static void CenterWindow(this Window window, double width, double height)
        {
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            window.X = (displayInfo.Width / displayInfo.Density - width) / 2;
            window.Y = (displayInfo.Height / displayInfo.Density - height) / 2;
            window.Width = width;
            window.Height = height;
        }
    }
}