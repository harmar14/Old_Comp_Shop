using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace kursproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
                MessageBox.Show("Заполните все поля", "Ошибка");
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = comboBox1.Text;
                dataGridView1.Rows[n].Cells[2].Value = textBox2.Text;
                dataGridView1.Rows[n].Cells[3].Value = numericUpDown4.Value;
                dataGridView1.Rows[n].Cells[4].Value = 0;
                dataGridView1.Rows[n].Cells[5].Value = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = "Data.xml";
            SFD.Filter = "XML (*.xml)|*.xml";

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataSet ds = new DataSet(); // создаем пустой кэш данных
                    DataTable dt = new DataTable(); //создаем пустую таблицу
                    ds.Tables.Add(dt);
                    dt.TableName = "Shop";
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Category");
                    dt.Columns.Add("Edition");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("Count");
                    dt.Columns.Add("Cost");

                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        DataRow row = ds.Tables["Shop"].NewRow();
                        row["Name"] = r.Cells[0].Value;
                        row["Category"] = r.Cells[1].Value;
                        row["Edition"] = r.Cells[2].Value;
                        row["Price"] = r.Cells[3].Value;
                        row["Count"] = 0;
                        row["Cost"] = 0;
                        ds.Tables["Shop"].Rows.Add(row);//добавление всей строки в таблицу
                    }

                    ds.WriteXml(SFD.FileName);
                    MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.FileName = "Data.xml";
            OFD.Filter = "XML (*.xml)|*.xml";

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                if (dataGridView1.Rows.Count > 0)
                    MessageBox.Show("Очистите поле перед загрузкой нового файла", "Ошибка");
                else
                {
                    if (File.Exists(OFD.FileName))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(OFD.FileName);
                        foreach (DataRow item in ds.Tables["Shop"].Rows)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = item["Name"];
                            dataGridView1.Rows[n].Cells[1].Value = item["Category"];
                            dataGridView1.Rows[n].Cells[2].Value = item["Edition"];
                            dataGridView1.Rows[n].Cells[3].Value = Convert.ToInt32(item["Price"]);
                            dataGridView1.Rows[n].Cells[4].Value = Convert.ToInt32(item["Count"]);
                            dataGridView1.Rows[n].Cells[5].Value = Convert.ToInt32(item["Cost"]);
                        }
                    }
                    else
                        MessageBox.Show("XML файл не найден", "Ошибка");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = comboBox1.Text;
                dataGridView1.Rows[n].Cells[2].Value = textBox2.Text;
                dataGridView1.Rows[n].Cells[3].Value = Convert.ToInt32(numericUpDown4.Value);       
            }
            else
                MessageBox.Show("Выберите строку для редактирования", "Ошибка");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index); //удаление
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблица пустая.", "Ошибка.");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            numericUpDown4.Value = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[3].Value);
            numericUpDown1.Value = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[4].Value);
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[4].Value = Convert.ToInt32(numericUpDown1.Value);
                dataGridView1.Rows[n].Cells[5].Value = Convert.ToInt32(numericUpDown1.Value) * Convert.ToInt32(dataGridView1.Rows[n].Cells[3].Value);
                textBox6.Text = dataGridView1.Rows[n].Cells[5].Value.ToString();
            }
            else
                MessageBox.Show("Выберите строку для редактрирования", "Ошибка");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var sum = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows) // пока в dataGridView1 есть строки
                sum += Convert.ToInt32(r.Cells[5].Value);
            if (checkBox1.Checked) sum += 1500;
            
            textBox8.Text = sum.ToString();
            MessageBox.Show("Спасибо за покупку", "Выполнено");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].FormattedValue.ToString().Contains(textBox5.Text.Trim()))
                {
                    dataGridView1.CurrentCell = dataGridView1[0, i];
                    return;
                }
                MessageBox.Show("Результаты не найдены", "Поиск");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
