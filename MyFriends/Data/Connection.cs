using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyFriends.Data
{
    
    class Connection
    {
        public static ObservableCollection<Friend> Friends = new ObservableCollection<Friend>();
        SqlConnection SqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\1111\source\repos\MyFriends\MyFriends\Data\FriendsBD.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand SqlCommand;

        public ObservableCollection<Friend> SelectAll()
        {
            SqlDataReader SqlDataReader = null;
            try
            {
                SqlCon.Open();
                SqlCommand = new SqlCommand("SELECT * from [Friends]", SqlCon);
                SqlDataReader = SqlCommand.ExecuteReader();
                while (SqlDataReader.Read())
                {
                    Friends.Add(new Friend
                    {
                        Id = Convert.ToInt32(SqlDataReader["Id"]),
                        Name = Convert.ToString(SqlDataReader["Name"]),
                        Surname = Convert.ToString(SqlDataReader["Surname"]),
                        Patronymic = Convert.ToString(SqlDataReader["Patronymic"]),
                        Year = Convert.ToInt32(SqlDataReader["Year"]),
                        Month = Convert.ToInt32(SqlDataReader["Month"]),
                        Day = Convert.ToInt32(SqlDataReader["Day"]),
                        Address = Convert.ToString(SqlDataReader["Address"]),
                        NumberCode = Convert.ToInt32(SqlDataReader["NumberCode"]),
                        Number = Convert.ToInt32(SqlDataReader["Number"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                SqlCon.Close();
            }

            return Friends;
        }

        public void InsertOne(Friend Friend)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                SqlCon.Open();
                SqlCommand = new SqlCommand($"insert into [Friends] (Name, Surname, Patronymic, Year, Month, Day, Address, NumberCode, Number) OUTPUT Inserted.ID values (N'{Friend.Name}',N' {Friend.Surname}',N' {Friend.Patronymic}', {Friend.Year}, {Friend.Month}, {Friend.Day},N' {Friend.Address}', {Friend.NumberCode}, {Friend.Number})", SqlCon);
                sqlDataReader = SqlCommand.ExecuteReader();
                sqlDataReader.Read();
                Friend.Id = Convert.ToInt32(sqlDataReader["id"]);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                SqlCon.Close();
            }
        }

        public void UpdateOne(Friend friend)
        {
            MessageBox.Show(friend.Id.ToString());

            try
            {
                SqlCon.Open();
                SqlCommand = new SqlCommand($"UPDATE [Friends] SET [Name] = N'{ friend.Name }', [Surname] = N'{friend.Surname}', [Patronymic] = N'{friend.Patronymic}', [Year] = {friend.Year}, [Month] = {friend.Month}, [Day] = {friend.Day}, [Address] = N'{friend.Address}', [NumberCode] = {friend.NumberCode}, [Number] = {friend.Number} WHERE [id] ={friend.Id}", SqlCon);
                SqlCommand.ExecuteNonQuery();
                SqlCon.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteById(int Id)
        {
            try
            {
                SqlCon.Open();
                SqlCommand = new SqlCommand($"DELETE FROM [Friends] WHERE [Id]={Id}", SqlCon);

                SqlCommand.ExecuteNonQuery();
                SqlCon.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
