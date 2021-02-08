namespace StudentApplication
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class StudentModel : DbContext
    {
        // Контекст настроен для использования строки подключения "StudentModel" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "StudentApplication.StudentModel" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "StudentModel" 
        // в файле конфигурации приложения.
        public StudentModel()
            : base("name=StudentModel")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Curator> Curators { get; set; }
    }

    public class Group
    {
        public int GroupID { get; set; }
        public string Title { get; set; }
        public int? CuratorID { get; set; }
        public Curator Curator { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Curator
    {
        public int CuratorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string Photo { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Year_of_admission { get; set; }
        public decimal Scholarship { get; set; }
        public int? GroupID { get; set; }
        public Group Group { get; set; }

    }
}