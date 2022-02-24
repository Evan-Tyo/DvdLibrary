using DvdLibrary.Interfaces;
using DvdLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Npgsql;

namespace DvdLibrary.Repositories
{
    // Implement the IDvdRepository
    public class DvdRepositoryADO_RE : IDvdRepository
    {
        // Set up the connection string from Web.config
        private string _connectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

        // Method to load all dvd data from database
        public List<Dvd> LoadAllDvds()
        {
            List<Dvd> dvds = new List<Dvd>();
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdSelectAll()";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                conn.Open();

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd newDvdRow = new Dvd();

                        newDvdRow.Id = (int)dr["Id"];
                        newDvdRow.Title = dr["Title"].ToString();
                        newDvdRow.ReleaseYear = (int)dr["ReleaseYear"];
                        newDvdRow.Director = dr["Director"].ToString();
                        newDvdRow.Rating = dr["Rating"].ToString();
                        newDvdRow.Notes = dr["Notes"].ToString();
                        dvds.Add(newDvdRow);
                    }
                }

                return dvds;
            }
        }

        // Method to load single dvd data based on id from database
        public Dvd LoadSingleDvd(int Id)
        {
            Dvd dvd = new Dvd();
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdSelectById(@Id)";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@Id", Id);
                conn.Open();

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dvd.Id = (int)dr["Id"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseYear = (int)dr["ReleaseYear"];
                        dvd.Director = dr["Director"].ToString();
                        dvd.Rating = dr["Rating"].ToString();
                        dvd.Notes = dr["Notes"].ToString();
                    }
                }

                return dvd;
            }
        }

        // Method to add dvd to database
        public void AddDvd(AddDvd addDvd)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdAdd(@Title, @ReleaseYear, @Director, @Rating, @Notes)";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@Title", addDvd.Title);
                command.Parameters.AddWithValue("@ReleaseYear", addDvd.ReleaseYear);
                command.Parameters.AddWithValue("@Director", addDvd.Director);
                command.Parameters.AddWithValue("@Rating", addDvd.Rating);
                command.Parameters.AddWithValue("@Notes", addDvd.Notes);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        // Method to update existing dvd based on id in database
        public void UpdateDvd(int Id, UpdateDvd updateDvd)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdUpdate(@Id, @Title, @ReleaseYear, @Director, @Rating, @Notes);";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Title", updateDvd.Title);
                command.Parameters.AddWithValue("@ReleaseYear", updateDvd.ReleaseYear);
                command.Parameters.AddWithValue("@Director", updateDvd.Director);
                command.Parameters.AddWithValue("@Rating", updateDvd.Rating);
                command.Parameters.AddWithValue("@Notes", updateDvd.Notes);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        // Method to delete dvd in database
        public void DeleteDvd(int Id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdDelete(@Id);";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@Id", Id);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        // Method to search for dvds based on title in database
        public List<Dvd> SearchTitle(string Title)
        {
            List<Dvd> dvds = new List<Dvd>();
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdSelectByTitle(@Title);";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@Title", Title);
                conn.Open();

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd newDvdRow = new Dvd();

                        newDvdRow.Id = (int)dr["Id"];
                        newDvdRow.Title = dr["Title"].ToString();
                        newDvdRow.ReleaseYear = (int)dr["ReleaseYear"];
                        newDvdRow.Director = dr["Director"].ToString();
                        newDvdRow.Rating = dr["Rating"].ToString();
                        newDvdRow.Notes = dr["Notes"].ToString();
                        dvds.Add(newDvdRow);
                    }
                }

                return dvds;
            }
        }

        // Method to search for dvds based on year in database
        public List<Dvd> SearchReleaseYear(int ReleaseYear)
        {
            List<Dvd> dvds = new List<Dvd>();
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdSelectByYear(@ReleaseYear);";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@ReleaseYear", ReleaseYear);
                conn.Open();

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd newDvdRow = new Dvd();

                        newDvdRow.Id = (int)dr["Id"];
                        newDvdRow.Title = dr["Title"].ToString();
                        newDvdRow.ReleaseYear = (int)dr["ReleaseYear"];
                        newDvdRow.Director = dr["Director"].ToString();
                        newDvdRow.Rating = dr["Rating"].ToString();
                        newDvdRow.Notes = dr["Notes"].ToString();
                        dvds.Add(newDvdRow);
                    }
                }

                return dvds;
            }
        }

        // Method to search for dvds based on director in database
        public List<Dvd> SearchDirector(string Director)
        {
            List<Dvd> dvds = new List<Dvd>();
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdSelectByDirector(@Director);";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@Director", Director);
                conn.Open();

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd newDvdRow = new Dvd();

                        newDvdRow.Id = (int)dr["Id"];
                        newDvdRow.Title = dr["Title"].ToString();
                        newDvdRow.ReleaseYear = (int)dr["ReleaseYear"];
                        newDvdRow.Director = dr["Director"].ToString();
                        newDvdRow.Rating = dr["Rating"].ToString();
                        newDvdRow.Notes = dr["Notes"].ToString();
                        dvds.Add(newDvdRow);
                    }
                }

                return dvds;
            }
        }

        // Method to search for dvds based on rating in database
        public List<Dvd> SearchRating(string Rating)
        {
            List<Dvd> dvds = new List<Dvd>();
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                string comm = "SELECT * FROM usp_DvdSelectByRating(@Rating);";
                NpgsqlCommand command = new NpgsqlCommand(comm, conn);

                command.Parameters.AddWithValue("@Rating", Rating);
                conn.Open();

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd newDvdRow = new Dvd();

                        newDvdRow.Id = (int)dr["Id"];
                        newDvdRow.Title = dr["Title"].ToString();
                        newDvdRow.ReleaseYear = (int)dr["ReleaseYear"];
                        newDvdRow.Director = dr["Director"].ToString();
                        newDvdRow.Rating = dr["Rating"].ToString();
                        newDvdRow.Notes = dr["Notes"].ToString();
                        dvds.Add(newDvdRow);
                    }
                }

                return dvds;
            }
        }
    }
}
