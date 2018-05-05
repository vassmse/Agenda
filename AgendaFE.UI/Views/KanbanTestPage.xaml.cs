using AgendaContracts.Models;
using AgendaFE.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaFE.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KanbanTestPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public KanbanTestPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
            DataContext = ViewModel;
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if ((e.OriginalSource as Grid)?.DataContext is MainViewModel targetAccount)
                if (await (e.DataView.GetDataAsync("ID")) is int sourceAccountId)
                {
                    var sourceAccount = sourceAccountId;
                }
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
            e.DragUIOverride.IsGlyphVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsCaptionVisible = true;
        }

        private void TextBlock_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            if ((sender as StackPanel)?.DataContext is TaskDto task)
            {
                args.AllowedOperations = DataPackageOperation.Move;
                args.Data.SetData("ID", task.Id);
            }
        }
    }
}
