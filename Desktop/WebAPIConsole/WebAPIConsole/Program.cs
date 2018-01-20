using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebAPIConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program newPro = new Program();
            newPro.menu();
        }

        public void menu()
        {
            while (true)
            {
                printMenu();
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid input. Enter a number from 1 to 5: ");
                }

                switch (choice)
                {
                    case 1:
                        GetRequest();
                        break;
                    case 2:
                        PostRequest();
                        break;
                    case 3:
                        PutRequest();
                        break;
                    case 4:
                        Delete();
                        break;
                    case 5:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
        }
        public static void GetRequest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58774/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Student[]>();
                    readTask.Wait();

                    var students = readTask.Result;
                    Console.WriteLine("ID  NAME");
                    foreach (var student in students)
                    {
                        Console.WriteLine(student.StudentID + "   " + student.StudentName);
                    }
                }
            }
        }

        public static void PostRequest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58774/api/");
                Console.WriteLine("Input the name of the new student.");
                string name = Console.ReadLine();
                var student = new Student() { StudentName = name };

                var postTask = client.PostAsJsonAsync<Student>("student", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    var insertedStudent = readTask.Result;

                    Console.WriteLine("Student {0} inserted with id: {1}", student.StudentName, student.StudentID);
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
        }

        public static void PutRequest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58774/api/");
                Console.WriteLine("Input Student ID of the student you want to edit");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input the updated name.");
                string name = Console.ReadLine();

                var student = new Student() { StudentName = name, StudentID = id };

                var putTask = client.PutAsJsonAsync<Student>("student?id=" + id, student);
                Console.WriteLine(student.StudentID);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine("It was updated");
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
        }

        public static void Delete()
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:58774/api/");
                Console.WriteLine("Input the Student ID of the student you would like to delete.");
                int id = Convert.ToInt32(Console.ReadLine());
                var delete = client.DeleteAsync("student/" + id);
                delete.Wait();
                var result = delete.Result;
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine("It was deleted");
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
        }

        public Boolean Exit()
        {
            return false;
        }

        public void printMenu()
        {
            Console.WriteLine("1. Show Students");
            Console.WriteLine("2. Add Students");
            Console.WriteLine("3. Edit Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
        }
    }
}
