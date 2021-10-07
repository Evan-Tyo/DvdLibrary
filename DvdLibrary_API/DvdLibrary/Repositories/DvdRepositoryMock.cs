using DvdLibrary.Interfaces;
using DvdLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibrary.Repositories
{
    // Implement the IDvdRepository
    public class DvdRepositoryMock : IDvdRepository
    {
        // Method to load all dvd data from database
        public List<Dvd> LoadAllDvds()
        {
            return _dvds;
        }

        // Method to load single dvd data based on id from database
        public Dvd LoadSingleDvd(int Id)
        {
            return _dvds.FirstOrDefault(d => d.Id == Id);
        }

        // Method to add dvd to database
        public void AddDvd(AddDvd addDvd)
        {
            Dvd dvd = new Dvd();

            if (_dvds.Any())
            {
                dvd.Id = _dvds.Max(d => d.Id) + 1;
            }
            else
            {
                dvd.Id = 0;
            }

            dvd.Title = addDvd.Title;
            dvd.ReleaseYear = addDvd.ReleaseYear;
            dvd.Director = addDvd.Director;
            dvd.Rating = addDvd.Rating;
            dvd.Notes = addDvd.Notes;

            _dvds.Add(dvd);
        }

        // Method to update existing dvd based on id in database
        public void UpdateDvd(int Id, UpdateDvd updateDvd)
        {
            Dvd dvd = new Dvd();

            dvd.Title = updateDvd.Title;
            dvd.ReleaseYear = updateDvd.ReleaseYear;
            dvd.Director = updateDvd.Director;
            dvd.Rating = updateDvd.Rating;
            dvd.Notes = updateDvd.Notes;

            _dvds.RemoveAll(d => d.Id == Id);
            dvd.Id = Id;
            _dvds.Add(dvd);
        }

        // Method to delete dvd in database
        public void DeleteDvd(int Id)
        {
            _dvds.RemoveAll(d => d.Id == Id);
        }

        // Method to search for dvds based on title in database
        public List<Dvd> SearchTitle(string Title)
        {
            var dvdTitleSearch = _dvds.Where(d => d.Title == Title).ToList();
            return dvdTitleSearch;
        }

        // Method to search for dvds based on year in database
        public List<Dvd> SearchReleaseYear(int ReleaseYear)
        {
            var dvdReleaseYearSearch = _dvds.Where(d => d.ReleaseYear == ReleaseYear).ToList();
            return dvdReleaseYearSearch;
        }

        // Method to search for dvds based on director in database
        public List<Dvd> SearchDirector(string Director)
        {
            var dvdDirectorSearch = _dvds.Where(d => d.Director == Director).ToList();
            return dvdDirectorSearch;
        }

        // Method to search for dvds based on rating in database
        public List<Dvd> SearchRating(string Rating)
        {
            var dvdRatingSearch = _dvds.Where(d => d.Rating == Rating).ToList();
            return dvdRatingSearch;
        }

        // Mock data to test mock API
        private static List<Dvd> _dvds = new List<Dvd>()
        {
            new Dvd { Id = 1, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" },
            new Dvd { Id = 2, Title = "A Good Tale", ReleaseYear = 2012, Director = "Joe Smith", Rating = "PG-13", Notes = "This is a good tale!" },
            new Dvd { Id = 3, Title = "A Super Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "G", Notes = "A great remake!" },
            new Dvd { Id = 4, Title = "A Super Tale", ReleaseYear = 1985, Director = "Joe Smith", Rating = "PG", Notes = "The original!" },
            new Dvd { Id = 5, Title = "A Wonderful Tale", ReleaseYear = 2011, Director = "Sam Smith", Rating = "R", Notes = "Wonderful, just wonderful!" },
            new Dvd { Id = 6, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones", Rating = "PG", Notes = "This really is a great tale!" },
        };
    }
}