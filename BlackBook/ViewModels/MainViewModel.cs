using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool canResizeWindow { get; set; } = true;
        private bool canMinimizeWindow { get; set; } = true;

        public MainViewModel()
        {
            this.CloseWindowCommand = new Command<Window>(this.CloseWindow);
            this.MaximizeWindowCommand = new Command<Window>(this.MaximizeWindow, o => this.canResizeWindow);
            this.MinimizeWindowCommand = new Command<Window>(this.MinimizeWindow, o => this.canMinimizeWindow);
            this.RestoreWindowCommand = new Command<Window>(this.RestoreWindow, o => this.canResizeWindow);
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
            if (this.CanResizeWindow(window))
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private void MinimizeWindow(Window window)
        {
            if (this.CanMinimizeWindow(window))
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void RestoreWindow(Window window)
        {
            if (this.CanResizeWindow(window))
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
