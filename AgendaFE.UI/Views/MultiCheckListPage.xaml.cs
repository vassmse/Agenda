﻿using AgendaContracts.Models;
using AgendaFE.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class MultiCheckListPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public MultiCheckListPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
            DataContext = ViewModel;

        }

        private void ItemsControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);

        }

        private void ItemsControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);

        }

        private void TaskReady(object sender, RoutedEventArgs e)
        {
            TaskDto source = ((CheckBox)sender).DataContext as TaskDto;
            ViewModel.CheckChangedAction(source);
        }
    }


}
