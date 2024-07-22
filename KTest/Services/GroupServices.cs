using EF.Contexts;
using KTest.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EF.Entities;

namespace KTest.Services
{
    internal class GroupServices
    {
        private static readonly AppDbContext _context;

        static GroupServices()
        {
            _context = new AppDbContext();
        }

        public static void GetAllGroups()
        {
            foreach (var group in _context.Groups.ToList())
            {
                Console.WriteLine($"Id: {group.Id}, Name: {group.Name}, Limit: {group.Limit}, TeacherId: {group.TeacherId}");
            }
        }

        public static void CreateGroup()
        {
        GroupNameInput:
            Messages.InputMessage("Group's name");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Messages.InvalidInputMessage("Name");
                goto GroupNameInput;
            }

        GroupLimitInput:
            Messages.InputMessage("Group's limit");
            string limitInput = Console.ReadLine();
            int limit;
            bool isSucceeded = int.TryParse(limitInput, out limit);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Limit");
                goto GroupLimitInput;
            }

        TeacherIdInput:
            Messages.InputMessage("Teacher's Id for the group");
            string teacherIdInput = Console.ReadLine();
            int teacherId;
            isSucceeded = int.TryParse(teacherIdInput, out teacherId);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Teacher Id");
                goto TeacherIdInput;
            }

            var teacher = _context.Teachers.Find(teacherId);
            if (teacher == null || teacher.IsDeleted)
            {
                Messages.NotFoundMessage("Teacher");
                goto TeacherIdInput;
            }

            EF.Entities.Group group = new EF.Entities.Group
            {
                Name = name,
                Limit = limit,
                TeacherId = teacherId
            };

            _context.Groups.Add(group);
            _context.SaveChanges();
            Messages.SuccessMessage("Adding Group");
        }

        public static void RemoveGroup()
        {
            Messages.InputMessage("Group's Id");
            string input_id = Console.ReadLine();
            int id;
            bool isSucceeded = int.TryParse(input_id, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Group's id");
                return;
            }

            var group = _context.Groups.Find(id);
            if (group == null || group.IsDeleted)
            {
                Messages.NotFoundMessage("Group");
                return;
            }

            group.IsDeleted = true;
            _context.SaveChanges();
            Messages.SuccessMessage("Group marked as deleted");
        }

        public static void UpdateGroup()
        {
            GetAllGroups();

        GroupIdInput:
            Messages.InputMessage("Group's Id");
            string input_id = Console.ReadLine();
            int id;
            bool isSucceeded = int.TryParse(input_id, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Group's id");
                goto GroupIdInput;
            }

            var group = _context.Groups.Find(id);
            if (group is null)
            {
                Messages.NotFoundMessage("Group");
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
                    group.Name = nameForChange;
                    Messages.SuccessMessage("Updating group's name");
                }
            }

            Messages.WantToChangeMessage("limit");
            Messages.InputMessage("Choice (y for yes, n for no)");
            choiceInput = Console.ReadLine();
            isSucceeded = char.TryParse(choiceInput, out choice);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Choice");
            }
            else if (isSucceeded && choice == 'y')
            {
                Messages.InputMessage("Limit for changing");
                string limitForChangeInput = Console.ReadLine();
                int limitForChange;
                isSucceeded = int.TryParse(limitForChangeInput, out limitForChange);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Limit for change");
                }
                else
                {
                    group.Limit = limitForChange;
                    Messages.SuccessMessage("Updating group's limit");
                }
            }

            Messages.WantToChangeMessage("teacher Id");
            Messages.InputMessage("Choice (y for yes, n for no)");
            choiceInput = Console.ReadLine();
            isSucceeded = char.TryParse(choiceInput, out choice);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Choice");
            }
            else if (isSucceeded && choice == 'y')
            {
                Messages.InputMessage("Teacher Id for changing");
                string teacherIdForChangeInput = Console.ReadLine();
                int teacherIdForChange;
                isSucceeded = int.TryParse(teacherIdForChangeInput, out teacherIdForChange);
                if (!isSucceeded)
                {
                    Messages.InvalidInputMessage("Teacher Id for change");
                }
                else
                {
                    var teacher = _context.Teachers.Find(teacherIdForChange);
                    if (teacher == null || teacher.IsDeleted)
                    {
                        Messages.NotFoundMessage("Teacher");
                    }
                    else
                    {
                        group.TeacherId = teacherIdForChange;
                        Messages.SuccessMessage("Updating group's teacher Id");
                    }
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

        public static void DetailsGroup()
        {
            GetAllGroups();

        GroupIdInput:
            Messages.InputMessage("Group's Id");
            string input_id = Console.ReadLine();
            int id;
            bool isSucceeded = int.TryParse(input_id, out id);
            if (!isSucceeded)
            {
                Messages.InvalidInputMessage("Group's id");
                goto GroupIdInput;
            }

            var group = _context.Groups.Find(id);
            if (group is null)
            {
                Messages.NotFoundMessage("Group");
                return;
            }

            Console.WriteLine($"Id: {group.Id}, Name: {group.Name}, Limit: {group.Limit}, TeacherId: {group.TeacherId}");
        }
    }
}
