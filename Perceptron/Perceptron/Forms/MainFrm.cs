﻿using System;
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

        private bool checkEnterDataOnTable()
        {
            // Возвращает, заполнены ли все входные данные в таблице
            bool result = true;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Выполнение алгоритма
            int count = dataGridView1.ColumnCount - 4;
            int exp = dataGridView1.Rows.Count;

            if (checkEnterDataOnTable())
            {
                writeLine("Проверка прошла успешно");
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
            // TODO: Доделать 
            writeLine("Команта \"Убрать последний эксперемент\" пока не реализована");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            writeLine("Таблица очищена");
            clearData();
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
