using Dapper;
using DvdLibrary.Interfaces;
using DvdLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DvdLibrary.Repositories
{
    // Implement the IDvdRepository
    public class DvdRepositoryDapper :IDvdRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

        // Method to load all dvd data from database
        public List<Dvd> LoadAllDvds()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                return conn.Query<Dvd>("usp_DvdSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        // Method to load single dvd data based on id from database
        public Dvd LoadSingleDvd(int Id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Id);

                return conn.Query<Dvd>("usp_DvdSelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        // Method to add dvd to database
        public void AddDvd(AddDvd addDvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Title", addDvd.Title);
                parameters.Add("@ReleaseYear", addDvd.ReleaseYear);
                parameters.Add("@Director", addDvd.Director);
                parameters.Add("@Rating", addDvd.Rating);
                parameters.Add("@Notes", addDvd.Notes);

                conn.Execute("usp_DvdAdd", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Method to update existing dvd based on id in database
        public void UpdateDvd(int Id, UpdateDvd updateDvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", Id);
                parameters.Add("@Title", updateDvd.Title);
                parameters.Add("@ReleaseYear", updateDvd.ReleaseYear);
                parameters.Add("@Director", updateDvd.Director);
                parameters.Add("@Rating", updateDvd.Rating);
                parameters.Add("@Notes", updateDvd.Notes);

                conn.Execute("usp_DvdUpdate", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Method to delete dvd in database
        public void DeleteDvd(int Id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Id);

                conn.Execute("usp_DvdDelete", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Method to search for dvds based on title in database
        public List<Dvd> SearchTitle(string Title)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", Title);

                return conn.Query<Dvd>("usp_DvdSelectByTitle", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        // Method to search for dvds based on year in database
        public List<Dvd> SearchReleaseYear(int ReleaseYear)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ReleaseYear", ReleaseYear);

                return conn.Query<Dvd>("usp_DvdSelectByYear", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        // Method to search for dvds based on director in database
        public List<Dvd> SearchDirector(string Director)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Director", Director);

                return conn.Query<Dvd>("usp_DvdSelectByDirector", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        // Method to search for dvds based on rating in database
        public List<Dvd> SearchRating(string Rating)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Rating", Rating);

                return conn.Query<Dvd>("usp_DvdSelectByRating", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}