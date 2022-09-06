using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Vacations.Core.Models.Codex;

namespace Vacations.Persistence
{
    public static class Seed
    {
        public static void SeedData(ApplicationDbContext context)
        {
            #region EmploymentTypes

            if (!context.EmployementTypes.Any())
            {
                List<EmploymentType> employmentTypesData = ReadInitialEmploymentTypesData();
                context.EmployementTypes.AddRange(employmentTypesData);
                context.SaveChanges();
            }

            #endregion EmploymentTypes

            #region VacationRequestStatuses

            if (!context.VacationRequestStatuses.Any())
            {
                List<VacationRequestStatus> vacationRequestStatuses = ReadInitiaVacationRequestStatusesData();
                context.VacationRequestStatuses.AddRange(vacationRequestStatuses);
                context.SaveChanges();
            }

            #endregion VacationRequestStatuses
        }

        #region Genererate or read initial data

        private static List<EmploymentType> ReadInitialEmploymentTypesData()
        {
            List<EmploymentType> newItemsList = new List<EmploymentType>();

            try
            {
                var baseDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
                var fileFolder = "/Persistence/InitialData/";
                var fileName = "employmentTypes.json";

                var filePath = Path.GetFullPath(baseDirectory + fileFolder + fileName);

                using StreamReader c = new StreamReader(filePath);
                string jsonString = c.ReadToEnd();
                newItemsList = JsonConvert.DeserializeObject<List<EmploymentType>>(jsonString);
                c.Dispose();
            }
            catch (Exception)
            {
            }

            return newItemsList;
        }

        private static List<VacationRequestStatus> ReadInitiaVacationRequestStatusesData()
        {
            List<VacationRequestStatus> newItemsList = new List<VacationRequestStatus>();

            try
            {
                var baseDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
                var fileFolder = "/Persistence/InitialData/";
                var fileName = "vacationRequestStatuses.json";

                var filePath = Path.GetFullPath(baseDirectory + fileFolder + fileName);

                using StreamReader c = new StreamReader(filePath);
                string jsonString = c.ReadToEnd();
                newItemsList = JsonConvert.DeserializeObject<List<VacationRequestStatus>>(jsonString);
                c.Dispose();
            }
            catch (Exception)
            {
            }

            return newItemsList;
        }

        #endregion Genererate or read initial data
    }
}