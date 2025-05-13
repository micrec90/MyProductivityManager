using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyProductivityManager.Core.Models.WatermarkEditor
{
    public class ImageItem : ObservableObject
    {
        private ImageSource _source;
        public ImageSource Source
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged();
            }
        }
        public string Path { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
