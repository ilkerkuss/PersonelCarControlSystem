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
using System.Data.SqlClient;


namespace PersonelCarControlSystem
{
    public partial class Form1 : Form
    {

        public SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ilker\source\repos\PersonelCarControlSystem\PersonelCarControlSystem\Company.mdf;Integrated Security=True;MultipleActiveResultSets=True");
        public SqlCommand cmd = new SqlCommand();
        public SqlCommand cmd2 = new SqlCommand();

        public string selectedCar;
        public string selectedCarKey;
        public string selectedPersonel;
        public string selectedPersonelKey;
        public DateTime selectedDate;

        public string newPersonelTc;
        public string newPersonelName;
        public string newPersonelSurname;
        public int newPersonelTelNo;

        public string newCarPlate;
        public string newCarBrand;
        public string newCarModel;
        public string newCarChassis;

        public Form1()
        {
            InitializeComponent();

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ilker\source\repos\PersonelCarControlSystem\PersonelCarControlSystem\Company.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            using (cnn)
            {
                SqlCommand personelQuery = new SqlCommand();
                personelQuery.Connection = cnn;
                Console.WriteLine(selectedPersonelKey);
                personelQuery.CommandText = "SELECT transportedItem,carTakenDate FROM Transportation WHERE driverTc=@selectedPersonelKey";
                personelQuery.Parameters.AddWithValue("@selectedPersonelKey", selectedPersonelKey);
                cnn.Open();
                SqlDataReader dr2 = personelQuery.ExecuteReader();
                

                StringBuilder dataBuilder = new StringBuilder();


                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        string transportationData = dr2["transportedItem"].ToString() + " " + dr2["carTakenDate"].ToString();
                        dataBuilder.AppendLine(transportationData);

                    }


                    MessageBox.Show(dataBuilder.ToString(), "İstenenler");
                }
                cnn.Close();
            }
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (cnn)
            {
                
                //SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "SELECT tc,personelName,personelSurname FROM Personel";
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    var isim = dr["tc"].ToString()+" "+ dr["personelName"].ToString() + " " + dr["personelSurname"].ToString();
                    listBox1.Items.Add(isim);
                    selectedPersonelKey = dr["tc"].ToString();
                }
                //cnn.Close();

                
                cmd2.Connection = cnn;
                cmd2.CommandText = "SELECT carPlate,carModel,carBrand FROM Car";
                //cnn.Open();

                dr = cmd2.ExecuteReader();


                while (dr.Read())
                {
                    var car = dr["carPlate"].ToString()+" "+dr["carBrand"].ToString() + " " + dr["carModel"].ToString();
                    listBox2.Items.Add(car);
                    selectedCarKey = dr["carPlate"].ToString();
                }

                cnn.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ilker\source\repos\PersonelCarControlSystem\PersonelCarControlSystem\Company.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            using (cnn)
            {
                SqlCommand carQuery = new SqlCommand();
                carQuery.Connection = cnn;
                Console.WriteLine(selectedCarKey);
                
                carQuery.CommandText = "SELECT driverTc,transportedItem,carTakenDate FROM Transportation WHERE carPlate=@carPlate";
                carQuery.Parameters.AddWithValue("@carPlate",selectedCarKey);
                cnn.Open();
                SqlDataReader dr3 = carQuery.ExecuteReader();
                StringBuilder dataBuilder = new StringBuilder();


                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        string transportationData = dr3["driverTc"].ToString()+" "+dr3["transportedItem"].ToString() + " " + dr3["carTakenDate"].ToString();
                        dataBuilder.AppendLine(transportationData);

                    }


