using System.Windows;
using System.Windows.Media;

namespace ActivityAnalysis.WPF.Styles.NavRadioBar
{
    public class NavRadioImage
    {
        public static readonly DependencyProperty _image;
        
        public static ImageSource GetImage(DependencyObject obj) => (ImageSource) obj.GetValue(_image);
        
        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(_image, value);
        }

        static NavRadioImage()
        { 
            var metadata = new FrameworkPropertyMetadata((ImageSource) null);
            _image = DependencyProperty.RegisterAttached("Image", typeof (ImageSource), typeof (NavRadioImage), metadata);
        }
    }
}