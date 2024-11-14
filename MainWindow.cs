using Gtk;
using System;

public class MainWindow : Window
{
    private Entry display;
    private string currentNumber = "";
    private double? firstNumber = null;
    private string currentOperator = "";

    public MainWindow() : base("計算機")
    {
        // 設置窗口屬性
        SetDefaultSize(300, 400);
        SetPosition(WindowPosition.Center);

        // 創建主容器
        var mainBox = new Box(Orientation.Vertical, 5);
        mainBox.Margin = 10;

        // 創建顯示區域
        display = new Entry();
        display.IsEditable = false;
        display.Alignment = 1; // 右對齊
        display.MarginBottom = 10;
        display.HeightRequest = 50;
        display.WidthRequest = 280;
        mainBox.PackStart(display, false, true, 0);

        // 創建按鈕網格
        var grid = new Grid();
        grid.RowSpacing = 5;
        grid.ColumnSpacing = 5;
        grid.RowHomogeneous = true;
        grid.ColumnHomogeneous = true;

        // 按鈕文本和位置
        var buttons = new string[,] {
            {"7", "8", "9", "/"},
            {"4", "5", "6", "*"},
            {"1", "2", "3", "-"},
            {"0", ".", "=", "+"}
        };

        // 創建並添加按鈕
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var button = new Button(buttons[i, j]);
                button.HeightRequest = 60;

                if ("0123456789.".Contains(buttons[i, j]))
                {
                    button.Clicked += NumberButton_Clicked;
                }
                else if ("+-*/".Contains(buttons[i, j]))
                {
                    button.Clicked += OperatorButton_Clicked;
                }
                else if (buttons[i, j] == "=")
                {
                    button.Clicked += CalculateButton_Clicked;
                }

                grid.Attach(button, j, i, 1, 1);
            }
        }

        mainBox.PackStart(grid, true, true, 0);

        // 添加清除按鈕
        var clearButton = new Button("清除");
        clearButton.HeightRequest = 40;
        clearButton.MarginTop = 10;
        clearButton.Clicked += ClearButton_Clicked;
        mainBox.PackStart(clearButton, false, true, 0);

        // 添加主容器到窗口
        Add(mainBox);

        // 設置關閉事件
        DeleteEvent += (sender, e) => Application.Quit();

        // 添加鍵盤事件處理
        KeyPressEvent += MainWindow_KeyPressEvent;
    }

    [GLib.ConnectBefore]
    private void MainWindow_KeyPressEvent(object o, KeyPressEventArgs args)
    {
        switch (args.Event.Key)
        {
            // 數字鍵 0-9
            case Gdk.Key.Key_0:
            case Gdk.Key.KP_0:
                SimulateNumberInput("0");
                break;
            case Gdk.Key.Key_1:
            case Gdk.Key.KP_1:
                SimulateNumberInput("1");
                break;
            case Gdk.Key.Key_2:
            case Gdk.Key.KP_2:
                SimulateNumberInput("2");
                break;
            case Gdk.Key.Key_3:
            case Gdk.Key.KP_3:
                SimulateNumberInput("3");
                break;
            case Gdk.Key.Key_4:
            case Gdk.Key.KP_4:
                SimulateNumberInput("4");
                break;
            case Gdk.Key.Key_5:
            case Gdk.Key.KP_5:
                SimulateNumberInput("5");
                break;
            case Gdk.Key.Key_6:
            case Gdk.Key.KP_6:
                SimulateNumberInput("6");
                break;
            case Gdk.Key.Key_7:
            case Gdk.Key.KP_7:
                SimulateNumberInput("7");
                break;
            case Gdk.Key.Key_8:
            case Gdk.Key.KP_8:
                SimulateNumberInput("8");
                break;
            case Gdk.Key.Key_9:
            case Gdk.Key.KP_9:
                SimulateNumberInput("9");
                break;

            // 運算符
            case Gdk.Key.plus:
            case Gdk.Key.KP_Add:
                SimulateOperatorInput("+");
                break;
            case Gdk.Key.minus:
            case Gdk.Key.KP_Subtract:
                SimulateOperatorInput("-");
                break;
            case Gdk.Key.asterisk:
            case Gdk.Key.KP_Multiply:
                SimulateOperatorInput("*");
                break;
            case Gdk.Key.slash:
            case Gdk.Key.KP_Divide:
                SimulateOperatorInput("/");
                break;

            // 小數點
            case Gdk.Key.period:
            case Gdk.Key.KP_Decimal:
                SimulateNumberInput(".");
                break;

            // Enter 鍵計算結果
            case Gdk.Key.Return:
            case Gdk.Key.KP_Enter:
                Calculate();
                break;

            // Escape 鍵清除
            case Gdk.Key.Escape:
                ClearCalculator();
                break;

            // Backspace 鍵刪除
            case Gdk.Key.BackSpace:
                if (currentNumber.Length > 0)
                {
                    currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
                    display.Text = string.IsNullOrEmpty(currentNumber) ? "0" : currentNumber;
                }
                break;
        }
    }

    private void SimulateNumberInput(string number)
    {
        if (number == "." && currentNumber.Contains("."))
            return;

        currentNumber += number;
        display.Text = currentNumber;
    }

    private void SimulateOperatorInput(string op)
    {
        if (!string.IsNullOrEmpty(currentNumber))
        {
            if (firstNumber.HasValue)
            {
                Calculate();
            }
            else
            {
                firstNumber = double.Parse(currentNumber);
            }
            currentOperator = op;
            currentNumber = "";
        }
    }

    private void NumberButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // 防止多個小數點
            if (button.Label == "." && currentNumber.Contains("."))
                return;

            currentNumber += button.Label;
            display.Text = currentNumber;
        }
    }

    private void OperatorButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && !string.IsNullOrEmpty(currentNumber))
        {
            if (firstNumber.HasValue)
            {
                Calculate();
            }
            else
            {
                firstNumber = double.Parse(currentNumber);
            }
            currentOperator = button.Label;
            currentNumber = "";
        }
    }

    private void CalculateButton_Clicked(object sender, EventArgs e)
    {
        Calculate();
    }

    private void Calculate()
    {
        if (firstNumber.HasValue && !string.IsNullOrEmpty(currentNumber))
        {
            try
            {
                double secondNumber = double.Parse(currentNumber);
                double result = 0;

                switch (currentOperator)
                {
                    case "+":
                        result = firstNumber.Value + secondNumber;
                        break;
                    case "-":
                        result = firstNumber.Value - secondNumber;
                        break;
                    case "*":
                        result = firstNumber.Value * secondNumber;
                        break;
                    case "/":
                        if (secondNumber == 0)
                            throw new DivideByZeroException();
                        result = firstNumber.Value / secondNumber;
                        break;
                }

                display.Text = result.ToString();
                firstNumber = result;
                currentNumber = "";
            }
            catch (DivideByZeroException)
            {
                display.Text = "錯誤：除數不能為零";
                ClearCalculator();
            }
            catch (Exception)
            {
                display.Text = "錯誤";
                ClearCalculator();
            }
        }
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        ClearCalculator();
    }

    private void ClearCalculator()
    {
        currentNumber = "";
        firstNumber = null;
        currentOperator = "";
        display.Text = "";
    }
}