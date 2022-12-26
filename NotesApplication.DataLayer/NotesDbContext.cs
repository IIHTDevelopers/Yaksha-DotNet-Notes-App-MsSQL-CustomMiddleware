using Microsoft.EntityFrameworkCore;
using NotesApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApplication.DataLayer
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
