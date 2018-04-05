using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buildTable()
        {
            // Строит таблицу
            dataGridView1.Columns.Add("exp", "Опыт");
            addX();
            dataGridView1.Columns.Add("u", "U");
            dataGridView1.Columns.Add("y", "Y");
            dataGridView1.Columns.Add("d", "D");
            setWidthAllColumns(48, 36);
        }

        private void setWidthAllColumns(int widthFirstColumn, int widthAllColumns)
        {
            // Устанавливает фиксированный размер столбцов
            dataGridView1.Columns[0].Width = widthFirstColumn;
            int columns = dataGridView1.ColumnCount;
            while (columns > 1)
            {
                dataGridView1.Columns[--columns].Width = widthAllColumns;
            }
        }

        private void addX()
        {
            // Добавляет в таблицу требуемое количество входных данных
            for (int i = 1; i <= numericUpDown1.Value; i++)
            {
                dataGridView1.Columns.Add("x" + i, "X" + i);
            }
        }

        private void clearData()
        {
            // Очищает все данные с таблицы
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
        }

        private bool checkEnterDataOnTable()
        {
            // Возвращает, заполнены ли все входные данные в таблице
            bool result = true;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value == null)
                {
                    result = false;
                    return result;
                }
                for (int j = 1; j < dataGridView1.ColumnCount - 3; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        result = false;
                        return result;
                    }
                }
            }
            return result;
        }

        private string getCurentTime()
        {
            // Возвращает текущее время
            return DateTime.UtcNow.ToString();
        }

        private void writeLine(string text)
        {
            // Кастомный вывод текста на консоль
            Console.WriteLine(getCurentTime() + " " + text);
        }

        Random rand = new Random();

        private double getRandomNumber()
        {
            int minValue = -10;
            int maxValue = 10;
            return rand.Next(minValue, maxValue);
        }

        private double getU(double[] x, double[] w)
        {
            double u = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                u += x[i] * w[i];
            }
            return u;
        }

        private double getU(int row, double[,] x, double[] w)
        {
            double u = 0;
            for (int i = 0; i < x.GetLength(1); i++)
            {
                u += x[row, i] * w[i];
            }
            return u;
        }

        private double getY(double u)
        {
            double y;
            if (u < 0)
            {
                y = 0;
            }
            else
            {
                y = 1;
            }
            return y;
        }

        private void persectron()
        {
            int count = dataGridView1.ColumnCount - 3;
            int exp = dataGridView1.Rows.Count - 1;

            double[,] x = new double[exp, count];
            double[] w = new double[count];

            // Заполняем массив входов
            for (int i = 0; i < exp; i++)
            {
                x[i, 0] = 1;
                for (int j = 1; j < count; j++)
                {
                    x[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value.ToString());
                }
            }

            // Проверка заполнения массива
            for (int i = 0; i < exp; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Console.WriteLine("x[{0}, {1}] = {2}", i, j, x[i, j]);  
                }
            }

            // Заполняем раздомными значениями веса
            for (int i = 0; i < count; i++)
            {
                w[i] = getRandomNumber();
            }

            bool cycle;
            do
            {
                cycle = false;
                for (int i = 0; i < exp; i++)
                {
                    double d = Double.Parse(dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value.ToString());
                    double u = getU(i, x, w);
                    double y = getY(u);

                    while (d != y)
                    {
                        cycle = true;

                        for (int j = 0; j < count; j++)
                        {
                            w[j] += x[i, j] * (d - y);
                        }
                        y = getY(getU(i, x, w));
                    }
                    dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 3].Value = u;
                    dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 2].Value = y;
                }
            } while (cycle);


            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("w{0} = {1}", i, w[i]);
            }

        }

        private void debugData()
        {

        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('-'))
            {
                writeLine("Ввод знака '-' запрещен!");
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearData();
            writeLine("Таблица очищена");
            buildTable();
            writeLine("Таблица построена");

            // DEBUG DATA
            debugData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Выполнение алгоритма
            if (checkEnterDataOnTable())
            {
                writeLine("Проверка прошла успешно");
                persectron();
            } else
            {
                writeLine("Данные в таблице заполнены неверно");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // TODO: Доделать 
            writeLine("Команта \"Добавить X\" пока не реализована");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // TODO: Доделать 
            writeLine("Команта \"Добавить эксперемент\" пока не реализована");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // TODO: Доделать 
            writeLine("Команта \"Убрать последний\" пока не реализована");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int countExp = dataGridView1.Rows.Count - 1;
            if (countExp > 0)
            {
                dataGridView1.Rows.RemoveAt(countExp - 1);
            }
            writeLine("Последний эксперемент удален");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clearData();
            writeLine("Таблица очищена");
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // При добавлении пользователем нового эксперемента, то
            // в этом эксперементе автоматически заполняется номер опыта
            int rows = dataGridView1.Rows.Count;
            dataGridView1.Rows[rows - 2].Cells[0].Value = rows - 1;
        }

    }
}
