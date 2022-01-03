using Microsoft.EntityFrameworkCore;
using MicroVistaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.DatabaseContext
{
    public class DBContextClass : DbContext
    {


        public DBContextClass(DbContextOptions<DBContextClass> options) : base(options)
        {    }

        public DbSet<StudentClass> studentTable { get; set; }
        public DbSet<StudentPostClass> studentPostTable { get; set; }



    }
}
