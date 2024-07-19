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
        internal class StudentServices
        {
            private static readonly AppDbContext _context;

            static StudentServices()
            {
                _context = new AppDbContext();
            }

            public static void GetAllStudents()
            {
                foreach (var student in _context.Students.ToList())
                {
                    Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Email: {student.Email}, BirthDate: {student.BirthDate.ToShortDateString()}, GroupId: {student.GroupId}");
                }
            }

            public static void CreateStudent()
            {
            StudentNameInput:
                Messages.InputMessage("Student's name");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Messages.InvalidInputMessage("Name");
                    goto StudentNameInput;
                }

            StudentSurnameInput:
                Messages.InputMessage("Student's surname");
                string surname = Console.ReadLine();
                if (string.IsNullOrEmpty(surname))
                {
                    Messages.InvalidInputMessage("Surname");
                    goto StudentSurnameInput;
                }

            StudentEmailInput:
                Messages.InputMessage("Student's email");
                string email = Console.ReadLine();
                if (string.IsNullOrEmpty(email))
                {
                    Messages.InvalidInputMessage("Email");
                    goto StudentEmailInput;
                }

            StudentBirthDateInput:
                Messages.InputMessage("Student's birth date (yyyy-MM-dd)");
                string birthDateInput = Console.ReadLine();
                DateTime birthDate;
                bool isSucceeded = DateTime.TryParse(birthDateInput, out birthDate);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Birth Date");
                    goto StudentBirthDateInput;
                }

            GroupIdInput:
                Messages.InputMessage("Group's Id");
                string groupIdInput = Console.ReadLine();
                int groupId;
                isSucceeded = int.TryParse(groupIdInput, out groupId);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Group Id");
                    goto GroupIdInput;
                }

                var group = _context.Groups.Find(groupId);
                if (group == null || group.IsDeleted)
                {
                    Messages.NotFoundMessage("Group");
                    goto GroupIdInput;
                }

                Student student = new Student
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    BirthDate = birthDate,
                    GroupId = groupId
                };

                _context.Students.Add(student);
                _context.SaveChanges();
                Messages.SuccessMessage("Adding Student");
            }

            public static void RemoveStudent()
            {
                Messages.InputMessage("Student's Id");
                string input_id = Console.ReadLine();
                int id;
                bool isSucceeded = int.TryParse(input_id, out id);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Student's id");
                    return;
                }

                var student = _context.Students.Find(id);
                if (student == null || student.IsDeleted)
                {
                    Messages.NotFoundMessage("Student");
                    return;
                }

                student.IsDeleted = true;
                _context.SaveChanges();
                Messages.SuccessMessage("Student marked as deleted");
            }

            public static void UpdateStudent()
            {
                GetAllStudents();

            StudentIdInput:
                Messages.InputMessage("Student's Id");
                string input_id = Console.ReadLine();
                int id;
                bool isSucceeded = int.TryParse(input_id, out id);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Student's id");
                    goto StudentIdInput;
                }

                var student = _context.Students.Find(id);
                if (student is null)
                {
                    Messages.NotFoundMessage("Student");
                    return;
                }

                Messages.WantToChangeMessage("name");
                Messages.InputMessage("Choice (y for yes, n for no)");
                var choiceInput = Console.ReadLine();
                char choice;
                isSucceeded = char.TryParse(choiceInput, out choice);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Choice");
                }
                else if (isSucceeded && choice == 'y')
                {
                    Messages.InputMessage("Name for changing");
                    string nameForChange = Console.ReadLine();
                    if (string.IsNullOrEmpty(nameForChange))
                    {
                        Messages.InvalidInputMessage("Name for change");
                    }
                    else
                    {
                        student.Name = nameForChange;
                        Messages.SuccessMessage("Updating student's name");
                    }
                }

                Messages.WantToChangeMessage("surname");
                Messages.InputMessage("Choice (y for yes, n for no)");
                choiceInput = Console.ReadLine();
                isSucceeded = char.TryParse(choiceInput, out choice);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Choice");
                }
                else if (isSucceeded && choice == 'y')
                {
                    Messages.InputMessage("Surname for changing");
                    string surnameForChange = Console.ReadLine();
                    if (string.IsNullOrEmpty(surnameForChange))
                    {
                        Messages.InvalidInputMessage("Surname for change");
                    }
                    else
                    {
                        student.Surname = surnameForChange;
                        Messages.SuccessMessage("Updating student's surname");
                    }
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

            public static void DetailsStudent()
            {
                GetAllStudents();

            StudentIdInput:
                Messages.InputMessage("Student's Id");
                string input_id = Console.ReadLine();
                int id;
                bool isSucceeded = int.TryParse(input_id, out id);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Student's id");
                    goto StudentIdInput;
                }

                var student = _context.Students.Find(id);
                if (student is null)
                {
                    Messages.NotFoundMessage("Student");
                    return;
                }

                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Email: {student.Email}, BirthDate: {student.BirthDate.ToShortDateString()}, GroupId: {student.GroupId}");
            }

            public static void AddStudentToGroup()
            {
                Messages.InputMessage("Student's Id");
                string studentIdInput = Console.ReadLine();
                int studentId;
                bool isSucceeded = int.TryParse(studentIdInput, out studentId);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Student's Id");
                    return;
                }

                var student = _context.Students.Find(studentId);
                if (student == null || student.IsDeleted)
                {
                    Messages.NotFoundMessage("Student");
                    return;
                }

                Messages.InputMessage("New Group's Id");
                string groupIdInput = Console.ReadLine();
                int groupId;
                isSucceeded = int.TryParse(groupIdInput, out groupId);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Group Id");
                    return;
                }

                var group = _context.Groups.Find(groupId);
                if (group == null || group.IsDeleted)
                {
                    Messages.NotFoundMessage("Group");
                    return;
                }

                student.GroupId = groupId;
                try
                {
                    _context.SaveChanges();
                    Messages.SuccessMessage("Student added to the group");
                }
                catch
                {
                    Messages.ErrorOccuredMessage();
                }
            }
        }
    }


