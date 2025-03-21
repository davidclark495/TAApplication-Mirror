﻿/*
	Author: Robert Davidson
	Partner: David Clark
	Date: 09/23/2022
	Course: CS 4540, University of Utah, School of Computing
	Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.
	
  	I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from 
  	another source.  Any references used in the completion of the assignment are cited in my README file.

	I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from
	another source. Any references used in the completion of the assignment are cited in my README file.

	File Contents

		C# representation of Database. Contains method for seeding users and roles. 
*/

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Xml.Linq;
using TAApplication.Areas.Identity.Data;
using TAApplication.Models;

namespace TAApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        // DB Tables
        public DbSet<Application> Applications { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<EnrollmentRecord> EnrollmentRecords { get; set; }

        // Misc. Properties 
        private IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task InitializeUsers(UserManager<TAUser> um, RoleManager<IdentityRole> rm)
        {
            // Look for any TAUsers.
            if (um.Users.Any<TAUser>())
            {
                return;   // DB has been seeded
            }

            // Code snippet: https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users
            string[] roleNames = { "Admin", "Professor", "Applicant" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await rm.RoleExistsAsync(roleName);
                if (!roleExist)
                    await rm.CreateAsync(new IdentityRole(roleName));
            }
            // End code snippet

            // Adds Applicants to db and assigns role
            var users = new TAUser[]
            {
                new TAUser{Name="Jill Germain",Unid=7654321,ReferredTo="Pro",Email="professor@utah.edu"},
                new TAUser{Name="Jim Germain",Unid=1234567,ReferredTo="Admin Dude",Email="admin@utah.edu"},
                new TAUser{Name="User 0",Unid=0000000,ReferredTo="U0",Email="u0000000@utah.edu"},
                new TAUser{Name="User 1",Unid=0000001,ReferredTo="U1",Email="u0000001@utah.edu"},
                new TAUser{Name="User 2",Unid=0000002,ReferredTo="U2",Email="u0000002@utah.edu"}
            };
            foreach (TAUser user in users)
            {
                user.UserName = user.Email;
                user.EmailConfirmed = true;
                await um.CreateAsync(user, "123ABC!@#def");
                if (user.Unid == 1234567)
                    await um.AddToRoleAsync(user, "Admin");
                else if (user.Unid == 7654321)
                    await um.AddToRoleAsync(user, "Professor");
                else
                    await um.AddToRoleAsync(user, "Applicant");
            }
            //await um.CreateAsync(new TAUser { Name = "User 2", Unid = 0000002, ReferredTo = "U2", Email = "u0000002@utah.edu", UserName = "u0000002@utah.edu" }, "123ABC!@#def");
            this.SaveChanges();
        }

        public async Task InitializeApplications(UserManager<TAUser> um)
        {
            if (this.Applications.Any<Application>())
            {
                return;   // DB has been seeded
            }
            TAUser user0 = await um.FindByEmailAsync("u0000000@utah.edu");
            TAUser user1 = await um.FindByEmailAsync("u0000001@utah.edu");
            var apps = new Application[]
            {
                new Application
                {
                    PursuingDegree = TAApplication.Models.DegreeLevel.PhD,
                    Program = "CS",
                    GPA = 5.0,
                    HoursWanted = 20,
                    EarlyAvailability = true,
                    SemestersCompletedAtUtah = 400,
                    PersonalStatement = "Yes, I am applicant zero.",
                    TransferSchool = "Harvard Law",
                    LinkedInURL = "https://linkedin.com/BillGates",
                    ResumeFilename = "BillGates_Resume_Fall22.pdf",
                    TAUser = user0
                },
                new Application
                {
                    PursuingDegree = TAApplication.Models.DegreeLevel.BS,
                    Program = "BUS",
                    GPA = 1.0,
                    HoursWanted = 5,
                    EarlyAvailability = false,
                    SemestersCompletedAtUtah = 69,
                    TAUser = user1
                }
            };
            foreach (Application app in apps)
            {
                await this.Applications.AddAsync(app);
            }
            this.SaveChanges();
        }
        
        public async Task InitializeSlots(UserManager<TAUser> um)
        {
            if (this.Slots.Any<Slot>())
            {
                return;   // DB has been seeded
            }
            TAUser user0 = await um.FindByEmailAsync("u0000000@utah.edu");

            // 8am to noon on monday and friday
            // noon to 5pm on tuesday and thursday
            var slots = new Slot[240];
            for(int i = 0; i < 240; i++)
            {
                slots[i] = new Slot
                {
                    IsOpen = false,
                    SlotNumber = i,
                    TAUser = user0
                };
            }

            // Monday 8am -> 12pm
            for(int i = 0; i < 16; i++)
                slots[i].IsOpen = true;
            // Tuesday 12pm -> 5pm
            for(int i = 48+16; i < 48+16+20; i++)
                slots[i].IsOpen = true;
            // Thursday 12pm -> 5pm
            for(int i = 144+16; i < 144+16+20; i++)
                slots[i].IsOpen = true;
            // Friday 8am -> 12pm
            for(int i = 192; i < 192+16; i++)
                slots[i].IsOpen = true;


            foreach (Slot sl in slots)
            {
                await this.Slots.AddAsync(sl);
            }
            this.SaveChanges();
        }
        
        public async Task InitializeCourses(UserManager<TAUser> um)
        {
            if (this.Courses.Any<Course>())
            {
                return;   // DB has been seeded
            }
            TAUser prof = await um.FindByEmailAsync("professor@utah.edu");
            var additCourses = new Course[]
            {
                new Course
                {
                    Semester = TAApplication.Models.Semester.Spring,
                    Year = 2022,
                    Title = "Introduction to Object Oriented Programming",
                    Department = "CS",
                    Number = 1400,
                    Section = "001",
                    Description = "When you think CS is easy.",
                    ProfessorUNID = prof.Unid,
                    ProfessorName = prof.Name,
                    TimeAndDaysOffered = "M/W 3:30-5:00",
                    Location = "WEB L104",
                    CreditHours = 3,
                    Enrollment = 0,
                    Note = "Oh Yeah! I'm an ADMIN, WOOOOO!"
                },
                new Course
                {
                    Semester = TAApplication.Models.Semester.Spring,
                    Year = 2022,
                    Title = "Intro to Syntax",
                    Department = "LING",
                    Number = 4020,
                    Section = "001",
                    Description = "When you give up on CS.",
                    ProfessorUNID = prof.Unid,
                    ProfessorName = prof.Name,
                    TimeAndDaysOffered = "M/W 3:30-5:00",
                    Location = "GEO L104",
                    CreditHours = 4,
                    Enrollment = 0
                },
                new Course
                {
                    Semester = TAApplication.Models.Semester.Spring,
                    Year = 2022,
                    Title = "Research Forum",
                    Department = "CS",
                    Number = 3020,
                    Section = "001",
                    Description = "When you think CS is very easy.",
                    ProfessorUNID = prof.Unid,
                    ProfessorName = prof.Name,
                    TimeAndDaysOffered = "F 3:30-5:00",
                    Location = "ART 210",
                    CreditHours = 1,
                    Enrollment = 0
                },
                new Course
                {
                    Semester = TAApplication.Models.Semester.Spring,
                    Year = 2022,
                    Title = "Artifical Intelligence",
                    Department = "CS",
                    Number = 4300,
                    Section = "001",
                    Description = "When you think CS is not easy.",
                    ProfessorUNID = prof.Unid,
                    ProfessorName = prof.Name,
                    TimeAndDaysOffered = "M/W 3:30-5:00",
                    Location = "WEB L104",
                    CreditHours = 3,
                    Enrollment = 0
                },
                new Course
                {
                    Semester = TAApplication.Models.Semester.Spring,
                    Year = 2022,
                    Title = "Computer Systems",
                    Department = "CS",
                    Number = 4400,
                    Section = "001",
                    Description = "When you think CS is hard.",
                    ProfessorUNID = prof.Unid,
                    ProfessorName = prof.Name,
                    TimeAndDaysOffered = "T/Th 3:30-5:00",
                    Location = "WEB L101",
                    CreditHours = 4,
                    Enrollment = 0,
                    Note = "Course 4400"
                }
            };
            foreach (Course c in additCourses)
            {
                await this.Courses.AddAsync(c);
            }
            this.SaveChanges();
        }

        public async Task InitializeEnrollmentData()
        {
            if (this.EnrollmentRecords.Any<EnrollmentRecord>())
            {
                return;
            }

            // csv-reading code taken from https://stackoverflow.com/questions/3507498/reading-csv-files-using-c-sharp
            using (TextFieldParser parser = new TextFieldParser(@"wwwroot\csv\temp.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                List<String> dateStrs = new List<String>();
                int rowNum = 0;
                // iterate over all rows of the csv
                while (!parser.EndOfData)
                {
                    if (rowNum != 0)
                    {
                        // process enrollment data
                        string[] row = parser.ReadFields();
                        string[] courseDeptAndNo = row[0].Split(' ');
                        string dept = courseDeptAndNo[0];
                        int no = Int32.Parse(courseDeptAndNo[1]);
                        Course course = await Courses.Where(c => c.Department == dept && c.Number == no).FirstOrDefaultAsync();
                        if(course == null)
                        {// generate a default course if needed
                            course = new Course
                            {
                                Department = dept,
                                Number = no,

                                Semester = TAApplication.Models.Semester.Fall,
                                Year = 0,
                                Title = "No Title Provided",
                                Section = "0",
                                Description = "No Desc. Provided",
                                ProfessorUNID = 0,
                                ProfessorName = "No Prof. Provided",
                                TimeAndDaysOffered = "No Times Provided",
                                Location = "No Location Provided",
                                CreditHours = 0,
                                Enrollment = 300
                            };
                            await this.Courses.AddAsync(course);
                            await SaveChangesAsync();
                        }
                        for (int col = 1; col < row.Length; col++)
                        {
                            EnrollmentRecord er = new EnrollmentRecord
                            {
                                CourseID = course.ID,
                                Date = DateTime.Parse(dateStrs[col]),
                                Enrollment = Int32.Parse(row[col])
                            };
                            await this.EnrollmentRecords.AddAsync(er);
                        }
                    }
                    // grab dates / header row
                    else
                    {
                        dateStrs.AddRange(parser.ReadFields());
                    }
                    rowNum++;
                }
            }

            await SaveChangesAsync();
        }

        /// <summary>
        /// Every time Save Changes is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()  // JIM: Override save changes to add timestamps
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        /// <summary>
        /// Every time Save Changes (Async) is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddTimestamps();   // JIM: Override save changes async to add timestamps
            return await base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is ModificationTracking
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = "";

            if (_httpContextAccessor.HttpContext == null) // happens during startup/initialization code
            {
                currentUsername = "DBSeeder";
            }
            else
            {
                currentUsername = _httpContextAccessor.HttpContext.User.Identity?.Name;
            }

            currentUsername ??= "Sadness"; // JIM: compound assignment magic... test for null, and if so, assign value

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ModificationTracking)entity.Entity).CreationDate = DateTime.UtcNow;
                    ((ModificationTracking)entity.Entity).CreatedBy = currentUsername;
                }
                ((ModificationTracking)entity.Entity).ModificationDate = DateTime.UtcNow;
                ((ModificationTracking)entity.Entity).ModifiedBy = currentUsername;
            }
        }
    }
}