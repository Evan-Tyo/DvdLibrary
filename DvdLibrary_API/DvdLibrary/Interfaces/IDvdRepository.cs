using DvdLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibrary.Interfaces
{
    // Interface for all repositorys
    public interface IDvdRepository
    {
        List<Dvd> LoadAllDvds();

        Dvd LoadSingleDvd(int Id);

        void AddDvd(AddDvd addDvd);

        void UpdateDvd(int Id, UpdateDvd updateDvd);

        void DeleteDvd(int Id);

        List<Dvd> SearchTitle(string Title);

        List<Dvd> SearchReleaseYear(int ReleaseYear);

        List<Dvd> SearchDirector(string Director);

        List<Dvd> SearchRating(string Rating);

    }
}
