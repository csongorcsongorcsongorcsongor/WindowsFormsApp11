using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp11
{
    class dbHandler : IdbHandler
    {
        MySqlConnection connection;
        string tableName = "users";
        public dbHandler()
        {
            string user = "root";
            string password = "";
            string dbName = "kolbasz";
            string host = "localhost";
            string connectionString = $"host={host};username={user};password={password};database={dbName};";
            connection = new MySqlConnection(connectionString);
            
        }


        public void DeleteAll()
        {
            try
            {
                connection.Open();
                string query = $"delete from {tableName}";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        public void DeleteOne(User oneUser)
        {
            try
            {
                connection.Open();
                string query = $"delete from {tableName} where id = {oneUser.id}";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        public void InsertOne(User oneUser)
        {
            try
            {
                connection.Open();
                string query = $"insert into {tableName} (id, username, password, points) values ('{oneUser.id}','{oneUser.username}','{oneUser.password}','{oneUser.points}');";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ReadAll()
        {
            try
            {
                connection.Open();
                string query = $"Select * from {tableName}";
                MySqlCommand command = new MySqlCommand(query,connection);
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    User oneUser = new User();
                    oneUser.id = read.GetInt32(read.GetOrdinal("id"));
                    oneUser.username = read.GetString(read.GetOrdinal("username"));
                    oneUser.password = read.GetString(read.GetOrdinal("password"));
                    oneUser.points = read.GetInt32(read.GetOrdinal("points"));
                    User.allUser.Add(oneUser);
                }
                read.Close();
                command.Dispose();
                connection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void UpdateOne(User oneUser)
        {
            try
            {
                connection.Open();
                string query = $"Update {tableName} set points = {oneUser.points} where id = {oneUser.id}";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
    }
}
