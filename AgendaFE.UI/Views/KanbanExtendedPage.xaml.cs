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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaFE.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KanbanExtendedPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public KanbanExtendedPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
            DataContext = ViewModel;
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if (((e.OriginalSource as ScrollViewer)?.DataContext is MainViewModel targetAccount) && (e.OriginalSource as ScrollViewer).Name != null)
                if (await (e.DataView.GetDataAsync("ID")) is int taskId)
                {
                    try
                    {
                        var targetState = (e.OriginalSource as ScrollViewer)?.Name;
                        switch (targetState)
                        {
                            case "BacklogPanel":
                                ViewModel.ChangeTaskState(taskId, 3);
                                break;
                            case "TodoPanel":
                                ViewModel.ChangeTaskState(taskId, 0);
                                break;
                            case "DoingPanel":
                                ViewModel.ChangeTaskState(taskId, 2);
                                break;
                            case "TestingPanel":
                                ViewModel.ChangeTaskState(taskId, 4);
                                break;
                            case "DonePanel":
                                ViewModel.ChangeTaskState(taskId, 1);
                                break;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
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

        private void ItemsControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);

        }

        private void ItemsControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);

        }

    }
}