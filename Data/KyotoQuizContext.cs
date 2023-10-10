using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KyotoQuiz.Models;

namespace KyotoQuiz.Data
{
    public class KyotoQuizContext : DbContext
    {
        public KyotoQuizContext (DbContextOptions<KyotoQuizContext> options)
            : base(options)
        {
        }

        public DbSet<KyotoQuiz.Models.Genre> Genre { get; set; } = default!;
    }
}