                    MessageBox.Show(dataBuilder.ToString(), "İstenenler");
                }
                cnn.Close();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //seçilen araç plakası alma
            selectedCar = listBox2.SelectedItem.ToString();
            int spaceIndex = selectedCar.IndexOf(' ');
            
            for (int i = 0; i < spaceIndex; i++)
            {
                if (selectedCarKey != null && selectedCarKey.Length >= spaceIndex)
                {
                    selectedCarKey = "";
                }

                selectedCarKey += listBox2.SelectedItem.ToString()[i];
            }
            Console.WriteLine(selectedCarKey);
            Console.WriteLine(listBox2.SelectedItem.ToString());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //seçilen personel tc alma
            selectedPersonel = listBox1.SelectedItem.ToString();
            int spaceIndex = selectedPersonel.IndexOf(' ');

            for (int i = 0; i < spaceIndex; i++)
            {
                if (selectedPersonelKey!=null && selectedPersonelKey.Length>=spaceIndex)
                {
                    selectedPersonelKey = "";
                }

                selectedPersonelKey += listBox1.SelectedItem.ToString()[i];
            }
            Console.WriteLine(selectedPersonelKey);
            Console.WriteLine(listBox1.SelectedItem.ToString());
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            selectedDate = monthCalendar1.SelectionStart;
            Console.WriteLine(selectedDate);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ilker\source\repos\PersonelCarControlSystem\PersonelCarControlSystem\Company.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            using (cnn)
            {
                SqlCommand carQuery = new SqlCommand();
                carQuery.Connection = cnn;
                Console.WriteLine(selectedCarKey);

                carQuery.CommandText = "SELECT driverTc,transportedItem FROM Transportation WHERE carTakenDate=@carTakenDate";
                carQuery.Parameters.AddWithValue("@carTakenDate", selectedDate);
                cnn.Open();
                SqlDataReader dr3 = carQuery.ExecuteReader();
                StringBuilder dataBuilder = new StringBuilder();


                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        string transportationData = dr3["driverTc"].ToString() + " " + dr3["transportedItem"].ToString();
                        dataBuilder.AppendLine(transportationData);

                    }


                    MessageBox.Show(dataBuilder.ToString(), "İstenenler");
                }
                cnn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ilker\source\repos\PersonelCarControlSystem\PersonelCarControlSystem\Company.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            using (cnn)
            {
                SqlCommand personelAddQuery = new SqlCommand();
                personelAddQuery.Connection = cnn;
                Console.WriteLine(selectedCarKey);

                personelAddQuery.CommandText = "INSERT INTO Personel(tc,personelName,personelSurname,personelTelNo) VALUES (@tc,@personelName,@personelSurname,@personelTelNo)";
                personelAddQuery.Parameters.AddWithValue("@tc", newPersonelTc);
                personelAddQuery.Parameters.AddWithValue("@personelName", newPersonelName);
                personelAddQuery.Parameters.AddWithValue("@personelSurname", newPersonelSurname);
                personelAddQuery.Parameters.AddWithValue("@personelTelNo", newPersonelTelNo);
                cnn.Open();

                int rowsAffected = personelAddQuery.ExecuteNonQuery();
                StringBuilder databuilder = new StringBuilder();
                databuilder.AppendLine(newPersonelTc);
                databuilder.AppendLine(newPersonelName);
                databuilder.AppendLine(newPersonelSurname);
                databuilder.AppendLine(newPersonelTelNo.ToString());

                if (rowsAffected > 0)
                {
                    MessageBox.Show(databuilder.ToString(),"Eklenen Personel");
                    var pers = newPersonelTc+" "+newPersonelName+" "+newPersonelSurname;
                    listBox2.Items.Add(pers);
                }
                cnn.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            newPersonelTc = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            newPersonelName = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            newPersonelSurname = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            newPersonelTelNo =Int32.Parse(textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            newCarPlate = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            newCarBrand = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            newCarModel = textBox7.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ilker\source\repos\PersonelCarControlSystem\PersonelCarControlSystem\Company.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            using (cnn)
            {
                SqlCommand personelAddQuery = new SqlCommand();
                personelAddQuery.Connection = cnn;
                Console.WriteLine(selectedCarKey);

                personelAddQuery.CommandText = "INSERT INTO Car(carPlate,carBrand,carModel,chasisNo) VALUES (@carPlate,@carName,@carModel,@chasisNo)";
                personelAddQuery.Parameters.AddWithValue("@carPlate", newCarPlate);
                personelAddQuery.Parameters.AddWithValue("@carName", newCarBrand);
                personelAddQuery.Parameters.AddWithValue("@carModel", newCarModel);
                personelAddQuery.Parameters.AddWithValue("@chasisNo", newCarChassis);
                cnn.Open();

                int rowsAffected = personelAddQuery.ExecuteNonQuery();
                StringBuilder databuilder = new StringBuilder();
                databuilder.AppendLine(newCarPlate);
                databuilder.AppendLine(newCarBrand);
                databuilder.AppendLine(newCarModel);
                databuilder.AppendLine(newCarChassis);

                if (rowsAffected > 0)
                {
                    MessageBox.Show(databuilder.ToString(), "Eklenen Araç");
                    var car = newCarPlate+" "+newCarBrand+" "+newCarModel;
                    listBox2.Items.Add(car);
                }
                cnn.Close();
            }


        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            newCarChassis = textBox8.Text;
        }
    }
}
