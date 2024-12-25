using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using GR30323.API.Controllers;
using GR30323.API.Data;
using GR30323.Domain.Entities;
using GR30323.Domain.Models;

namespace GR30323.Tests
{
    public class BooksControllerTests
    {
        private readonly BooksController _controller;
        private readonly AppDbContext _context;

        public BooksControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _controller = new BooksController(_context, null); 
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {

            _context.Database.EnsureDeleted(); 
            _context.Database.EnsureCreated(); 
           
            var category = new Category
            {
                Id = 1,
                Name = "Fiction",
                NormalizedName = "FICTION"
            };

            var book1 = new Book
            {
                Id = 1,
                Name = "Book 1",
                Description = "Description for Book 1", 
                Image = "image1.jpg",
                Avtor = "Author 1", 
                PublicationDate = 2021,
                Price = 19.99,
                Category = category
            };

            var book2 = new Book
            {
                Id = 2,
                Name = "Book 2",
                Description = "Description for Book 2",
                Image = "image2.jpg",
                Avtor = "Author 2", 
                PublicationDate = 2022,
                Price = 29.99,
                Category = category
            };

            _context.Categories.Add(category);
            _context.Books.AddRange(book1, book2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetBooks(null, 1, 10);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseData<ListModel<Book>>>>(result);
            var responseData = Assert.IsType<ResponseData<ListModel<Book>>>(actionResult.Value);
            Assert.NotNull(responseData.Data);
            Assert.Equal(2, responseData.Data.Items.Count);
        }

        [Fact]
        public async Task GetBook_ReturnsBook_WhenBookExists()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Fiction",
                NormalizedName = "FICTION" 
            };

            var book = new Book
            {
                Id = 1,
                Name = "Book 1",
                Description = "Description for Book 1", 
                Avtor = "Author 1", 
                PublicationDate = 2021,
                Price = 19.99,
                Category = category
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetBook(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseData<Book>>>(result);
            var responseData = Assert.IsType<ResponseData<Book>>(actionResult.Value);
            Assert.NotNull(responseData.Data);
            Assert.Equal("Book 1", responseData.Data.Name);
        }

        [Fact]
        public async Task GetBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            
            var result = await _controller.GetBook(999); 

            
            var actionResult = Assert.IsType<ActionResult<ResponseData<Book>>>(result);
            var responseData = Assert.IsType<ResponseData<Book>>(actionResult.Value);
            Assert.False(responseData.Success);
            Assert.Equal("Данные не найдены", responseData.ErrorMessage);
        }

        [Fact]
        public async Task PostBook_CreatesNewBook()
        {
            
            var category = new Category
            {
                Id = 1,
                Name = "Fiction",
                NormalizedName = "FICTION" 
            };

            var book = new Book
            {
                Name = "New Book",
                Description = "Description for New Book", 
                Avtor = "New Author", 
                PublicationDate = 2023,
                Price = 15.99,
                Category = category
            };

            // Act
            var result = await _controller.PostBook(book);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.NotNull(createdAtActionResult.Value);
            var createdBook = Assert.IsType<Book>(createdAtActionResult.Value);
            Assert.Equal("New Book", createdBook.Name);
        }

        [Fact]
        public async Task DeleteBook_RemovesBook_WhenBookExists()
        {
            
            var category = new Category
            {
                Id = 1,
                Name = "Fiction",
                NormalizedName = "FICTION" 
            };

            var book = new Book
            {
                Id = 1,
                Name = "Book to Delete",
                Description = "Description for Book to Delete", 
                Avtor = "Author to Delete", 
                PublicationDate = 2021,
                Price = 19.99,
                Category = category
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            
            var result = await _controller.DeleteBook(1);

            
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await _context.Books.FindAsync(1)); 
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            
            var result = await _controller.DeleteBook(999); 

           
            Assert.IsType<NotFoundResult>(result);
        }
    }
}