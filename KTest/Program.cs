
using Azure;
using KTest.Constants;
using KTest.Services;

namespace KTest
{
    public static class Program
    {
        public static void Main()
        {
            bool continuty = true;
            while (continuty) 
            {
                ShowMenu();
                Messages.InputMessage("Choise");
                string choiceInput = Console.ReadLine();
                int choice;
                bool isSucceeded = int.TryParse(choiceInput, out choice);

                if (isSucceeded)
                {
                    switch ((Operations)choice)
                    {
                        case Operations.AllTeachers:
                            TeacherServices.GetAllTeachers();
                            break;
                        case Operations.CreateTeacher:
                            TeacherServices.CreateTeacher();
                            break;
                        case Operations.RemoveTeacher:
                            TeacherServices.RemoveTeacher();
                            break;
                        case Operations.UpdateTeacher:
                            TeacherServices.UpdateTeacher();
                            break;
                        case Operations.DetailsTeacher:
                            TeacherServices.DetailsTeacher();
                            break;
                        case Operations.CreateGroup:
                            GroupServices.CreateGroup();
                            break;
                        case Operations.UpdateGroup:
                            GroupServices.UpdateGroup();
                            break;
                        case Operations.DeleteGroup:
                            GroupServices.RemoveGroup();
                            break;
                        case Operations.DetailsGroup:
                            GroupServices.DetailsGroup();
                            break;
                        case Operations.Exit:
                            continuty = false;
                            return;
                        case Operations.CreateStudent:
                            StudentServices.CreateStudent();
                            break;
                        case Operations.DeleteStudent:
                            StudentServices.RemoveStudent();
                            break;
                        case Operations.UpdateStudent:
                            StudentServices.UpdateStudent();
                            break;
                        case Operations.AddStudentToGroup:  
                            StudentServices.AddStudentToGroup();
                            break;

                        default:
                            Messages.InvalidInputMessage("choice");
                            break;
                    }
                }
                else
                {
                    Messages.InvalidInputMessage("choice");
                }
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("------MENU------");
            Console.WriteLine("1.All teachers");
            Console.WriteLine("2.Add teacher");
            Console.WriteLine("3.Delete teacher");
            Console.WriteLine("4.Update teacher");
            Console.WriteLine("5.Details of teacher");
            Console.WriteLine("0.Exit");
        }
    }

}