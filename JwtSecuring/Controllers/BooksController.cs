using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using JwtSecuring.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtSecuring.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        [HttpGet, Authorize]
        public IEnumerable<Book> Get()
        {
            var currentUser = User;                        
            var resultBookList = new[] {
                new Book { Author = "Ray Bradbury",Title = "Fahrenheit 451" },
                new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude" },
                new Book { Author = "George Orwell", Title = "1984" },
                new Book { Author = "Anais Nin", Title = "Delta of Venus" }
            };

            var birthDate = currentUser.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value;
            if (birthDate != null && 
                DateTime.Today.Year - DateTime.Parse(birthDate).Year < 18)
            {
                resultBookList = resultBookList.Where(b => !b.AgeRestriction).ToArray();
            }

            return resultBookList;
        }
    }
}
