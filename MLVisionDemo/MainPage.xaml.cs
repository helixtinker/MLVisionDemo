using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TheSeeingPi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MLVisionDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            StartVideoPreviewAsync();
            LoadModelAsync();
        }


        private readonly DisplayRequest _displayRequest = new DisplayRequest();

        private readonly MediaCapture _mediaCapture = new MediaCapture();

        private async Task StartVideoPreviewAsync()
        {
            await _mediaCapture.InitializeAsync();
            _displayRequest.RequestActive();

            PreviewControl.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();
        }

        private string _modelFileName = "mycustomvision.onnx";

        private MyCustomVisionModel _model = null;

        private async Task LoadModelAsync()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => StatusText.Text = $"Loading {_modelFileName}");

            var modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/{_modelFileName}"));
            _model = await MyCustomVisionModel.CreateFromStreamAsync(modelFile);

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => StatusText.Text = $"Loaded {_modelFileName}");
        }

    }
}
