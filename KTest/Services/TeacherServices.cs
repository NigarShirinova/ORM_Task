using EF.Contexts;
using EF.Entities;
using KTest.Constants;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTest.Services
{
    internal class TeacherServices
    {
        public static readonly AppDbContext _context;
        static TeacherServices()
        {
            _context = new AppDbContext();
        }

        public static void GetAllTeachers()
        {
            foreach (var teacher in _context.Teachers.ToList()) 
            {
                Console.WriteLine($"Id: {teacher.Id}, Name: {teacher.Name}, Surname: {teacher.Surname}");
            }
        }
        public static void CreateTeacher()
        {
            TeacherNameInput: Messages.InputMessage("Teacher's name");
            string name = Console.ReadLine();
            if (name.IsNullOrEmpty())
            {
                Messages.InvalidInputMessage("Name");
                goto TeacherNameInput;
            }

        TeacherSurnameInput: Messages.InputMessage("Teacher's surname");
            string surname = Console.ReadLine();
            if (surname.IsNullOrEmpty())
            {
                Messages.InvalidInputMessage("Surname");
                goto TeacherSurnameInput;
            }

            Teacher teacher = new Teacher
            {
                Name = name,
                Surname = surname
            };
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            Messages.SuccessMessage("Adding Teacher");

        }
        public static void RemoveTeacher()
        {
            Messages.InputMessage("Teacher's Id");
            string input_id = Console.ReadLine();
            int id;
            bool isSucceeded = int.TryParse(input_id, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Teacher's id");
                return;
            }

            var teacher = _context.Teachers.Find(id);
            if (teacher == null || teacher.IsDeleted)
            {
                Messages.NotFoundMessage("Teacher");
                return;
            }

            teacher.IsDeleted = true;
            _context.SaveChanges();
            Messages.SuccessMessage("Teacher marked as deleted");
        }

        public static void UpdateTeacher()
        {
            GetAllTeachers();

        TeacherIdInput: Messages.InputMessage("Teacher's Id");
            string input_id = Console.ReadLine();
            int id;
            bool isSuceeded = int.TryParse(input_id, out id);
            if(!isSuceeded)
            {
                Messages.InvalidInputMessage("Teacher's id");
                goto TeacherIdInput;
            }
            var teacher = _context.Teachers.Find(id);
            if (teacher is null)
            {
                Messages.NotFoundMessage("Teacher");
                return;
            }
            Messages.WantToChangeMessage("name");
            Messages.InputMessage("Choice(y for yes, n for no)");
            var choiceInput = Console.ReadLine();
            char choice;
            isSuceeded = char.TryParse(choiceInput, out choice);
            if(!isSuceeded)
            {
                Messages.InvalidInputMessage($"Choice");
            }
            else if (isSuceeded)
            {

                if (choice == 'y')
                {
                    Messages.InputMessage("Name for changing");
                    string nameForChange = Console.ReadLine();
                    if (nameForChange.IsNullOrEmpty())
                        Messages.InvalidInputMessage("Name for change");
                    teacher.Name = nameForChange;
                    Messages.SuccessMessage("Updating teacher's name");
                }
                Messages.WantToChangeMessage("surname");
                Messages.InputMessage("Choice(y for yes, n for no)");
                choiceInput = Console.ReadLine();
                isSuceeded = char.TryParse(choiceInput, out choice);
                if (!isSuceeded)
                {
                    Messages.InvalidInputMessage($"Choice");
                }
                if (choice == 'y')
                {
                    Messages.InputMessage("Surame for changing");
                    string surnameForChange = Console.ReadLine();
                    if (surnameForChange.IsNullOrEmpty())
                        Messages.InvalidInputMessage("Surname for change");
                    teacher.Surname = surnameForChange;
                    Messages.SuccessMessage("Updating teacher's surname");
                }
                try
                {
                    _context.SaveChanges();
                }
                catch 
                {
                    Messages.ErrorOccuredMessage();
                }
            }

        }
        public static void DetailsTeacher()
        {
                GetAllTeachers();

            TeacherIdInput: Messages.InputMessage("Teacher's Id");
                string input_id = Console.ReadLine();
                int id;
                bool isSuceeded = int.TryParse(input_id, out id);
                if (!isSuceeded)
                {
                    Messages.InvalidInputMessage("Teacher's id");
                    goto TeacherIdInput;
                }
                var teacher = _context.Teachers.Find(id);
                if (teacher is null)
                {
                    Messages.NotFoundMessage("Teacher");
                    return;
                }
            Console.WriteLine($"id: {teacher.Id}, name: {teacher.Name}, surname: {teacher.Surname} ");
        }
     
    }
}
