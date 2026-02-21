using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace project
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public bool IsAvailable { get; set; }
    }
}