using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace Laba7
{
    class Car
    {
        public int carId;
        public string brand;
        public string model;
        private string color;
        public int year;

        private static SqlConnection sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["AvtoConnectionString"].ConnectionString);

        public string Color
        {
            get
            {
                if (color == null)
                {
                    return "Unknown";
                }
                else
                {
                    return color;
                }
            }

            set => color = value;
        }

        public override string ToString()
        {
            return string.Format("id = {0} - Brand: {1}, Model: {2}, Year: {3}, Color: {4}", carId, brand, model, year, Color);
        }

        public static List<Car> GetAllCars()
        {
            List<Car> list = new List<Car>();
            string command = "SELECT id, brand, model, year, color FROM Car";
            SqlCommand getAllCommand = new SqlCommand(command, sqlConnection);
            sqlConnection.Open();
            var reader = getAllCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(new Car()
                    {
                        carId = reader.GetInt32(0),
                        brand = reader.GetString(1),
                        model = reader.GetString(2),
                        year = reader.GetInt32(3),
                        color = reader.GetValue(4) as string
                    });
                }
            }
            sqlConnection.Close();
            return list;
        }

        public void InsertCar()
        {
            string command = "INSERT INTO Car (brand, model, year, color) VALUES (@brand, @model, @year, @color)";
            SqlCommand insertCommand = new SqlCommand(command, sqlConnection);
            insertCommand.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("brand", brand),
                new SqlParameter("model", model),
                new SqlParameter("year", year),
                new SqlParameter("color", color)
            });
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static Car GetCarById(int id)
        {
            Car car = new Car();
            string command = "SELECT id, brand, model, year, color FROM Car WHERE id = @id";
            SqlCommand getByIdCommand = new SqlCommand(command, sqlConnection);
            getByIdCommand.Parameters.Add(new SqlParameter("id", id));

            sqlConnection.Open();

            var reader = getByIdCommand.ExecuteReader();
            if (reader.HasRows)
            {
                car.carId = reader.GetInt32(0);
                car.brand = reader.GetString(1);
                car.model = reader.GetString(2);
                car.year = reader.GetInt32(3);
                car.color = reader.GetString(4);
            }
            sqlConnection.Close();
            return car;
        }

        public void UpdateCar()
        {
            string command = "UPDATE Car SET brand = @brand, model = @model, year = @year, color = @color WHERE id = @id";
            SqlCommand updateCommand = new SqlCommand(command, sqlConnection);
            updateCommand.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("id", carId),
                new SqlParameter("brand", brand),
                new SqlParameter("model", model),
                new SqlParameter("year", year),
                new SqlParameter("color", color)
            });
            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }


        public static void DeleteCarById(int id)
        {
            Car car = new Car();
            string command = "DELETE FROM Car WHERE id = @id";
            SqlCommand getByIdCommand = new SqlCommand(command, sqlConnection);
            getByIdCommand.Parameters.Add(new SqlParameter("id", id));

            sqlConnection.Open();
            getByIdCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
