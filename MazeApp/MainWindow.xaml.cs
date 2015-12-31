using MazeLib;
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

namespace MazeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MazeGrid_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void DrawMaze(MazeSize size)
        {
            MazeGrid.Children.Clear();
            MazeGrid.RowDefinitions.Clear();
            MazeGrid.ColumnDefinitions.Clear();
            IMazeFactory mazefactory = MazeFactoryStratigy.GetMazeFactory(MazeStratigy.WidthFirst);
            Maze maze = mazefactory.BuildMaze(size);
            int mazeSize = maze.Size;
            double height = MazeGrid.Height / mazeSize;

            for (int i = 0; i < mazeSize; i++)
            {
                MazeGrid.RowDefinitions.Add(new RowDefinition());
                MazeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            DrawingBrush _gridBrush;
            _gridBrush = new DrawingBrush(new GeometryDrawing(
                new SolidColorBrush(Colors.Red),
                     new Pen(new SolidColorBrush(Colors.Black), 1.0),
                         new EllipseGeometry(new Rect(0, 0, height/2, height/2))));
            _gridBrush.Stretch = Stretch.None;
            _gridBrush.TileMode = TileMode.Tile;
            _gridBrush.Viewport = new Rect(0.0, 0.0, height, height);
            _gridBrush.ViewportUnits = BrushMappingMode.Absolute;

            DrawingBrush _gridBrushEndPoint;
            _gridBrushEndPoint = new DrawingBrush(new GeometryDrawing(
                new SolidColorBrush(Colors.Black),
                     new Pen(new SolidColorBrush(Colors.Black), 1.0),
                         new EllipseGeometry(new Rect(0, 0, height / 2, height / 2))));
            _gridBrushEndPoint.Stretch = Stretch.None;
            _gridBrushEndPoint.TileMode = TileMode.Tile;
            _gridBrushEndPoint.Viewport = new Rect(0.0, 0.0, height, height);
            _gridBrushEndPoint.ViewportUnits = BrushMappingMode.Absolute;
            int borderthickness =2;
            for (int i = 0; i < mazeSize; i++)
            {
                for (int j = 0; j < mazeSize; j++)
                {
                    Border bd = new Border();
                    bd.Background = Brushes.Black;
                    bd.BorderBrush = Brushes.Black;
                    if (i == 0 && j < mazeSize - 1)
                    {
                        bd.BorderThickness = new Thickness(borderthickness, borderthickness, 0, borderthickness);
                    }
                    else
                    {
                        bd.BorderThickness = new Thickness(borderthickness, 0, 0, borderthickness);
                    }

                    if (j == mazeSize - 1 && i > 0)
                    {
                        bd.BorderThickness = new Thickness(borderthickness, 0, borderthickness, borderthickness);
                    }
                    else if (i > 0)
                    {
                        bd.BorderThickness = new Thickness(borderthickness, 0, 0, borderthickness);
                    }

                    if (i == 0 && j == mazeSize - 1)
                    {
                        bd.BorderThickness = new Thickness(borderthickness);
                    }

                    Grid.SetColumn(bd, j);
                    Grid.SetRow(bd, i);

                    MazeGrid.Children.Add(bd);

                }
            }

            for (int row = 0; row < mazeSize; row++)
            {
                for (int col = 0; col < mazeSize; col++)
                {
                    var bd = GetGridBorder(row, col);
                    bd.Background = Brushes.White;

                    var nbs = maze.GetPassedNeighbors(new MazeCell() { X = row, Y = col });
                    for (int b = 0; b < nbs.Count; b++)
                    {
                        var border = GetGridBorder(nbs[b].X, nbs[b].Y);
                        border.Background = Brushes.White;
                        if (row == nbs[b].X && col < nbs[b].Y) //right
                        {
                            border.BorderThickness = new Thickness(0, border.BorderThickness.Top, border.BorderThickness.Right, border.BorderThickness.Bottom);
                            bd.BorderThickness = new Thickness(bd.BorderThickness.Left, bd.BorderThickness.Top, 0, bd.BorderThickness.Bottom);
                        }
                        if (row == nbs[b].X && col > nbs[b].Y) //Left
                        {
                            border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, 0, border.BorderThickness.Bottom);
                            bd.BorderThickness = new Thickness(0, bd.BorderThickness.Top, bd.BorderThickness.Right, bd.BorderThickness.Bottom);
                        }
                        if (row < nbs[b].X && col == nbs[b].Y) //bottom
                        {
                            border.BorderThickness = new Thickness(border.BorderThickness.Left, 0, border.BorderThickness.Right, border.BorderThickness.Bottom);
                            bd.BorderThickness = new Thickness(bd.BorderThickness.Left, bd.BorderThickness.Top, bd.BorderThickness.Right, 0);
                        }

                        if (row > nbs[b].X && col == nbs[b].Y)//top
                        {
                            border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, border.BorderThickness.Right, 0);
                            bd.BorderThickness = new Thickness(bd.BorderThickness.Left, 0, bd.BorderThickness.Right, bd.BorderThickness.Bottom);
                        }
                    }
                }
            }
            var startPoint = GetGridBorder(maze.StartPoint.X, maze.StartPoint.Y);
            startPoint.Background = _gridBrush;
            startPoint.BorderThickness = new Thickness(startPoint.BorderThickness.Left, 0, startPoint.BorderThickness.Right, startPoint.BorderThickness.Bottom);

            var exitPoint = GetGridBorder(maze.Size - 1, maze.Size - 1);
            exitPoint.Background = _gridBrushEndPoint;
            exitPoint.BorderThickness = new Thickness(exitPoint.BorderThickness.Left, exitPoint.BorderThickness.Top, exitPoint.BorderThickness.Right, 0);
        }

        private Border GetGridBorder(int row, int col)
        {
            var bd = MazeGrid.Children
       .Cast<UIElement>()
       .First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);

            return bd as Border;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawMaze(MazeSize.Small);
        }

        private void btnMedium_Click(object sender, RoutedEventArgs e)
        {
            DrawMaze(MazeSize.Medium);
        }
        
        private void btnLarge_Click(object sender, RoutedEventArgs e)
        {
            DrawMaze(MazeSize.Large);
        }

        private void btnXtraLarge_Click(object sender, RoutedEventArgs e)
        {
            DrawMaze(MazeSize.ExtraLarge);
        }
        //private 
        //private void docCanvas_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (_gridBrush == null)
        //    {
        //        double width = docCanvas.Width / 8;
        //        _gridBrush = new DrawingBrush(new GeometryDrawing(
        //            new SolidColorBrush(Colors.White),
        //                 new Pen(new SolidColorBrush(Colors.LightGray), 1.0),
        //                     new RectangleGeometry(new Rect(0, 0, width, width))));
        //        _gridBrush.Stretch = Stretch.None;
        //        _gridBrush.TileMode = TileMode.Tile;
        //        _gridBrush.Viewport = new Rect(0.0, 0.0, width, width);
        //        _gridBrush.ViewportUnits = BrushMappingMode.Absolute;
        //        docCanvas.Background = _gridBrush;
        //    }
        //}
    }
}
