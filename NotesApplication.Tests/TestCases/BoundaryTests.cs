using Moq;
using NotesApplication.BusinessLayer.Interfaces;
using NotesApplication.BusinessLayer.Services;
using NotesApplication.BusinessLayer.Services.Repository;
using NotesApplication.BusinessLayer.ViewModels;
using NotesApplication.DataLayer;
using NotesApplication.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NotesApplication.Tests.TestCases
{
    public class BoundaryTests
    {
        private readonly ITestOutputHelper _output;
        private readonly INoteService _noteServices;
        public readonly Mock<INoteRepository> noteservice = new Mock<INoteRepository>();
        private readonly Note _note;
        private static string type = "Boundary";
        public BoundaryTests(ITestOutputHelper output)
        {
            _noteServices = new NoteService(noteservice.Object);
            _output = output;

            _note = new Note
            {
                NoteId = 1,
                Description = "MyNote",
                Author = "MyAuthor",
                Status = "Completed",
                Title = "MyTitle",
            };
        }

        /// <summary>
        /// Test to validate note status connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Note_Status_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                noteservice.Setup(repo => repo.AddNote(_note)).ReturnsAsync(_note);
                var result = await _noteServices.AddNote(_note);
                if (result.Status.ToString().Length != 0)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
    }
}