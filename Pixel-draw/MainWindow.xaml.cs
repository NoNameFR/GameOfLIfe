using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameOfLife;

namespace Pixel_draw
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        static WriteableBitmap writeableBitmap;
        static Image i;
        static MainWindow w;

        static int ratio;
        GameOfLife.GameOfLife _game;

        public MainWindow()
        {
            InitializeComponent();

            i = new Image();
            RenderOptions.SetBitmapScalingMode(i, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(i, EdgeMode.Aliased);

            w = this;
            w.Content = i;
            w.Show();

            _game = new GameOfLife.GameOfLife();
            _game.Init();

            writeableBitmap = new WriteableBitmap(
                _game.Size,
                _game.Size,
                96,
                96,
                PixelFormats.Bgr32,
                null);

            i.Source = writeableBitmap;

            i.Stretch = Stretch.Uniform;
            i.HorizontalAlignment = HorizontalAlignment.Left;
            i.VerticalAlignment = VerticalAlignment.Top;

            Display(_game);
            this.KeyUp += MainWindow_KeyUp;
            //i.MouseMove += new MouseEventHandler(i_MouseMove);
            //i.MouseLeftButtonDown +=
            //    new MouseButtonEventHandler(i_MouseLeftButtonDown);
            //i.MouseRightButtonDown +=
            //    new MouseButtonEventHandler(i_MouseRightButtonDown);

            //w.MouseWheel += new MouseWheelEventHandler(w_MouseWheel);


        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            _game.ComputeNextTurn();
            Display(_game);
        }

        private void Display(GameOfLife.GameOfLife game)
        {
            for (int i = 0; i < game.Size; i++)
            {

                for (int j = 0; j < game.Size; j++)
                {
                    try
                    {
                        // Reserve the back buffer for updates.
                        writeableBitmap.Lock();

                        unsafe
                        {
                            // Get a pointer to the back buffer.
                            IntPtr pBackBuffer = writeableBitmap.BackBuffer;

                            // Find the address of the pixel to draw.
                            pBackBuffer += j * writeableBitmap.BackBufferStride;
                            pBackBuffer += i * 4;

                     

                            if (game.board[i, j])
                            {
                                // Compute the pixel's color.
                                int color_data = 0 << 16; // R
                                color_data |= 255 << 8;   // G
                                color_data |= 0 << 0;   // B
                                // Assign the color data to the pixel.
                                *((int*)pBackBuffer) = color_data;
                            }
                            else
                            {
                
                                // Assign the color data to the pixel.
                                *((int*)pBackBuffer) = 0;
                            }
                        }

                        // Specify the area of the bitmap that changed.
                        writeableBitmap.AddDirtyRect(new Int32Rect(i, j, 1, 1));
                    }
                    finally
                    {
                        // Release the back buffer and make it available for display.
                        writeableBitmap.Unlock();
                    }

                }
            }


        }

        //// The DrawPixel method updates the WriteableBitmap by using
        //// unsafe code to write a pixel into the back buffer.
        //static void DrawPixel(MouseEventArgs e)
        //{
        //    int column = (int)e.GetPosition(i).X;
        //    int row = (int)e.GetPosition(i).Y;

        //    try
        //    {
        //        // Reserve the back buffer for updates.
        //        writeableBitmap.Lock();

        //        unsafe
        //        {
        //            // Get a pointer to the back buffer.
        //            IntPtr pBackBuffer = writeableBitmap.BackBuffer;

        //            // Find the address of the pixel to draw.
        //            pBackBuffer += row * writeableBitmap.BackBufferStride;
        //            pBackBuffer += column * 4;

        //            // Compute the pixel's color.
        //            int color_data = 0 << 16; // R
        //            color_data |= 255 << 8;   // G
        //            color_data |= 0 << 0;   // B

        //            // Assign the color data to the pixel.
        //            *((int*)pBackBuffer) = color_data;
        //        }

        //        // Specify the area of the bitmap that changed.
        //        writeableBitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
        //    }
        //    finally
        //    {
        //        // Release the back buffer and make it available for display.
        //        writeableBitmap.Unlock();
        //    }
        //}

        //static void ErasePixel(MouseEventArgs e)
        //{
        //    byte[] ColorData = { 0, 0, 0, 0 }; // B G R

        //    Int32Rect rect = new Int32Rect(
        //            (int)(e.GetPosition(i).X),
        //            (int)(e.GetPosition(i).Y),
        //            1,
        //            1);

        //    writeableBitmap.WritePixels(rect, ColorData, 4, 0);
        //}

        //static void i_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    ErasePixel(e);
        //}

        //static void i_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    DrawPixel(e);
        //}

        //static void i_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        DrawPixel(e);
        //    }
        //    else if (e.RightButton == MouseButtonState.Pressed)
        //    {
        //        ErasePixel(e);
        //    }
        //}

        //static void w_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    System.Windows.Media.Matrix m = i.RenderTransform.Value;

        //    if (e.Delta > 0)
        //    {
        //        m.ScaleAt(
        //            1.5,
        //            1.5,
        //            e.GetPosition(w).X,
        //            e.GetPosition(w).Y);
        //    }
        //    else
        //    {
        //        m.ScaleAt(
        //            1.0 / 1.5,
        //            1.0 / 1.5,
        //            e.GetPosition(w).X,
        //            e.GetPosition(w).Y);
        //    }

        //    i.RenderTransform = new MatrixTransform(m);
        //}
    }
}
