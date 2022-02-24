using DvdLibrary.Interfaces;
using DvdLibrary.Repositories;
using DvdLibrary.Scripts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DvdLibrary.Factories
{
    public class RepositoryFactory
    {
        // Method to choose the appropriate repository for use
        public static IDvdRepository GetRepository()
        {
            // Set the mode based on the configuration string from Web.config
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            RunScript.EveryHourScript();

            // Chose the repository basedon the mode
            switch (mode)
            {
                case "SampleData":
                    return new DvdRepositoryMock();
                case "ADO":
                    return new DvdRepositoryADO();
                case "Dapper":
                    return new DvdRepositoryDapper();
                case "ADO_RE":
                    return new DvdRepositoryADO_RE();
                default:
                    throw new Exception("Mode value in app settings is not valid.");
            }
        }
    }
}
