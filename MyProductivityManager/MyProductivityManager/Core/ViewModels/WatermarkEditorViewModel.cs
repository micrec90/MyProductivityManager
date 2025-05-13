using Microsoft.Win32;
using MyProductivityManager.Core.Models.ProjectTasks;
using MyProductivityManager.Core.Models.WatermarkEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyProductivityManager.Core.ViewModels
{
    public class WatermarkEditorViewModel : ViewModel
    {
        private bool _useImageWatermark = false;
        public bool UseImageWatermark
        {
            get { return _useImageWatermark; }
            set
            {
                _useImageWatermark = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UseTextWatermark)); // won't update without this
            }
        }
        public bool UseTextWatermark => !UseImageWatermark; // needs one-way binding
        private string _watermarkText = string.Empty;
        public string WatermarkText
        {
            get { return _watermarkText; }
            set
            {
                _watermarkText = value;
                OnPropertyChanged();
            }
        }
        private int _fontSize = 24;
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }
        private ImageSource _watermarkImage;
        public ImageSource WatermarkImage
        {
            get { return _watermarkImage; }
            set
            {
                _watermarkImage = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<WatermarkPosition> WatermarkPositionValues { get; set; }
        private WatermarkPosition _watermarkPosition;
        public WatermarkPosition WatermarkPosition
        {
            get { return _watermarkPosition; }
            set
            {
                _watermarkPosition = value;
                OnPropertyChanged();
            }
        }
        private float _opacity = 0.5f;
        public float Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OnPropertyChanged();
            }
        }
        private string _imagePath;
        private ImageItem _previewImage;
        public ImageItem PreviewImage
        {
            get { return _previewImage; }
            set
            {
                _previewImage = value;
                OnPropertyChanged();
            }
        }
        private string[] _imagePaths;
        public ObservableCollection<ImageItem> ImportedImages { get; set; } = new ObservableCollection<ImageItem>();
        private List<ImageSource> _selectedImages;
        public List<ImageSource> SelectedImages
        {
            get { return _selectedImages; }
            set
            {
                _selectedImages = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand LoadImageCommand { get; set; }
        public RelayCommand ApplyWatermarkCommand { get; set; }
        public RelayCommand ApplyWatermarkToAllCommand { get; set; }
        public RelayCommand ExportImageCommand { get; set; }
        public RelayCommand LoadWatermarkImageCommand { get; set; }
        public RelayCommand RemoveImageCommand { get; set; }
        public RelayCommand ClearAllCommand { get; set; }
        public WatermarkEditorViewModel()
        {
            WatermarkPositionValues = new ObservableCollection<WatermarkPosition>(Enum.GetValues(typeof(WatermarkPosition)).Cast<WatermarkPosition>());

            LoadImageCommand = new RelayCommand(obj => { LoadImages(); }, obj => true);
            ApplyWatermarkCommand = new RelayCommand(async obj => { await ApplyWatermarkAsync(obj); }, obj => true);
            ExportImageCommand = new RelayCommand(obj => { ExportImages(obj); }, obj => true);
            LoadWatermarkImageCommand = new RelayCommand(obj => { LoadWatermarkImage(); }, obj => true);
            RemoveImageCommand = new RelayCommand(obj => { RemoveImage(obj); }, obj => true);
            ClearAllCommand = new RelayCommand(obj => { ClearAllImages(); }, obj => ImportedImages.Count > 0);
        }
        private void RemoveImage(object obj)
        {
            ImportedImages.Remove((ImageItem) obj);
        }
        private void ClearAllImages()
        {
            ImportedImages.Clear();
        }
        private void LoadWatermarkImage()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp",
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                WatermarkImage = new BitmapImage(new Uri(dialog.FileName));
                UseImageWatermark = true;
            }
        }
        private void LoadImages()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp",
                Multiselect = true
            };

            if (dialog.ShowDialog() == true)
            {
                _imagePaths = dialog.FileNames;
                //SelectedImages = new List<ImageSource>();
                for (int i = 0; i < _imagePaths.Length; i++)
                {
                    ImageSource imageSource = new BitmapImage(new Uri(_imagePaths[i]));
                    ImageItem imageItem = new ImageItem()
                    {
                        Source = imageSource,
                        Path = _imagePaths[i],
                        FileName = System.IO.Path.GetFileName(_imagePaths[i])
                    };
                    ImportedImages.Add(imageItem);
                }
                if (ImportedImages.Count > 0)
                    PreviewImage = ImportedImages.Last();
            }
        }
        private async Task ApplyWatermarkAsync(object param)
        {
            if (PreviewImage == null)
                return;
            if (param == null)
                await ApplyWatermartToImageAsync(PreviewImage);
            else
            {
                foreach (var im in ImportedImages)
                {
                    await ApplyWatermartToImageAsync(im);
                }
            }
        }
        private async Task ApplyWatermartToImageAsync(ImageItem imageItem)
        {
            if (string.IsNullOrEmpty(imageItem.Path))
                return;
            var original = new BitmapImage(new Uri(imageItem.Path));

            int width = original.PixelWidth;
            int height = original.PixelHeight;
            var visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                // draw original image
                context.DrawImage(original, new Rect(0, 0, width, height));
                if (UseImageWatermark && WatermarkImage is BitmapSource watermark)
                {
                    double scaleFactor = 0.25;
                    double wmWidth = width * scaleFactor;
                    double wmHeight = watermark.PixelHeight * (wmWidth / watermark.PixelWidth);

                    Point position = GetWatermarkPosition(WatermarkPosition, width, height, wmWidth, wmHeight);
                    var rect = new Rect(position, new Size(wmWidth, wmHeight));

                    var brush = new ImageBrush(watermark) { Opacity = Opacity };
                    context.DrawRectangle(brush, null, rect);
                }
                else
                {
                    var formattedText = new FormattedText(
                        WatermarkText,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        FontSize,
                        Brushes.White,
                        VisualTreeHelper.GetDpi(Application.Current.MainWindow).PixelsPerDip);
                    formattedText.TextAlignment = TextAlignment.Left;
                    Point position = GetWatermarkPosition(WatermarkPosition, width, height, formattedText.Width, formattedText.Height);

                    // Apply opacity
                    var brush = new SolidColorBrush(Colors.White) { Opacity = Opacity };

                    context.DrawText(formattedText, position);
                }
            }
            var bmp = new RenderTargetBitmap(width, height, original.DpiX, original.DpiY, PixelFormats.Pbgra32);
            bmp.Render(visual);
            imageItem.Source = bmp;
        }
        private Point GetWatermarkPosition(WatermarkPosition position, int imageWidth, int imageHeight, double textWidth, double textHeight)
        {
            const int padding = 20;

            switch(position)
            {
                case WatermarkPosition.TopLeft:
                    return new Point(padding, padding);
                case WatermarkPosition.Top:
                    return new Point((imageWidth - textWidth) / 2, padding);
                case WatermarkPosition.TopRight:
                    return new Point(imageWidth - textWidth - padding, padding);
                case WatermarkPosition.Left:
                    return new Point(padding, (imageHeight - textHeight) / 2);
                case WatermarkPosition.Middle:
                    return new Point((imageWidth - textWidth) / 2, (imageHeight - textHeight) / 2);
                case WatermarkPosition.Right:
                    return new Point(imageWidth - textWidth - padding, (imageHeight - textHeight) / 2);
                case WatermarkPosition.BottomLeft:
                    return new Point(padding, imageHeight - textHeight - padding);
                case WatermarkPosition.Bottom:
                    return new Point((imageWidth - textWidth) / 2, imageHeight - textHeight - padding);
                case WatermarkPosition.BottomRight:
                    return new Point(imageWidth - textWidth - padding, imageHeight - textHeight - padding);
                default:
                    return new Point(padding, padding);

            }
        }
        private void ExportImages(object param)
        {
            if (PreviewImage == null)
                return;
            if (param == null)
                ExportImage(PreviewImage.Source);
            else
                ExportAllImagesToZip();
        }
        private void ExportImage(ImageSource imageSource)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                FileName = "watermarked.png"
            };

            if (dialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource) imageSource));

                using (var stream = new FileStream(dialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }

                MessageBox.Show("Image exported successfully!", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ExportAllImagesToZip()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "ZIP Archive|*.zip",
                FileName = $"Watermarked_{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip"
            };
            if (dialog.ShowDialog() == true)
            {
                using (var zipToCreate = new FileStream(dialog.FileName, FileMode.Create))
                {
                    using (var archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                    {
                        for (int i = 0; i < ImportedImages.Count; i++)
                        {
                            var imageSource = ImportedImages[i].Source;

                            using (var imageStream = new MemoryStream())
                            {
                                var encoder = new PngBitmapEncoder();
                                encoder.Frames.Add(BitmapFrame.Create((BitmapSource) imageSource));
                                encoder.Save(imageStream);
                                imageStream.Position = 0;

                                var zipEntry = archive.CreateEntry(ImportedImages[i].FileName);

                                using (var entryStream = zipEntry.Open())
                                {
                                    imageStream.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Images exported to ZIP successfully!", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
