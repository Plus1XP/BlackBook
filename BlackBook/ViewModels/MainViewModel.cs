using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BlackBook.Models;

namespace BlackBook.ViewModels
{
    class MainViewModel
    {
        public MainViewModel()
        {
            this.CloseWindowCommand = new Command<Window>(this.CloseWindow);
            this.MaximizeWindowCommand= new Command<Window>(this.MaximizeWindow);
            this.MinimizeWindowCommand = new Command<Window>(this.MinimizeWindow);
            this.RestoreWindowCommand = new Command<Window>(this.RestoreWindow);
        }

        public Command<Window> CloseWindowCommand { get; set; }
        public Command<Window> MaximizeWindowCommand { get; set; }
        public Command<Window> MinimizeWindowCommand { get; set; }
        public Command<Window> RestoreWindowCommand { get; set; }

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        private void MaximizeWindow(Window window)
        {
            if (window.ResizeMode == ResizeMode.CanResize || window.ResizeMode == ResizeMode.CanResizeWithGrip)
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private void MinimizeWindow(Window window)
        {
            if (window.ResizeMode != ResizeMode.NoResize)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void RestoreWindow(Window window)
        {
            if (window.ResizeMode == ResizeMode.CanResize || window.ResizeMode == ResizeMode.CanResizeWithGrip)
            {
                window.WindowState = WindowState.Normal;
            }
        }

        private bool CanResizeWindow(Window window)
        {
            return window.ResizeMode == ResizeMode.CanResize || window.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private bool CanMinimizeWindow(Window window)
        {
            return window.ResizeMode != ResizeMode.NoResize;
        }
    }
}
