using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using GeometryApp.Shapes;

namespace GeometryApp
{
    public partial class Form1 : Form
    {
        private Panel leftPanel;
        private Panel rightPanel;
        private Panel bottomPanel;
        private Panel canvasPanel;
        private ComboBox shapeSelector;

        private Label coordinatesLabel;
        private Button undoButton;
        private Button clearButton;
        private ListBox shapesListBox;
        private Button deleteButton;
        private Button applyButton;
        private TextBox x1Input;
        private TextBox y1Input;
        private TextBox x2Input;
        private TextBox y2Input;
        private TextBox radius1Input;
        private TextBox radius2Input;
        private TextBox sideInput;
        private TextBox widthInput;
        private TextBox heightInput;
        private CheckBox fillCheckBox;
        private CheckBox borderCheckBox;
        private ComboBox colorСomboBox;
        private ComboBox borderColorComboBox;
        private TextBox borderSizeInput;
        private TrackBar redInput;
        private TrackBar greenInput;
        private TrackBar blueInput;

        private TrackBar redBorderInput;
        private TrackBar greenBorderInput;
        private TrackBar blueBorderInput;
        
        


        private List<object> shapes = new List<object>();

        private int? startX = null, startY = null;
        private int? rectX1 = null, rectY1 = null;
        private int? ellipseX1 = null, ellipseY1 = null;
        private int? circleX1 = null, circleY1 = null;
        private int? squareX1 = null, squareY1 = null;

        private Dictionary<string, Color> colorMapping;

        public Form1()
        {
            InitializeComponent();
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            this.Text = "Geometry App";
            this.Size = new Size(1024, 720);

            leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200
            };
            this.Controls.Add(leftPanel);

            rightPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 200
            };
            this.Controls.Add(rightPanel);

            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100
            };
            this.Controls.Add(bottomPanel);

            canvasPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(canvasPanel);


            shapeSelector = new ComboBox
            {
                Dock = DockStyle.Top
            };
            shapeSelector.Items.AddRange(new string[]
            {
                "Точка",
                "Отрезок",
                "Прямоугольник",
                "Эллипс",
                "Круг",
                "Квадрат",
                "Полигон",
                "Правильный N-угольник",
                "Звезда"
            });
            leftPanel.Controls.Add(shapeSelector);

            undoButton = new Button
            {
                Text = "Убрать последнюю",
                Dock = DockStyle.Top,
                Height = 30
            };
            undoButton.Click += UndoButton_Click;
            rightPanel.Controls.Add(undoButton);

            TableLayoutPanel bottomLayout = new TableLayoutPanel
            {
                ColumnCount = 20,
                RowCount = 2,
                Dock = DockStyle.Left,
                AutoSize = true
            };


            x1Input = new TextBox { Name = "x1Input", Width = 50, PlaceholderText = "X1", Visible = false };
            y1Input = new TextBox { Name = "y1Input", Width = 50, PlaceholderText = "Y1", Visible = false };
            x2Input = new TextBox { Name = "x2Input", Width = 50, PlaceholderText = "X2", Visible = false };
            y2Input = new TextBox { Name = "y2Input", Width = 50, PlaceholderText = "Y2", Visible = false };
            

            radius1Input = new TextBox { Name = "radius1Input", Width = 50, PlaceholderText = "Radius", Visible = false };
            sideInput = new TextBox { Name = "sideInput", Width = 50, PlaceholderText = "Side", Visible = false };
            widthInput = new TextBox { Name = "widthInput", Width = 50, PlaceholderText = "Width", Visible = false };
            heightInput = new TextBox { Name = "heightInput", Width = 50, PlaceholderText = "Height", Visible = false };
            radius2Input = new TextBox { Name = "radius2Input", Width = 50, PlaceholderText = "Radius", Visible = false };

            fillCheckBox = new CheckBox { Text = "Заливка", Visible = false  };
            colorСomboBox = new ComboBox { Visible = false};
            colorСomboBox.SelectedIndexChanged += ColorСomboBox_Changed;
            borderCheckBox = new CheckBox { Text = "Обводка", Visible = false };
            borderCheckBox.Click += BorderCheckBox_Click;
            borderSizeInput = new TextBox { Name = "borderSizeInput", Width = 50, PlaceholderText = "Раземер обводки", Visible = false, Text="3" };
            borderColorComboBox = new ComboBox { Visible = false };
            borderColorComboBox.SelectedIndexChanged += BorderColorComboBox_Changed;
            

            redInput = new TrackBar { Name = "redInput", Width = 75,  Visible = false, Maximum = 255, Minimum = 0, Value = 125 };
            greenInput = new TrackBar { Name = "greenInput", Width = 75, Visible = false, Maximum = 255, Minimum = 0, Value = 125 };
            blueInput = new TrackBar { Name = "blueInput", Width = 75,  Visible = false, Maximum = 255, Minimum = 0, Value = 125 };


            redBorderInput = new TrackBar { Name = "redBorderInput", Width = 50, Visible = false, Maximum = 255, Minimum = 0, Value = 125 };
            greenBorderInput = new TrackBar { Name = "greenBorderInput", Width = 50, Visible = false, Maximum = 255, Minimum = 0, Value = 125 };
            blueBorderInput = new TrackBar { Name = "blueBorderInput", Width = 50, Visible = false, Maximum = 255, Minimum = 0, Value = 125 };

            colorMapping = new Dictionary<string, Color>
            {
                { "Красный", Color.Red },
                { "Оранжевый", Color.Orange },
                { "Желтый", Color.Yellow },
                { "Зеленый", Color.Green },
                { "Голубой", Color.LightBlue },
                { "Синий", Color.Blue },
                { "Фиолетовый", Color.Purple },
                { "Розовый", Color.Pink },
                { "Белый", Color.White },
                { "Чёрный", Color.Black },
                { "Бюрюзовый", Color.Turquoise },
                { "Светло-серый", Color.LightGray },
                { "Тёмно-зелёный", Color.DarkGreen },
                { "Тёмно-серый", Color.DarkGray },
                { "Коричневый", Color.Brown },
                { "Собственный", Color.Transparent }
            };

            colorСomboBox.Items.AddRange(colorMapping.Keys.ToArray());
            borderColorComboBox.Items.AddRange(colorMapping.Keys.ToArray());

            borderColorComboBox.SelectedIndex = 9;
            colorСomboBox.SelectedIndex = 0;



            applyButton = new Button
            {
                Text = "Применить",
                Size = new Size(100, 30),
                Visible = false,
                Dock = DockStyle.Bottom
            };
            applyButton.Click += ApplyButton_Click;
            

            coordinatesLabel = new Label
            {
                Dock = DockStyle.Top,
                Text = "Координаты: (0, 0)",
                AutoSize = true
            };
            

            clearButton = new Button
            {
                Text = "Очистить холст",
                Dock = DockStyle.Top,
                Height = 30
            };
            clearButton.Click += ClearButton_Click;
            

            shapesListBox = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 200
            };
            shapesListBox.SelectedIndexChanged += ShapesListBox_SelectedIndexChanged;
            

            deleteButton = new Button
            {
                Text = "Удалить",
                Dock = DockStyle.Top,
                Height = 30
            };
            deleteButton.Click += DeleteButton_Click;


            leftPanel.Controls.Add(applyButton);
            bottomPanel.Controls.Add(bottomLayout);
            leftPanel.Controls.Add(coordinatesLabel);
            rightPanel.Controls.Add(shapesListBox);
            rightPanel.Controls.Add(clearButton);
            rightPanel.Controls.Add(deleteButton);
            bottomLayout.Controls.Add(colorСomboBox);
            bottomLayout.Controls.Add(redInput);
            bottomLayout.Controls.Add(greenInput);
            bottomLayout.Controls.Add(blueInput); 
            bottomLayout.Controls.Add(x1Input);
            bottomLayout.Controls.Add(y1Input);
            bottomLayout.Controls.Add(x2Input);
            bottomLayout.Controls.Add(y2Input);
            bottomLayout.Controls.Add(radius1Input);
            bottomLayout.Controls.Add(sideInput);
            bottomLayout.Controls.Add(widthInput);
            bottomLayout.Controls.Add(heightInput);
            bottomLayout.Controls.Add(radius2Input);
            bottomLayout.Controls.Add(fillCheckBox);
            bottomLayout.Controls.Add(borderCheckBox);
            bottomLayout.Controls.Add(borderColorComboBox);
            bottomLayout.Controls.Add(borderSizeInput);
            bottomLayout.Controls.Add(redBorderInput);
            bottomLayout.Controls.Add(greenBorderInput);
            bottomLayout.Controls.Add(blueBorderInput);
            

            canvasPanel.MouseDown += CanvasPanel_MouseDown;
            canvasPanel.MouseMove += CanvasPanel_MouseMove;
        }

        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            coordinatesLabel.Text = $"Координаты: ({e.X}, {e.Y})";
        }


        private void ColorСomboBox_Changed(object sender, EventArgs e)
        {
            if (colorСomboBox.SelectedItem != null)
            {
                string selectedColor = colorСomboBox.SelectedItem.ToString();
                if (selectedColor == "Собственный")
                {
                    redInput.Visible = true;
                    greenInput.Visible = true;
                    blueInput.Visible = true;
                }

                else
                {
                    redInput.Visible = false;
                    greenInput.Visible = false;
                    blueInput.Visible = false;
                }
            }
        
        }


        private void BorderCheckBox_Click(object sender, EventArgs e)
        {
            if (borderCheckBox.Checked)
            {
                borderSizeInput.Visible = true;
                borderColorComboBox.Visible = true;
            }
            else
            {
                borderSizeInput.Visible = false;
                borderColorComboBox.Visible = false;
                redBorderInput.Visible = false;
                greenBorderInput.Visible = false;
                blueBorderInput.Visible = false;
            }
        }



        private void BorderColorComboBox_Changed(object sender, EventArgs e)
        {
            if (borderColorComboBox.SelectedItem != null) {

                string selectedColor = borderColorComboBox.SelectedItem.ToString();
                if (selectedColor == "Собственный") {
                    redBorderInput.Visible = true;
                    greenBorderInput.Visible = true;
                    blueBorderInput.Visible = true;
                }

                else {
                    redBorderInput.Visible = false;
                    greenBorderInput.Visible = false;
                    blueBorderInput.Visible = false;
                }

            }
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (shapes.Count > 0)
            {
                shapes.RemoveAt(shapes.Count - 1);
                shapesListBox.Items.RemoveAt(shapesListBox.Items.Count - 1);
                RedrawCanvas();
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (shapesListBox.SelectedItem != null)
                {
                    int index = shapesListBox.SelectedIndex;
                    if (index >= 0 && index < shapes.Count)
                    {
                        var shape = shapes[index];

                        if (int.TryParse(x1Input.Text, out int x1) && int.TryParse(y1Input.Text, out int y1))
                        {
                            if (shape is MyPoint point)
                            {
                                point.Move(x1, y1);

                                if (redInput.Visible)
                                {
                                    point.ChangeColor(Color.FromArgb(redInput.Value, greenInput.Value, blueInput.Value));
                                }
                                else
                                {
                                    point.ChangeColor(colorMapping[colorСomboBox.SelectedItem.ToString()]);
                                }

                            }
                            else if (shape is Segment segment)
                            {
                                if (int.TryParse(x2Input.Text, out int x2) && int.TryParse(y2Input.Text, out int y2))
                                {
                                    segment.Move(x1, y1, x2, y2);

                                    if (redInput.Visible)
                                    {
                                        segment.ChangeColor(Color.FromArgb(redInput.Value, greenInput.Value, blueInput.Value));
                                    }
                                    else
                                    {
                                        segment.ChangeColor(colorMapping[colorСomboBox.SelectedItem.ToString()]);
                                    }

                                    segment.SetWidth(int.Parse(widthInput.Text));

                                }
                                else
                                {
                                    MessageBox.Show("Invalid input for segment coordinates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (shape is MyRectangle rectangle)
                            {
                                rectangle.Move(x1, y1);

                                if (redInput.Visible)
                                {
                                    rectangle.ChangeColor(Color.FromArgb(redInput.Value, greenInput.Value, blueInput.Value));
                                }
                                else
                                {
                                    rectangle.ChangeColor(colorMapping[colorСomboBox.SelectedItem.ToString()]);
                                }

                                if (fillCheckBox.Checked)
                                {
                                    rectangle.Fill();
                                }
                                else
                                {
                                    rectangle.Unfill();
                                }

                                if (borderCheckBox.Checked)
                                {
                                    rectangle.SetBorder(int.Parse(borderSizeInput.Text), Color.Red);
                                    if (redBorderInput.Visible)
                                    {
                                        rectangle.ChangeBorderColor(Color.FromArgb(redBorderInput.Value, greenBorderInput.Value, blueBorderInput.Value));
                                    }
                                    else
                                    {
                                        rectangle.ChangeBorderColor(colorMapping[borderColorComboBox.SelectedItem.ToString()]);
                                    }
                                }

                                rectangle.SetWidth(int.Parse(widthInput.Text));
                                rectangle.SetHeight(int.Parse(heightInput.Text));

                            }
                            else if (shape is MyEllipse ellipse)
                            {
                                ellipse.Move(x1, y1);

                                if (redInput.Visible)
                                {
                                    ellipse.ChangeColor(Color.FromArgb(redInput.Value, greenInput.Value, blueInput.Value));
                                }
                                else
                                {
                                    ellipse.ChangeColor(colorMapping[colorСomboBox.SelectedItem.ToString()]);
                                }

                                if (fillCheckBox.Checked)
                                {
                                    ellipse.Fill();
                                }
                                else
                                {
                                    ellipse.Unfill();
                                }

                                if (borderCheckBox.Checked)
                                {
                                    ellipse.SetBorder(int.Parse(borderSizeInput.Text), Color.Red);
                                    if (redBorderInput.Visible)
                                    {
                                        ellipse.ChangeBorderColor(Color.FromArgb(redBorderInput.Value, greenBorderInput.Value, blueBorderInput.Value));
                                    }
                                    else
                                    {
                                        ellipse.ChangeBorderColor(colorMapping[borderColorComboBox.SelectedItem.ToString()]);
                                    }
                                }

                                ellipse.SetWidth(int.Parse(widthInput.Text));
                                ellipse.SetHeight(int.Parse(heightInput.Text));
                            }
                            else if (shape is MyCircle circle)
                            {
                                circle.Move(x1, y1);

                                if (redInput.Visible)
                                {
                                    circle.ChangeColor(Color.FromArgb(redInput.Value, greenInput.Value, blueInput.Value));
                                }
                                else
                                {
                                    circle.ChangeColor(colorMapping[colorСomboBox.SelectedItem.ToString()]);
                                }

                                if (fillCheckBox.Checked)
                                {
                                    circle.Fill();
                                }
                                else
                                {
                                    circle.Unfill();
                                }

                                if (borderCheckBox.Checked)
                                {
                                    circle.SetBorder(int.Parse(borderSizeInput.Text), Color.Red);

                                    if (redBorderInput.Visible)
                                    {
                                        circle.ChangeBorderColor(Color.FromArgb(redBorderInput.Value, greenBorderInput.Value, blueBorderInput.Value));
                                    }
                                    else
                                    {
                                        circle.ChangeBorderColor(colorMapping[borderColorComboBox.SelectedItem.ToString()]);
                                    }
                                }

                                circle.SetRadius(int.Parse(radius1Input.Text));

                            }
                            else if (shape is MySquare square)
                            {
                                square.Move(x1, y1);

                                if (redInput.Visible)
                                {
                                    square.ChangeColor(Color.FromArgb(redInput.Value, greenInput.Value, blueInput.Value));
                                }
                                else
                                {
                                    square.ChangeColor(colorMapping[colorСomboBox.SelectedItem.ToString()]);
                                }

                                if (fillCheckBox.Checked)
                                {
                                    square.Fill();
                                }
                                else
                                {
                                    square.Unfill();
                                }

                                if (borderCheckBox.Checked)
                                {
                                    square.SetBorder(int.Parse(borderSizeInput.Text), Color.Red);
                                    if (redBorderInput.Visible)
                                    {
                                        square.ChangeBorderColor(Color.FromArgb(redBorderInput.Value, greenBorderInput.Value, blueBorderInput.Value));
                                    }
                                    else
                                    {
                                        square.ChangeBorderColor(colorMapping[borderColorComboBox.SelectedItem.ToString()]);
                                    }
                                }

                                square.SetSide(int.Parse(sideInput.Text));
                            }

                            RedrawCanvas();
                        }
                        else
                        {
                            MessageBox.Show("Invalid input for coordinates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Invalid input format: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Null value encountered: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"Value out of range: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"Key not found: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            
            shapes.Clear();
            shapesListBox.Items.Clear();
            canvasPanel.Invalidate();
        }

        private void RedrawCanvas()
        {

            canvasPanel.Invalidate();
            canvasPanel.Update();

            using (Graphics g = canvasPanel.CreateGraphics())
            {
                foreach (var shape in shapes)
                {
                    if (shape is MyPoint point) point.Draw(g);
                    if (shape is Segment segment) segment.Draw(g);
                    if (shape is MyRectangle rectangle) rectangle.Draw(g);
                    if (shape is MyEllipse ellipse) ellipse.Draw(g);
                    if (shape is MyCircle circle) circle.Draw(g);
                    if (shape is MySquare square) square.Draw(g);
                }
            }
        }

        private void ShapesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            x1Input.Visible = false;
            x2Input.Visible = false;
            y1Input.Visible = false;
            y2Input.Visible = false;
            applyButton.Visible = false;
            radius1Input.Visible = false;
            radius2Input.Visible = false;
            colorСomboBox.Visible = false;
            borderColorComboBox.Visible = false;
            borderSizeInput.Visible = false;
            redInput.Visible = false;
            greenInput.Visible = false;
            blueInput.Visible = false;
            sideInput.Visible = false;
            widthInput.Visible = false;
            heightInput.Visible = false;
            borderCheckBox.Visible = false;
            fillCheckBox.Visible = false;


            if (shapesListBox.SelectedItem != null)
            {   
                

                applyButton.Visible = true;

                x1Input.Visible = true;
                y1Input.Visible = true;
                colorСomboBox.Visible = true;
                
                

                int index = shapesListBox.SelectedIndex;
                if (shapes[index] is MyPoint point)
                {
                    int x = point.GetX();
                    int y = point.GetY();
                    x1Input.Text = x.ToString();
                    y1Input.Text = y.ToString();


                }

                else if (shapes[index] is Segment segment )
                {
                    x2Input.Visible = true;
                    y2Input.Visible = true;
                    widthInput.Visible = true;
                    int x1 = segment.GetX1();
                    int y1 = segment.GetY1();
                    int x2 = segment.GetX2();
                    int width = segment.GetWidth();
                    int y2 = segment.GetY2();
                    x1Input.Text = x1.ToString();
                    y1Input.Text = y1.ToString();
                    x2Input.Text = x2.ToString();
                    y2Input.Text = y2.ToString();
                    widthInput.Text = width.ToString();

                    

                }

                else if (shapes[index] is MyCircle circle )
                {
                    int x = circle.GetX();
                    int y = circle.GetY();
                    x1Input.Text = x.ToString();
                    y1Input.Text = y.ToString();
                    radius1Input.Visible = true;
                    radius1Input.Text = circle.GetRadius().ToString();
                    borderCheckBox.Visible = true;
                    borderCheckBox.Checked = circle.hasBorder();
                    fillCheckBox.Visible = true;
                    fillCheckBox.Checked = circle.IsFilled();
                    borderSizeInput.Text = circle.GetBorderSize().ToString();

                    
                }


                else if (shapes[index] is MyRectangle rectangle )
                {
                    int x = rectangle.GetX();
                    int y = rectangle.GetY();
                    x1Input.Text = x.ToString();
                    y1Input.Text = y.ToString();
                    widthInput.Visible = true;
                    heightInput.Visible = true;
                    widthInput.Text = rectangle.GetWidth().ToString();
                    heightInput.Text = rectangle.GetHeight().ToString();
                    borderCheckBox.Visible = true;
                    borderCheckBox.Checked = rectangle.hasBorder();
                    fillCheckBox.Visible = true;
                    fillCheckBox.Checked = rectangle.IsFilled();
                    borderSizeInput.Text = rectangle.GetBorderSize().ToString();

                }


                else if (shapes[index] is MyEllipse ellipse )
                {
                    int x = ellipse.GetX();
                    int y = ellipse.GetY();
                    x1Input.Text = x.ToString();
                    y1Input.Text = y.ToString();
                    widthInput.Visible = true;
                    heightInput.Visible = true;
                    widthInput.Text = ellipse.GetWidth().ToString();
                    heightInput.Text = ellipse.GetHeight().ToString();
                    borderCheckBox.Visible = true;
                    borderCheckBox.Checked = ellipse.hasBorder();
                    fillCheckBox.Visible = true;
                    fillCheckBox.Checked = ellipse.IsFilled();
                    borderSizeInput.Text = ellipse.GetBorderSize().ToString();

                }


                else if (shapes[index] is MySquare square )
                {
                    int x = square.GetX();
                    int y = square.GetY();
                    x1Input.Text = x.ToString();
                    y1Input.Text = y.ToString();
                    sideInput.Visible = true;
                    sideInput.Text = square.GetSide().ToString();
                    borderCheckBox.Visible = true;
                    borderCheckBox.Checked = square.hasBorder();
                    fillCheckBox.Visible = true;
                    fillCheckBox.Checked = square.IsFilled();
                    borderSizeInput.Text = square.GetBorderSize().ToString();


                }

            }
            else
            {
                applyButton.Visible = false;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
           
            if (shapesListBox.SelectedItem != null)
            {
                int index = shapesListBox.SelectedIndex;
                shapes.RemoveAt(index);
                shapesListBox.Items.RemoveAt(index);

                RedrawCanvas();
            }
        }

        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            string selectedShape = shapeSelector.SelectedItem?.ToString();
            if (selectedShape == "Точка")
            {
                MyPoint point = new MyPoint();
                point.Create(e.X, e.Y);
                using (Graphics g = canvasPanel.CreateGraphics())
                {
                    point.Draw(g);
                }
                shapes.Add(point);
                shapesListBox.Items.Add("Точка");
            }
            else if (selectedShape == "Отрезок")
            {
                if (startX == null && startY == null)
                {
               
                    startX = e.X;
                    startY = e.Y;
                }
                else
                {

                    Segment segment = new Segment();
                    segment.Create(startX.Value, startY.Value, e.X, e.Y);
                    using (Graphics g = canvasPanel.CreateGraphics())
                    {
                        segment.Draw(g);
                    }
                    shapes.Add(segment);
                    shapesListBox.Items.Add("Отрезок");


                    startX = null;
                    startY = null;
                }
            }
            else if (selectedShape == "Прямоугольник")
            {
                if (rectX1 == null && rectY1 == null)
                {

                    rectX1 = e.X;
                    rectY1 = e.Y;
                }
                else
                {

                    MyRectangle rectangle = new MyRectangle();


                    int x1 = Math.Min(rectX1.Value, e.X);
                    int y1 = Math.Min(rectY1.Value, e.Y);
                    int x2 = Math.Max(rectX1.Value, e.X);
                    int y2 = Math.Max(rectY1.Value, e.Y);


                    int width = x2 - x1;
                    int height = y2 - y1;

                    rectangle.Create(x1, y1, width, height);
                    using (Graphics g = canvasPanel.CreateGraphics())
                    {
                        rectangle.Draw(g);
                    }
                    shapes.Add(rectangle);
                    shapesListBox.Items.Add("Прямоугольник"); 

                    rectX1 = null;
                    rectY1 = null;
                }
            }
            else if (selectedShape == "Эллипс")
            {
                if (ellipseX1 == null && ellipseY1 == null)
                {

                    ellipseX1 = e.X;
                    ellipseY1 = e.Y;
                }
                else
                {

                    MyEllipse ellipse = new MyEllipse();


                    int x1 = Math.Min(ellipseX1.Value, e.X);
                    int y1 = Math.Min(ellipseY1.Value, e.Y);
                    int x2 = Math.Max(ellipseX1.Value, e.X);
                    int y2 = Math.Max(ellipseY1.Value, e.Y);


                    int width = x2 - x1;
                    int height = y2 - y1;

                    ellipse.Create(x1, y1, width, height);
                    using (Graphics g = canvasPanel.CreateGraphics())
                    {
                        ellipse.Draw(g);
                    }
                    shapes.Add(ellipse); 
                    shapesListBox.Items.Add("Эллипс"); 


                    ellipseX1 = null;
                    ellipseY1 = null;
                }
            }
            else if (selectedShape == "Круг")
            {
                if (circleX1 == null && circleY1 == null)
                {

                    circleX1 = e.X;
                    circleY1 = e.Y;
                }
                else
                {

                    MyCircle circle = new MyCircle();


                    int radius = (int)Math.Sqrt(Math.Pow(e.X - circleX1.Value, 2) + Math.Pow(e.Y - circleY1.Value, 2));

                    circle.Create(circleX1.Value, circleY1.Value, radius);
                    using (Graphics g = canvasPanel.CreateGraphics())
                    {
                        circle.Draw(g);
                    }
                    shapes.Add(circle); 
                    shapesListBox.Items.Add("Круг"); 


                    circleX1 = null;
                    circleY1 = null;
                }
            }
            else if (selectedShape == "Квадрат")
            {
                if (squareX1 == null && squareY1 == null)
                {

                    squareX1 = e.X;
                    squareY1 = e.Y;
                }
                else
                {

                    MySquare square = new MySquare();


                    int x1 = Math.Min(squareX1.Value, e.X);
                    int y1 = Math.Min(squareY1.Value, e.Y);
                    int x2 = Math.Max(squareX1.Value, e.X);
                    int y2 = Math.Max(squareY1.Value, e.Y);


                    int side = Math.Max(x2 - x1, y2 - y1);

                    square.Create(x1, y1, side);
                    using (Graphics g = canvasPanel.CreateGraphics())
                    {
                        square.Draw(g);
                    }
                    shapes.Add(square); 
                    shapesListBox.Items.Add("Квадрат"); 


                    squareX1 = null;
                    squareY1 = null;
                }
            }
        }
    }
}