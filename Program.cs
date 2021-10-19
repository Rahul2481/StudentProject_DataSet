using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.ConstrainedExecution;

namespace Midterm_Part1
{
    class Program
    {
        static void Main(string[] args)
        {

            //Creatind Dataset
            DataSet dsStudentProject = new DataSet("StudentProjectDS");

            //Creating Tables
            DataTable dtStudents = new DataTable("Students");
            DataTable dtProjects = new DataTable("Projects");
            DataTable dtProjectAssignments = new DataTable("ProjectAssignments");

            //Adding it to the Dataset
            dsStudentProject.Tables.Add(dtStudents);
            dsStudentProject.Tables.Add(dtProjects);
            dsStudentProject.Tables.Add(dtProjectAssignments);

            //Table Students
            dtStudents.Columns.Add("StudentNumber",typeof(Int32));
            dtStudents.Columns.Add("FirstName",typeof(string));
            dtStudents.Columns.Add("LastName", typeof(string));
            dtStudents.Columns.Add("Password", typeof(string));
            dtStudents.PrimaryKey = new DataColumn[] { dtStudents.Columns["StudentNumber"] };

            Console.WriteLine("\n\n\n");
            foreach (DataColumn dc in dtStudents.Columns)
            {
                Console.WriteLine("\n\t" + dc.ColumnName + "\t\t" + dc.DataType);
            }

            dtStudents.Rows.Add(1111111, "Mary", "Brown", "mary1111");
            dtStudents.Rows.Add(2222222, "Mary", "Green", "mary2222");
            dtStudents.Rows.Add(3333333, "Thomas", "Moore", "thomas3333");

            foreach (DataRow dr in dtStudents.Rows)
            {
                Console.WriteLine("\n\t\t" + dr["StudentNumber"] + "\t\t" + dr["FirstName"] + "\t\t" + dr["LastName"] + "\t\t" + dr["Password"]);
            }

            //Table Projects
            dtProjects.Columns.Add("ProjectCode", typeof(string));
            dtProjects.Columns.Add("ProjectTitle", typeof(string));
            dtProjects.Columns.Add("DueDate", typeof(DateTime));
            dtProjects.PrimaryKey = new DataColumn[] { dtProjects.Columns["ProjectCode"] };

            Console.WriteLine("\n\n\n");
            foreach (DataColumn dc in dtProjects.Columns)
            {
                Console.WriteLine("\n\t" + dc.ColumnName + "\t\t" + dc.DataType);
            }

            dtProjects.Rows.Add("PRJ101", "Shopping Cart in C#","2020-12-20");
            dtProjects.Rows.Add("PRJ102", "Hi-Tech Online Order Management","2020-12-30");


            foreach (DataRow dr in dtProjects.Rows)
            {
                Console.WriteLine("\n\t\t" + dr["ProjectCode"] + "\t\t" + dr["ProjectTitle"]+"\t\t" + dr["DueDate"]);
            }


            //Table ProjectAssignments
            dtProjectAssignments.Columns.Add("StudentNumber",typeof(Int32));
            dtProjectAssignments.Columns.Add("ProjectCode",typeof(string));
            dtProjectAssignments.Columns.Add("AssignmentDate",typeof(DateTime));
            dtProjectAssignments.Columns.Add("SubmittedDate", typeof(DateTime));
            dtProjectAssignments.PrimaryKey = new DataColumn[] { dtProjectAssignments.Columns["StudentNumber"],dtProjectAssignments.Columns["ProjectCode"] };

            DataRelation drSoPA = new DataRelation("StoPA", dtStudents.Columns["StudentNumber"], dtProjectAssignments.Columns["StudentNumber"]);
            DataRelation drPoPA = new DataRelation("PtoPA", dtProjects.Columns["ProjectCode"], dtProjectAssignments.Columns["ProjectCode"]);

            dsStudentProject.Relations.Add(drSoPA);
            dsStudentProject.Relations.Add(drPoPA);

            Console.WriteLine("\n\n\n");
            foreach (DataColumn dc in dtProjectAssignments.Columns)
            {
                Console.WriteLine("\n\t" + dc.ColumnName + "\t\t" + dc.DataType);
            }


            dtProjectAssignments.Rows.Add(1111111, "PRJ102", "2020-09-22", "2020-12-15");
            dtProjectAssignments.Rows.Add(1111111, "PRJ101", "2020-09-22", "2020-12-15");
            dtProjectAssignments.Rows.Add(3333333, "PRJ101", "2020-09-22", "2020-12-10");
            dtProjectAssignments.Rows.Add(2222222, "PRJ101", "2020-09-22", "2020-12-10");

            foreach (DataRow dr in dtProjectAssignments.Rows)
            {
                Console.WriteLine("\n\t\t" + dr["ProjectCode"] + "\t\t" + dr["StudentNumber"] + "\t\t" + dr["AssignmentDate"] + "\t\t" + dr["SubmittedDate"]);
            }
            //Finding
            DataRow drProject = dtProjects.Rows.Find("PRJ101");
            Console.WriteLine("\n\n\t\t\tProject List");
            Console.WriteLine("\n\n\t\t" + "Project Code: " + drProject["ProjectCode"] + ", " + drProject["ProjectTitle"]);
            Console.WriteLine("\n\n\n\tProject Code" + "\t\tStudent Number");

            foreach (DataRow dr in dtProjectAssignments.Rows)
            {
                if (dr["ProjectCode"].ToString() == "PRJ101")
                {
                    DataRow drProjectAssignments = dtStudents.Rows.Find(dr["StudentNumber"]);

                    Console.WriteLine("\n\n\t " + dr["ProjectCode"] + "\t\t " + drProjectAssignments["StudentNumber"]);
                }
            }

            Console.WriteLine("\n\n\tPress Any key to exit the Program...");
            Console.ReadKey();
        }

    }
}
