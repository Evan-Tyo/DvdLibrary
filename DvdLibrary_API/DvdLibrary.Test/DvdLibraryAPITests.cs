using DvdLibrary.Factories;
using DvdLibrary.Interfaces;
using DvdLibrary.Models;
using DvdLibrary.Repositories;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Test
{
    [TestFixture]
    class DvdLibraryAPITests
    {

        [Test]
        public void TestLoadAllDvds()
        {
            // Set up repository and set data to list
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.LoadAllDvds();

            // Assert to show tested dvd count is correct
            Assert.AreEqual(6, testedDvd.Count);

            // Assert to show first dvd title is correct
            Assert.AreEqual("A Great Tale", testedDvd[0].Title);
        }

        [Test]
        public void TestLoadSingleDvd()
        {
            // Set up repository and set dvd at id = 1 to variable
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.LoadSingleDvd(1);

            // Create expected dvd data for test
            var expectedDvd = new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" };

            // Assert to show tested dvd and expected dvd data are equal
            Assert.AreEqual(testedDvd.Title, expectedDvd.Title);
            Assert.AreEqual(testedDvd.ReleaseYear, expectedDvd.ReleaseYear);
            Assert.AreEqual(testedDvd.Director, expectedDvd.Director);
            Assert.AreEqual(testedDvd.Rating, expectedDvd.Rating);
            Assert.AreEqual(testedDvd.Notes, expectedDvd.Notes);
        }

        [Test]
        public void TestAddDvd()
        {
            // Set up repository, set data to list, and get list count
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.LoadAllDvds();
            var count = testedDvd.Count;

            // Create added dvd data for test
            var addedDvd = new AddDvd { Title = "A Seventh Story", ReleaseYear = 2020, Director = "The Seventh Director", Rating = "G", Notes = "Notes about A Seventh Story" };

            // Add dvd data to repository and set data to list
            repository.AddDvd(addedDvd);
            var addedTestedDvd = repository.LoadAllDvds();

            // Assert to show tested dvd list and added dvd list are not equal count
            Assert.AreNotEqual(count, addedTestedDvd.Count);

            // Create expected dvd data for test
            var expectedDvd = new Dvd { Id = 7, Title = "A Seventh Story", ReleaseYear = 2020, Director = "The Seventh Director", Rating = "G", Notes = "Notes about A Seventh Story" };

            // Assert to show added dvd id and expected dvd id are equal
            Assert.AreEqual(addedTestedDvd[6].Id, expectedDvd.Id);
        }

        [Test]
        public void TestUpdateDvd()
        {
            // Set up repository and set data to list
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.LoadAllDvds();

            // Create expected dvd data for test
            var expectedDvd = new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" };

            // Assert to show first dvd director in tested dvd is equal to director in expected dvd
            Assert.AreEqual(testedDvd[0].Director, expectedDvd.Director);

            // Create updated dvd data for test
            var updatedDvd = new UpdateDvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones Updated", Rating = "PG", Notes = "This really is a great tale!" };

            // Update dvd data to repository and set data to list
            repository.UpdateDvd(1, updatedDvd);
            testedDvd = repository.LoadAllDvds();

            // Assert to show first dvd director in tested dvd is not equal to director in expected dvd
            Assert.AreNotEqual(testedDvd[0].Director, expectedDvd.Director);
        }

        [Test]
        public void TestDeleteDvd()
        {
            // Set up repository and set data to list
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.LoadAllDvds();

            // Create expected dvd data for test
            var expectedDvd = new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" };

            // Assert to show tested dvd id for first entry and expected dvd id are equal
            Assert.AreEqual(testedDvd[0].Id, expectedDvd.Id);

            // Delete dvd data from repository and set data to list
            repository.DeleteDvd(1);
            testedDvd = repository.LoadAllDvds();

            // Assert to show tested dvd id for first entry and expected dvd id are not equal
            Assert.AreNotEqual(testedDvd[0].Id, expectedDvd.Id);
        }

        [Test]
        public void TestSearchTitle()
        {
            // Set up repository and set data to list based on entered title
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.SearchTitle("A Great Tale");

            // Create exppected dvd list data for test
            var expectedDvd = new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" };

            // Assert to show tested dvd and expected dvd data are equal
            Assert.AreEqual(testedDvd[0].Title, expectedDvd.Title);
            Assert.AreEqual(testedDvd[0].ReleaseYear, expectedDvd.ReleaseYear);
            Assert.AreEqual(testedDvd[0].Director, expectedDvd.Director);
            Assert.AreEqual(testedDvd[0].Rating, expectedDvd.Rating);
            Assert.AreEqual(testedDvd[0].Notes, expectedDvd.Notes);

            // Assert to show the correct count of dvds returned
            Assert.AreEqual(2, testedDvd.Count);
        }

        [Test]
        public void TestSearchReleaseYear()
        {
            // Set up repository and set data to list based on entered title
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.SearchReleaseYear(2015);

            // Create exppected dvd list data for test
            var expectedDvd = new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" };

            // Assert to show tested dvd and expected dvd data are equal
            Assert.AreEqual(testedDvd[0].Title, expectedDvd.Title);
            Assert.AreEqual(testedDvd[0].ReleaseYear, expectedDvd.ReleaseYear);
            Assert.AreEqual(testedDvd[0].Director, expectedDvd.Director);
            Assert.AreEqual(testedDvd[0].Rating, expectedDvd.Rating);
            Assert.AreEqual(testedDvd[0].Notes, expectedDvd.Notes);

            // Assert to show the correct count of dvds returned
            Assert.AreEqual(3, testedDvd.Count);
        }

        [Test]
        public void TestSearchDirector()
        {
            // Set up repository and set data to list based on entered title
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.SearchDirector("Sam Jones");

            // Create exppected dvd list data for test
            var expectedDvd = new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" };

            // Assert to show tested dvd and expected dvd data are equal
            Assert.AreEqual(testedDvd[0].Title, expectedDvd.Title);
            Assert.AreEqual(testedDvd[0].ReleaseYear, expectedDvd.ReleaseYear);
            Assert.AreEqual(testedDvd[0].Director, expectedDvd.Director);
            Assert.AreEqual(testedDvd[0].Rating, expectedDvd.Rating);
            Assert.AreEqual(testedDvd[0].Notes, expectedDvd.Notes);

            // Assert to show the correct count of dvds returned
            Assert.AreEqual(3, testedDvd.Count);
        }

        [Test]
        public void TestSearchRating()
        {
            // Set up repository and set data to list based on entered title
            var repository = RepositoryFactory.GetRepository();
            var testedDvd = repository.SearchRating("PG");

            // Create exppected dvd list data for test
            var expectedDvd = new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" };

            // Assert to show tested dvd and expected dvd data are equal
            Assert.AreEqual(testedDvd[0].Title, expectedDvd.Title);
            Assert.AreEqual(testedDvd[0].ReleaseYear, expectedDvd.ReleaseYear);
            Assert.AreEqual(testedDvd[0].Director, expectedDvd.Director);
            Assert.AreEqual(testedDvd[0].Rating, expectedDvd.Rating);
            Assert.AreEqual(testedDvd[0].Notes, expectedDvd.Notes);

            // Assert to show the correct count of dvds returned
            Assert.AreEqual(3, testedDvd.Count);
        }
    }
}
