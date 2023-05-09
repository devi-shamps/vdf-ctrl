using MySql.Data.MySqlClient;
using vdfClasses.Business;

User initUser = new User(1, "admin");
initUser.password = BCrypt.Net.BCrypt.HashPassword("admin");

MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=vdf;port=3306;password=&6HAUTdanslaFauré;");
conn.Open();
MySqlCommand cmd = new MySqlCommand($"INSERT INTO user (id, name, password) VALUES ({initUser.id}, '{initUser.login}', '{initUser.password}')", conn);
cmd.ExecuteNonQuery();

cmd = new MySqlCommand($"SELECT * FROM user WHERE id = {initUser.id}", conn);
MySqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(reader.GetInt32(0) + reader.GetString(1) + reader.GetString(2));
}
conn.Close();





