using Student_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_MVC.DAL
{
    public class student_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["mvcConnectionstring"].ToString();



        //get all students details
        public List<student>GetAllStudents()
        {
            List<student> studentList = new List<student>();

            using(SqlConnection con = new SqlConnection(conString)) 
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPS_studentDetails";
                SqlDataAdapter sqlDA= new SqlDataAdapter(cmd);
                DataTable dtStudents = new DataTable();

                con.Open();
                sqlDA.Fill(dtStudents);
                con.Close();

                foreach(DataRow dr in dtStudents.Rows)
                {
                    studentList.Add(new student
                    {
                        studentID = Convert.ToInt32(dr["studentID"]),
                        studentName = dr["studentName"].ToString(),
                        course = dr["course"].ToString(),
                        department = dr["department"].ToString()
                    });
                }

            }

            return studentList;
        }




        //insert student details
        public bool InsertStudent(student student)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPI_studentDetails",con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@studentName", student.studentName);
                command.Parameters.AddWithValue("@course", student.course);
                command.Parameters.AddWithValue("@department", student.department);

                con.Open();
                id =  command.ExecuteNonQuery();
                con.Close();
            }
            if(id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        //get student details by ID
        public List<student> GetStudentsByID(int studentID)
        {
            List<student> studentList = new List<student>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPS_getStudentDetails";
                cmd.Parameters.AddWithValue("@studentID", studentID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtStudents = new DataTable();

                con.Open();
                sqlDA.Fill(dtStudents);
                con.Close();

                foreach (DataRow dr in dtStudents.Rows)
                {
                    studentList.Add(new student
                    {
                        studentID = Convert.ToInt32(dr["studentID"]),
                        studentName = dr["studentName"].ToString(),
                        course = dr["course"].ToString(),
                        department = dr["department"].ToString()
                    });
                }

            }

            return studentList;
        }



        //Update student details
        public bool UpdateStudent(student student)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPU_studentDetails", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@studentID", student.studentID);
                command.Parameters.AddWithValue("@studentName", student.studentName);
                command.Parameters.AddWithValue("@course", student.course);
                command.Parameters.AddWithValue("@department", student.department);

                con.Open();
                id = command.ExecuteNonQuery();
                con.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //delete student details

        public string DeleteStudentDetails(int studentID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SPD_studentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@studentID", studentID);
                cmd.Parameters.Add("@outputMessage", SqlDbType.VarChar,50).Direction = ParameterDirection.Output;


                con.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@outputMessage"].Value.ToString();
                con.Close();

            }
            return result;
        }

    }
}