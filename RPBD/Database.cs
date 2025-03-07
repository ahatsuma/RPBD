using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPBD
{
    public class Database
    {
        private MySqlConnection connection;

        public Database()
        {
            connection = new MySqlConnection("server=localhost;user=root;password=090721;database=RPBD;port=3306");
        }

        public List<Learner> GetLearners()
        {
            List<Learner> learners = new List<Learner>();

            try
            {
                connection.Open();
                string query = @"
                SELECT learners.learner_id, learners.first_name, learners.last_name, learners.group_id, study_groups.group_name 
                FROM learners 
                JOIN study_groups ON learners.group_id = study_groups.group_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Learner learner = new Learner
                    {
                        LearnerId = Convert.ToInt32(reader["learner_id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        GroupId = Convert.ToInt32(reader["group_id"]),
                        GroupName = reader["group_name"].ToString()
                    };
                    learners.Add(learner);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return learners;
        }

        public void AddLearner(string firstName, string lastName, int groupId)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO learners (first_name, last_name, group_id) VALUES (@firstName, @lastName, @groupId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@groupId", groupId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteLearner(int learnerId)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM learners WHERE learner_id = @learnerId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@learnerId", learnerId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<StudyGroup> GetGroups()
        {
            {
                List<StudyGroup> groups = new List<StudyGroup>();

                try
                {
                    connection.Open();
                    string query = "SELECT * FROM study_groups";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StudyGroup group = new StudyGroup
                        {
                            GroupId = Convert.ToInt32(reader["group_id"]),
                            GroupName = reader["group_name"].ToString()
                        };
                        groups.Add(group);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return groups;
            }

        }
    }
}
