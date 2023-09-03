using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace Отдел_по_продажам
{
    public partial class Form1 : Form
    {
        MySqlConnection Baza = new MySqlConnection("host = localhost; user = root; password = 12345; database = отдел_по_продажам");
        int kz = 0; double rch = 0; int ks = 0; int ksy = 0; int kp = 0; 
        public Form1()
        {
            InitializeComponent();
            Baza.Open();
            tabControl1.Visible = false;
            menu();
            items();
            Baza.Close();
        }

        void menu()
        {
            if (login.Text == "" && password.Text == "")
            {
                login.ForeColor = Color.Gray;
                password.ForeColor = Color.Gray;
                Font fnt = new Font(login.Font.FontFamily, 10.0F);
                Font fnt1 = new Font(password.Font.FontFamily, 10.0F);
                login.Font = fnt;
                password.Font = fnt1;
                login.Text = "Введите логин";
                password.Text = "Введите пароль";
                password.PasswordChar = '\0';
            }
            if (ZVbox4.Text == "" || KPbox4.Text == "")
            {
                ZVbox4.ForeColor = Color.Gray;
                KPbox4.ForeColor = Color.Gray;
                Font fnt = new Font(ZVbox4.Font.FontFamily, 10.0F);
                Font fnt1 = new Font(KPbox4.Font.FontFamily, 10.0F);
                ZVbox4.Font = fnt;
                KPbox4.Font = fnt1;
                ZVbox4.Text = "Введите номер";
                KPbox4.Text = "Введите E-mail";
            }
        }
        void download()
        {
            panel1.Visible = false;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView6.Rows.Clear();
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM отдел_по_продажам.товары_view order by idТовары asc;", Baza);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6]);
            }
            rdr.Close();
            /////////////////////////
            MySqlCommand cmd1 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.поставки_view where Ответсвенный = '{name}'", Baza);
            MySqlDataReader rdr1 = cmd1.ExecuteReader();
            while (rdr1.Read())
            {
                dataGridView3.Rows.Add(rdr1[0], rdr1[1], rdr1[2], rdr1[3], rdr1[4], rdr1[5], rdr1[6], rdr1[7]);
            }
            rdr1.Close();
            /////////////////////////
            if(id_N != "5")
            {
                MySqlCommand cmd2 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.эффективность_персонала_view where Ответсвенный = '{name}'", Baza);
                MySqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    dataGridView4.Rows.Add(rdr2[0], rdr2[1], rdr2[2], rdr2[3], rdr2[4], rdr2[5], rdr2[6]);
                }
                rdr2.Close();
            }
            else
            {
                MySqlCommand cmd2 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.эффективность_персонала_view", Baza);
                MySqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    dataGridView4.Rows.Add(rdr2[0], rdr2[1], rdr2[2], rdr2[3], rdr2[4], rdr2[5], rdr2[6]);
                }
                rdr2.Close();
            }
            /////////////////////////
            MySqlCommand cmd3 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.поставщики", Baza);
            MySqlDataReader rdr3 = cmd3.ExecuteReader();
            while (rdr3.Read())
            {
                dataGridView2.Rows.Add(rdr3[0], rdr3[1], rdr3[2], rdr3[3]);
            }
            rdr3.Close();
            /////////////////////////
            MySqlCommand cmd7 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.реализация_view where Статус_оплаты = 'Не оплачено' and Ответственный = '{name}';", Baza);
            MySqlDataReader rdr7 = cmd7.ExecuteReader();
            while (rdr7.Read())
            {
                dataGridView5.Rows.Add(rdr7[0], rdr7[1], rdr7[2], rdr7[3], rdr7[4], rdr7[5], rdr7[6], rdr7[7], rdr7[8], rdr7[9], rdr7[10], rdr7[11], rdr7[12]);
            }
            rdr7.Close();
            /////////////////////////
            MySqlCommand cmd8 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.реализация_view where Статус_оплаты = 'Оплачено' and Ответственный = '{name}'", Baza);
            MySqlDataReader rdr8= cmd8.ExecuteReader();
            while (rdr8.Read())
            {
                dataGridView6.Rows.Add(rdr8[0], rdr8[1], rdr8[2], rdr8[3], rdr8[4], rdr8[5], rdr8[6], rdr8[7], rdr8[8], rdr8[9], rdr8[10], rdr8[11], rdr8[12]);
            }
            rdr8.Close();
            /////////////////////////
            MySqlCommand cmd9 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.критерии_выполненения_задач;", Baza);
            MySqlDataReader rdr9 = cmd9.ExecuteReader();
            while (rdr9.Read())
            {
                dataGridView7.Rows.Add(rdr9[0], rdr9[1], rdr9[2], rdr9[3], rdr9[4], rdr9[5], rdr9[6], rdr9[7], rdr9[8]);
            }
            rdr9.Close();
            /////////////////////////
            MySqlCommand cmd10 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.сотрудники_view where Должность = 'Менеджер'", Baza);
            MySqlDataReader rdr10 = cmd10.ExecuteReader();
            while (rdr10.Read())
            {
                dataGridView8.Rows.Add(rdr10[0], rdr10[1], rdr10[2], rdr10[3], rdr10[4], rdr10[5], rdr10[6]);
            }
            rdr10.Close();


        }
        void items()
        { 
            DOLbox8.Items.Clear();
            MySqlCommand cmd4 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.товары_view", Baza);
            MySqlDataReader rdr4 = cmd4.ExecuteReader();
            while (rdr4.Read())
            {
                Tbox4.Items.Add(rdr4[1]);
                TOVbox1.Items.Add(rdr4[1]);
            }
            rdr4.Close();
            /////////////////////////
            MySqlCommand cmd5 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.вид_оплаты;", Baza);
            MySqlDataReader rdr5 = cmd5.ExecuteReader();
            while (rdr5.Read())
            {
                VObox4.Items.Add(rdr5[1]);
            }
            rdr5.Close();
            /////////////////////////
            MySqlCommand cmd6 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.вид_отправки;", Baza);
            MySqlDataReader rdr6 = cmd6.ExecuteReader();
            while (rdr6.Read())
            {
                VDbox4.Items.Add(rdr6[1]);
            }
            rdr6.Close();
            //////////////////////////
            MySqlCommand cmd7 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.должность;", Baza);
            MySqlDataReader rdr7 = cmd7.ExecuteReader();
            while (rdr7.Read())
            {
                DOLbox8.Items.Add(rdr7[1]);
            }
            rdr7.Close();
        }
        void razmer_sred_check()
        {
            double sum_check = 0;
            double k = 0;
            double sred_check = 0;
            MySqlCommand cmd2 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.реализация_view where Ответственный = '{name}'", Baza);
            MySqlDataReader rdr2 = cmd2.ExecuteReader();
            while (rdr2.Read())
            {
                sum_check += Convert.ToInt32(rdr2[5]);
                k++;
            }
            rdr2.Close();
            sred_check = sum_check / k;
            MySqlCommand cmd6 = new MySqlCommand($"UPDATE `отдел_по_продажам`.`эффективность_персонала` SET `Размер_среднего_чека` = '{Convert.ToInt32(sred_check)}' WHERE (`Ответсвенный` = '{id_N}');", Baza);
            cmd6.ExecuteNonQuery();
            rch = sred_check;
            download();
            grafics();
        }
        void grafics()
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();
            chart5.Series[0].Points.Clear();
            string sdelka = "", sdelka_Y = "", sdelka_MAX = "", sdelka_MIN = "", sdelka_MAX_Y = "", sdelka_MIN_Y = "";
            string money_Mounth = "", money_Year = "", Mounth = "", Year = "", LQS = "", SQL = "", mesac = "", god = "";
            int money = 0, sdacha = 0, money_MAX_Mounth = 0, money_MAX_Year = 0, sum_m = 0, sum_y = 0;
            bool yes_no = false;
            DateTime date = new DateTime();
            MySqlCommand cmd9 = new MySqlCommand($"delete FROM отдел_по_продажам.прибыль;", Baza);
            cmd9.ExecuteNonQuery();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////// Графики "Эффективности"
            if (id_N == "5")
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT MAX(Количество_сделок), MAX(Количество_успешных_сделок), MIN(Количество_сделок), MIN(Количество_успешных_сделок) FROM отдел_по_продажам.эффективность_персонала;", Baza);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sdelka_MAX = rdr[0].ToString();
                    sdelka_MAX_Y = rdr[1].ToString();
                    sdelka_MIN = rdr[2].ToString();
                    sdelka_MIN_Y = rdr[3].ToString();
                }
                rdr.Close();
                ////////////////////////////
                MySqlCommand cmd3 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.эффективность_персонала_view;", Baza);
                MySqlDataReader rdr3 = cmd3.ExecuteReader();
                int kol = 1;
                while (rdr3.Read())
                {

                    sdelka = rdr3[4].ToString();
                    sdelka_Y = rdr3[5].ToString();
                    chart1.ChartAreas[0].AxisY.Maximum = 600;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.Series[0].Points.AddXY($"{rdr3[1]}", $"{sdelka}");
                    chart1.Series[0].Points.AddXY($"{rdr3[1]}", $"{sdelka_Y}");
                    chart1.Series[0].Points[kol].Color = Color.Orange;
                    kol += 2;
                }
                rdr3.Close();
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT MAX(Количество_сделок), MAX(Количество_успешных_сделок), MIN(Количество_сделок), MIN(Количество_успешных_сделок) FROM отдел_по_продажам.эффективность_персонала;", Baza);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sdelka_MAX = rdr[0].ToString();
                    sdelka_MAX_Y = rdr[1].ToString();
                    sdelka_MIN = rdr[2].ToString();
                    sdelka_MIN_Y = rdr[3].ToString();
                }
                rdr.Close();
                ////////////////////////////
                MySqlCommand cmd3 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.эффективность_персонала where Ответсвенный = '{id_N}';", Baza);
                MySqlDataReader rdr3 = cmd3.ExecuteReader();
                while (rdr3.Read())
                {
                    sdelka = rdr3[4].ToString();
                    sdelka_Y = rdr3[5].ToString();
                }
                rdr3.Close();
                chart1.ChartAreas[0].AxisY.Maximum = 350;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.Series[0].Points.AddXY($"Сделки", $"{sdelka}");
                chart1.Series[0].Points.AddXY($"Успешные сделки", $"{sdelka_Y}");
                chart1.Series[0].Points[1].Color = Color.Orange;
            }
            
            ////////////////////////////////////////////////////////////////////////////////// Графики "Критерии выполнения"
            string sdelka_Mounth = "", sdelka_Year = "", sdelka_MAX_Mounth = "", sdelka_MAX_Year = "";
            List<string> Mounths = new List<string>();
            List<string> Years = new List<string>();
            int ms = 0, ys = 0;
            double proc_m = 0, proc_y = 0;
            MySqlCommand cmd11 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.критерии_выполненения_задач;", Baza);
            MySqlDataReader rdr11 = cmd11.ExecuteReader();
            while (rdr11.Read())
            {
                Years.Add(rdr11[4].ToString());
                Mounths.Add(rdr11[2].ToString());

            }
            rdr11.Close();
            /////////////////////////////////////////////////////
            MySqlCommand cmd12 = new MySqlCommand($"SELECT Дата, Сумма_к_оплате, Сдача  FROM отдел_по_продажам.реализация;", Baza);
            MySqlDataReader rdr12 = cmd12.ExecuteReader();
            while (rdr12.Read())
            {
                date = Convert.ToDateTime(rdr12[0]);
                Mounth = date.ToString("MMMM");
                Year = date.ToString("yyyy");
                for(int i = 0; i<Mounths.Count; i++)
                {
                    if(Mounth == Mounths[i])
                    {
                        mesac = Mounth;
                        break;
                    }
                }
                for (int i = 0; i < Mounths.Count; i++)
                {
                    if (Year == Years[i])
                    {
                        god = Years[i].ToString();
                        break;
                    }
                }
                
            }
            rdr12.Close();
            /////////////////////////////////////////////
            string data_Y = "", data_M = "";
            MySqlCommand cmd14 = new MySqlCommand($"SELECT Дата, Сумма_к_оплате, Сдача  FROM отдел_по_продажам.реализация;", Baza);
            MySqlDataReader rdr14 = cmd14.ExecuteReader();
            while (rdr14.Read())
            {
                data_Y = rdr14[0].ToString();
                data_Y = data_Y.Remove(0, 6);
                date = Convert.ToDateTime(rdr14[0]);
                data_M = date.ToString("MM");

            }
            rdr14.Close();
            ///////////////////////////////////////
            MySqlCommand cmd15 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.реализация where Дата LIKE '%{data_M}.{data_Y}';", Baza);
            MySqlDataReader rdr15 = cmd15.ExecuteReader();
            while (rdr15.Read())
            {
                sum_m += 1;
            }
            rdr15.Close();
            MySqlCommand cmd16 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.реализация where Дата LIKE '%{data_Y}';", Baza);
            MySqlDataReader rdr16 = cmd16.ExecuteReader();
            while (rdr16.Read())
            {
                sum_y += 1;
            }
            rdr16.Close();
            ////////////////////////////////////////
            MySqlCommand cmd13 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.критерии_выполненения_задач where Месяц = '{mesac}' and Год = '{god}'", Baza);
            MySqlDataReader rdr13 = cmd13.ExecuteReader();
            rdr13.Read();
            if (rdr13.HasRows)
            {
                yes_no = true;
            }
            rdr13.Close();
            ///////////////////////////// Проценты
            proc_m = (sum_m * 100) / 300;
            proc_y = (sum_y * 100) / 3300;
            //////////////////////////// Занесесние данных в таблицу "критерии оценивания"
            if (yes_no)
            {
                MySqlCommand cmd17 = new MySqlCommand($"UPDATE `отдел_по_продажам`.`критерии_выполненения_задач` SET `сделки_за_месяц` = '{sum_m}', `Сделки_за_год` = '{sum_y}', `процент_выполнения_за_месяц` = '{proc_m}%', `процент_выполнения_за_год` = '{proc_y}%' WHERE (`Месяц` = '{mesac}' and `Год` = '{god}')", Baza);
                cmd17.ExecuteNonQuery();
            }
            else if(!yes_no)
            {
                MySqlCommand cmd17 = new MySqlCommand($"INSERT INTO `отдел_по_продажам`.`критерии_выполненения_задач` (`сделки_за_месяц`, `Месяц`, `Сделки_за_год`, `Год`, `Необходимое_количество_сделок_за_месяц`, `Необходимое_количество_сделок_за_год`, `процент_выполнения_за_месяц`, `процент_выполнения_за_год`) VALUES ('{sum_m}', '{mesac}', '{sum_y}', '{god}', '300', '3300', '{proc_m}%', '{proc_y}')", Baza);
                cmd17.ExecuteNonQuery();
            }
            ///////////////////////////////////////////////////////////////////////////////
            MySqlCommand cmd1 = new MySqlCommand($"SELECT MAX(сделки_за_месяц), MAX(Сделки_за_год) FROM отдел_по_продажам.критерии_выполненения_задач;", Baza);
            MySqlDataReader rdr1 = cmd1.ExecuteReader();
            while (rdr1.Read())
            {
                sdelka_MAX_Mounth = rdr1[0].ToString();
                sdelka_MAX_Year = rdr1[1].ToString();
            }
            rdr1.Close();
            ////////////////////////////////////////////////////////////
            MySqlCommand cmd2 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.критерии_выполненения_задач group by Месяц;", Baza);
            MySqlDataReader rdr2 = cmd2.ExecuteReader();
            int kol1 = 1;
            while (rdr2.Read())
            {
                sdelka_Mounth = rdr2[1].ToString();
                chart2.ChartAreas[0].AxisY.Maximum = Convert.ToInt32(sdelka_MAX_Mounth);
                chart2.ChartAreas[0].AxisY.Minimum = 0;
                chart2.Series[0].Points.AddXY($"{rdr2[2]} {rdr2[4]}", $"{sdelka_Mounth}");
                kol1+=2;
            }
            rdr2.Close();
            /////////////////////////////////////////////////
            MySqlCommand cmd18 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.критерии_выполненения_задач group by Год;", Baza);
            MySqlDataReader rdr18 = cmd18.ExecuteReader();
            kol1 = 0;
            while (rdr18.Read())
            {
                sdelka_Year = rdr18[3].ToString();
                chart3.ChartAreas[0].AxisY.Maximum = Convert.ToInt32(sdelka_MAX_Year);
                chart3.ChartAreas[0].AxisY.Minimum = 0;
                chart3.Series[0].Points.AddXY($"{rdr18[4]}", $"{sdelka_Year}");
                chart3.Series[0].Points[kol1].Color = Color.Orange;
                kol1++;
            }
            rdr18.Close();
            ////////////////////////////////////////////////////////////////////////////////// Графики "Прибыль"
            MySqlCommand cmd4 = new MySqlCommand($"SELECT Дата, Сумма_к_оплате, Сдача  FROM отдел_по_продажам.реализация;", Baza);
            MySqlDataReader rdr4 = cmd4.ExecuteReader();
            while (rdr4.Read())
            {
                date = Convert.ToDateTime(rdr4[0]);
                Mounth = date.ToString("MMMM");
                Year = date.ToString("yyyy");
                money = Convert.ToInt32(rdr4[1]);
                sdacha = Convert.ToInt32(rdr4[2]);
                LQS += $"('{money - sdacha}', '{Mounth}', '{money - sdacha}', '{Year}'),";
            }
            rdr4.Close();
            SQL = LQS.TrimEnd(',');
            MySqlCommand cmd8 = new MySqlCommand($"INSERT INTO `отдел_по_продажам`.`прибыль` (`Прибыль_за_месяц`, `Месяц`, `Прибыль_за_год`, `Год`) VALUES {SQL}", Baza);
            cmd8.ExecuteNonQuery();
            //////////////////////////////////////////////////////
            MySqlCommand cmd7 = new MySqlCommand($"SELECT idприбыль, MAX(Сумма_за_месяц), Месяц, SUM(Сумма_за_год), Год FROM отдел_по_продажам.max_profit_view;", Baza);
            MySqlDataReader rdr7 = cmd7.ExecuteReader();
            while (rdr7.Read())
            {
                money_MAX_Mounth = Convert.ToInt32(rdr7[1]);
                money_MAX_Year = Convert.ToInt32(rdr7[3]);
            }
            rdr7.Close();
            ///////////////////////////////////////////////////////
            MySqlCommand cmd5 = new MySqlCommand($"SELECT idприбыль, SUM(Прибыль_за_месяц), Месяц, Sum(Прибыль_за_год), Год FROM отдел_по_продажам.прибыль Group by Месяц, Год;", Baza);
            MySqlDataReader rdr5 = cmd5.ExecuteReader();
            while (rdr5.Read())
            {
                money_Mounth = rdr5[1].ToString();
                chart4.ChartAreas[0].AxisY.Maximum = money_MAX_Mounth + 1000;
                chart4.ChartAreas[0].AxisY.Minimum = 0;
                chart4.Series[0].Points.AddXY($"{rdr5[2]} {rdr5[4]}", $"{money_Mounth}");
            }
            rdr5.Close();
            ////////////////////////////
            MySqlCommand cmd6 = new MySqlCommand($"SELECT idприбыль, SUM(Прибыль_за_месяц), Месяц, Sum(Прибыль_за_год), Год FROM отдел_по_продажам.прибыль Group by Год;", Baza);
            MySqlDataReader rdr6 = cmd6.ExecuteReader();
            kol1 = 0;
            while (rdr6.Read())
            {
                money_Year = rdr6[3].ToString();
                chart5.ChartAreas[0].AxisY.Maximum = money_MAX_Year+1000;
                chart5.ChartAreas[0].AxisY.Minimum = 0;
                chart5.Series[0].Points.AddXY($"{rdr6[4]}", $"{money_Year}");
                chart5.Series[0].Points[kol1].Color = Color.Orange;
                kol1++;
            }
            rdr6.Close();
        }
        void zoom()
        {
            int k_s = 0; int k_z = 0; int k_sy = 0; int k_p = 0;
            if(id_N != "5")
            {
                razmer_sred_check();
                MySqlCommand cmd1 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.реализация where Ответственный = '{id_N}';", Baza);
                MySqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    k_s++;
                }
                rdr1.Close();
                ////////////////////////////////////////////////////////////
                MySqlCommand cmd2 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.эффективность_персонала where Ответсвенный = '{id_N}';", Baza);
                MySqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    k_z = Convert.ToInt32(rdr2[2]);
                }
                rdr2.Close();
                ////////////////////////////////////////////////////////////
                MySqlCommand cmd3 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.реализация where Ответственный = '{id_N}' and Статус_оплаты = 'Оплачено';", Baza);
                MySqlDataReader rdr3 = cmd3.ExecuteReader();
                while (rdr3.Read())
                {
                    k_sy++;
                }
                rdr3.Close();
                ////////////////////////////////////////////////////////////
                MySqlCommand cmd4 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.эффективность_персонала where Ответсвенный = '{id_N}';", Baza);
                MySqlDataReader rdr4 = cmd4.ExecuteReader();
                while (rdr4.Read())
                {
                    k_p = Convert.ToInt32(rdr4[6]);
                }
                rdr4.Close();
                ////////////////////////////////////////////////////////////
                int rSh = Convert.ToInt32(rch);
                MySqlCommand cmd = new MySqlCommand($"UPDATE `отдел_по_продажам`.`эффективность_персонала` SET `Количество_звонков` = '{k_z+=kz}', `Размер_среднего_чека` = '{rSh}', `Количество_сделок` = '{k_s+=ks}', `Количество_успешных_сделок` = '{k_sy+=ksy}', `Количество_КП` = '{k_p+=kp}' WHERE (`Ответсвенный` = '{id_N}')", Baza);
                cmd.ExecuteNonQuery();
                kz = 0;
                rch = 0;
                ks = 0;
                ksy = 0;
                kp = 0;
            }
            
        }
        private void login_Click(object sender, EventArgs e)
        {
            if(login.ForeColor != Color.Black || login.Text == "")
            {
                login.Text = "";
                Font fnt = new Font(login.Font.FontFamily, 12.0F);
                login.Font = fnt;
                login.ForeColor = Color.Black;
            }
            if (password.Text == "" || password.ForeColor == Color.Gray)
            {
                password.ForeColor = Color.Gray;
                Font fnt1 = new Font(password.Font.FontFamily, 10.0F);
                password.Font = fnt1;
                password.Text = "Введите пароль";
                password.PasswordChar = '\0';
            }
        }

        private void password_Click(object sender, EventArgs e)
        {
            if (password.ForeColor != Color.Black || password.Text == "")
            {
                password.Text = "";
                password.PasswordChar = '*';
                Font fnt = new Font(password.Font.FontFamily, 12.0F);
                password.Font = fnt;
                password.ForeColor = Color.Black;
            }
            if (login.Text == "" || login.ForeColor == Color.Gray)
            {
                login.ForeColor = Color.Gray;
                Font fnt1 = new Font(login.Font.FontFamily, 10.0F);
                login.Font = fnt1;
                login.Text = "Введите логин";
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            menu();
        }
        string name = "", id_N = "";
        private void button1_Click(object sender, EventArgs e)
        {
            Baza.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM отдел_по_продажам.сотрудники where Логин = '{login.Text}' and Пароль = '{password.Text}'", Baza);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                MessageBox.Show($"Добро поажловать! {rdr[1]}");
                tabControl1.Visible = true;
                name = rdr[1].ToString();
                id_N = rdr[0].ToString();
                if (id_N != "5")
                {
                    tabPage7.Parent = null;
                    tabPage10.Parent = null;
                }
                else
                    tabPage4.Parent = null;
            }
            if (!rdr.HasRows)
                MessageBox.Show("Неверный логин или пароль:(");
            rdr.Close();
            zoom();
            download();
            grafics();
            Baza.Close();
        }
        string katal1 = "";
        string marka1 = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IDbox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Tbox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            Kbox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            KATbox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            OPbox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TMbox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            Cbox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IDbox2.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            FIObox2.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            Abox2.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            Tbox2.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button cl = (Button)sender;
            Baza.Open();
            switch (cl.Text)
            {
                case "Добавить":
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO `отдел_по_продажам`.`поставщики` (`ФИО`, `Телефон`, `Адрес`) VALUES ('{FIObox2.Text}', '{Tbox2.Text}', '{Abox2.Text}')", Baza);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Инфомация о поставщике добавлена !");
                    download();
                    grafics();
                    IDbox2.Text = FIObox2.Text = Abox2.Text = Tbox2.Text = "";
                    break;
                case "Изменить":
                    MySqlCommand cmd1 = new MySqlCommand($"UPDATE `отдел_по_продажам`.`поставщики` SET `ФИО` = '{FIObox2.Text}', `Телефон` = '{Tbox2.Text}', `Адрес` = '{Abox2.Text}' WHERE (`idПоставщики` = '{IDbox2.Text}')", Baza);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Данные изменены !");
                    download();
                    grafics();
                    IDbox2.Text = FIObox2.Text = Abox2.Text = Tbox2.Text = "";
                    break;
                case "Удалить":
                    DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите обновить эти данные ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MySqlCommand cmd2 = new MySqlCommand($"DELETE FROM `отдел_по_продажам`.`поставщики` WHERE (`idПоставщики` = '{IDbox2.Text}')", Baza);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Данные удалены !");
                        download();
                        grafics();
                        IDbox2.Text = FIObox2.Text = Abox2.Text = Tbox2.Text = "";
                    }
                    else if (dialogResult == DialogResult.No) ;
                    break;
            }
            Baza.Close();
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (TOVbox1.Text != "" & TOVbox1.Text != "Все товары")
            {
                dataGridView1.Rows.Clear();
                Baza.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM отдел_по_продажам.товары_view where Название = '{TOVbox1.Text}';", Baza);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6]);
                }
                rdr.Close();
                Baza.Close();
            }
            else
            {
                dataGridView1.Rows.Clear();
                Baza.Open();
                download();
                grafics();
                Baza.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (ZVbox4.Text != "" && ZVbox4.ForeColor != Color.Gray)
            {
                DialogResult dialogResult = MessageBox.Show("Дозвонились ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    ZVbox4.Text = "";
                    ZVbox4.ForeColor = Color.Gray;
                    Font fnt1 = new Font(ZVbox4.Font.FontFamily, 10.0F);
                    ZVbox4.Font = fnt1;
                    ZVbox4.Text = "Введите номер";
                    kz++;
                    Baza.Open();
                    zoom();
                    download();
                    grafics();
                    Baza.Close();
                }
                else if (dialogResult == DialogResult.No) ;
            }
            else
            {
                MessageBox.Show("Введите номер !");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button cl = (Button)sender;
            DateTime t = DateTime.Today;
            string time = t.ToString("dd/MM/yyyy");
            string tovar = ""; string vidOT = ""; string vidDO = ""; string klient = "";
            Baza.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM отдел_по_продажам.товары_view where Название = '{Tbox4.Text}' order by idТовары asc", Baza);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                tovar = rdr[0].ToString();
            }
            rdr.Close();
            /////////////////////////
            MySqlCommand cmd2 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.вид_оплаты;", Baza);
            MySqlDataReader rdr2 = cmd2.ExecuteReader();
            while (rdr2.Read())
            {
                vidOT = rdr2[0].ToString();
            }
            rdr2.Close();
            /////////////////////////
            MySqlCommand cmd3 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.вид_отправки;", Baza);
            MySqlDataReader rdr3 = cmd3.ExecuteReader();
            while (rdr3.Read())
            {
                vidDO = rdr3[0].ToString();
            }
            rdr3.Close();
            /////////////////////////
            MySqlCommand cmd5 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.сотрудники where ФИО = '{name}'", Baza);
            MySqlDataReader rdr5 = cmd5.ExecuteReader();
            while (rdr5.Read())
            {
                klient = rdr5[0].ToString();
            }
            rdr5.Close();
            switch (cl.Text)
            {
                case "Открыть сделку":
                    MySqlCommand cmd6 = new MySqlCommand($"INSERT INTO `отдел_по_продажам`.`реализация` (`Дата`, `Ответственный`, `Клиент`, `Товар`, `Стоимость_товара`, `Скидка`,  `Количество`, `Сумма_к_оплате`, `Вид_отправки`, `Вид_оплаты`, `Сдача`) VALUES ('{time}', '{klient}', '{Kbox4.Text}', '{tovar}', '{tovar}', '{SKbox4.Text}%', '{KOLbox4.Text}', '{SUMbox4.Text}', '{vidDO}', '{vidOT}', '{SDbox4.Text}')", Baza);
                    cmd6.ExecuteNonQuery();
                    MessageBox.Show("Сделка была открыта !");
                    Baza.Close();
                    Tbox4.Text = STbox4.Text = SKbox4.Text = SUMbox4.Text = SDbox4.Text = VObox4.Text = Kbox4.Text = VDbox4.Text = "";
                    panel1.Visible = false;
                    ks++;
                    Baza.Open();
                    zoom();
                    download();
                    grafics();
                    break;
                case "Отмена":
                    panel1.Visible = false;
                    break;
            }
            Baza.Close();
        }

        private void Tbox4_TextChanged(object sender, EventArgs e)
        {
            Baza.Open();
            MySqlCommand cmd3 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.товары where Название = '{Tbox4.Text}'", Baza);
            MySqlDataReader rdr3 = cmd3.ExecuteReader();
            while (rdr3.Read())
            {
                STbox4.Text = rdr3[6].ToString();
            }
            rdr3.Close();
            Baza.Close();
        }

        private void ZVbox4_Click(object sender, EventArgs e)
        {
            if (ZVbox4.ForeColor != Color.Black || ZVbox4.Text == "")
            {
                ZVbox4.Text = "";
                Font fnt = new Font(ZVbox4.Font.FontFamily, 12.0F);
                ZVbox4.Font = fnt;
                ZVbox4.ForeColor = Color.Black;
            }
            if (KPbox4.Text == "" || KPbox4.ForeColor == Color.Gray)
            {
                KPbox4.ForeColor = Color.Gray;
                Font fnt1 = new Font(KPbox4.Font.FontFamily, 10.0F);
                KPbox4.Font = fnt1;
                KPbox4.Text = "Введите E-mail";
            }
        }

        private void KPbox4_Click(object sender, EventArgs e)
        {
            if (KPbox4.ForeColor != Color.Black || KPbox4.Text == "")
            {
                KPbox4.Text = "";
                Font fnt = new Font(KPbox4.Font.FontFamily, 12.0F);
                KPbox4.Font = fnt;
                KPbox4.ForeColor = Color.Black;
            }
            if (ZVbox4.Text == "" || ZVbox4.ForeColor == Color.Gray)
            {
                ZVbox4.ForeColor = Color.Gray;
                Font fnt1 = new Font(ZVbox4.Font.FontFamily, 10.0F);
                ZVbox4.Font = fnt1;
                ZVbox4.Text = "Введите номер";
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            menu();
        }

        private void SKbox4_TextChanged(object sender, EventArgs e)
        {
            if(SKbox4.Text != "")
            {
                double sum = Convert.ToDouble(STbox4.Text);
                double skida = Convert.ToDouble(SKbox4.Text);
                double k = Convert.ToDouble(KOLbox4.Text);
                double isum = (sum * ((100 - skida) / 100))*k;
                int ISUM = Convert.ToInt32(isum);
                SUMbox4.Text = ISUM.ToString();
                SDbox4.Text = "0";
            }
            else
            {
                SUMbox4.Text = "";
                SDbox4.Text = "";
            }
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IDbox.Text = dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString();
            checkBox1.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Baza.Open();
            if(checkBox1.Checked == true)
            {
                MySqlCommand cmd = new MySqlCommand($"UPDATE `отдел_по_продажам`.`реализация` SET `Статус_оплаты` = 'Оплачено' WHERE (`idРеализация` = '{IDbox.Text}')", Baza);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Сделка получила статус 'Оплачено' !");
                zoom();
                download();
                grafics();
                IDbox.Text = "";
                checkBox1.Checked = false;
            }  
            Baza.Close();
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IDbox.Text = dataGridView6.Rows[e.RowIndex].Cells[0].Value.ToString();
            checkBox1.Checked = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Baza.Open();
            if (checkBox1.Checked == false)
            {
                MySqlCommand cmd = new MySqlCommand($"UPDATE `отдел_по_продажам`.`реализация` SET `Статус_оплаты` = 'Не оплачено' WHERE (`idРеализация` = '{IDbox.Text}')", Baza);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Сделка поменяла статус на 'Не оплачено' !");
                kz--;
                zoom();
                download();
                grafics();
                IDbox.Text = "";
            }
            Baza.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
            MailAddress kompany = new MailAddress("fushn456@gmail.com", "Компания АО НПП 'Пульсар'");
            MailAddress client = new MailAddress($"{KPbox4.Text}");
            MailMessage gmail = new MailMessage(kompany, client);
            gmail.Subject = "Коммерческое предложение";
            gmail.Body = "Здравствуйте Уважаемый клиент!" + Environment.NewLine + "Направляем вам сообщение с подробной информацией о предоставляемых товарах и параметрах нашей компании!";
            gmail.Attachments.Add(new Attachment(@"C:\Users\stepa\OneDrive\Рабочий стол\Дистант\Коммерческое предложение.docx"));
            Client.UseDefaultCredentials = false;
            Client.EnableSsl = true;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.Credentials = new System.Net.NetworkCredential(kompany.Address, "jxntqkhapdadojul");
            Client.Send(gmail);
            gmail = null;
            MessageBox.Show("Письмо было отправлено!");
            kp++;
            Baza.Open();
            zoom();
            download();
            grafics();
            Baza.Close();
        }

        private void chart3_Click(object sender, EventArgs e)
        {
            chart2.Visible = true;
            chart3.Visible = false;
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IDbox8.Text = dataGridView8.Rows[e.RowIndex].Cells[0].Value.ToString();
            FIObox8.Text = dataGridView8.Rows[e.RowIndex].Cells[1].Value.ToString();
            Lbox8.Text = dataGridView8.Rows[e.RowIndex].Cells[2].Value.ToString();
            Pbox8.Text = dataGridView8.Rows[e.RowIndex].Cells[3].Value.ToString();
            DOLbox8.Text = dataGridView8.Rows[e.RowIndex].Cells[4].Value.ToString();
            Abox8.Text = dataGridView8.Rows[e.RowIndex].Cells[5].Value.ToString();
            Tbox8.Text = dataGridView8.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string doljnost = "";
            Baza.Open();
            MySqlCommand cmd3 = new MySqlCommand($"SELECT * FROM отдел_по_продажам.должность where Название_Должности = '{DOLbox8.Text}';", Baza);
            MySqlDataReader rdr3 = cmd3.ExecuteReader();
            while (rdr3.Read())
            {
                doljnost = rdr3[0].ToString();   
            }
            rdr3.Close();
            Button cl = (Button)sender;
            switch (cl.Text)
            {
                case "Добавить сотрудника":
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO `отдел_по_продажам`.`сотрудники` (`ФИО`, `Логин`, `Пароль`, `Должность`, `Адрес`, `Телефон`) VALUES ('{FIObox8.Text}', '{Lbox8.Text}', '{Pbox8.Text}', '{doljnost}', '{Abox8.Text}', '{Tbox8.Text}');", Baza);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Сотрудник был добавлен !");
                    download();
                    grafics();
                    IDbox8.Text = FIObox8.Text = Abox8.Text = Tbox8.Text = DOLbox8.Text = Lbox8.Text = Pbox8.Text = "";
                    break;
                case "Поменять данные":
                    MySqlCommand cmd1 = new MySqlCommand($"UPDATE `отдел_по_продажам`.`сотрудники` SET `ФИО` = '{FIObox8.Text}', `Логин` = '{Lbox8.Text}', `Пароль` = '{Pbox8.Text}', `Должность` = '{doljnost}', `Адрес` = '{Abox8.Text}', `Телефон` = '{Tbox8.Text}' WHERE (`idСотрудники` = '{IDbox8.Text}')", Baza);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Инфомация была изменена !");
                    download();
                    grafics();
                    IDbox8.Text = FIObox8.Text = Abox8.Text = Tbox8.Text = DOLbox8.Text = Lbox8.Text = Pbox8.Text = "";
                    break;
                case "Уволить сотрудника":
                    MySqlCommand cmd2 = new MySqlCommand($"DELETE FROM `отдел_по_продажам`.`сотрудники` WHERE (`idСотрудники` = '{IDbox8.Text}')", Baza);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Сотрудник был удален из базы данных !");
                    download();
                    grafics();
                    IDbox8.Text = FIObox8.Text = Abox8.Text = Tbox8.Text = DOLbox8.Text = Lbox8.Text = Pbox8.Text = "";
                    break;
            }
            Baza.Close();
        }

        private void dataGridView6_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            IDbox.Text = dataGridView6.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            chart3.Visible = true;
            chart2.Visible = false;
        }
    }
}
