using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AzureKeyVaultDemo.Entities;
using AzureKeyVaultDemo.Models;

namespace AzureKeyVaultDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<BaseRecordModel> BaseRecordModels = ConfigurationManager.AppSettings["GetValuesFromDatabase"] == "true" ? Initialize() : new List<BaseRecordModel>();

            // Gross, what is happening here? If your head is spinning about this not being proper MVC architecture, you'd be justified. Where is my service layer?
            //   What about pattern repository? These comments clutter everything? This is garbage. Relax, this is a demonstration of ConfigBuilder.
            try
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["demoEDMXEntities"].ConnectionString;

                // I want to strip out identifiers in the connectionString before I display it to the page.
                ViewBag.ConnectionString = SecureConnectionString(connectionString);
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(BaseRecordModels);
        }

        /// <summary>
        /// Private methods in a controller?! This method will take a connectionString and search for any values we don't want to display, and swap them out with "******"
        /// It will replace data source, initial catalog, user id, and password.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private string SecureConnectionString(string connectionString)
        {
            var dataSourceRegex = new Regex("(?<=data source=).+?(?=(;|'|\"|$))");
            var initialCatalogRegex = new Regex("(?<=initial catalog=).+?(?=(;|'|\"|$))");
            var userIdRegex = new Regex("(?<=user id=).+?(?=(;|'|\"|$))");
            var passwordRegex = new Regex("(?<=password=).+?(?=(;|'|\"|$))");

            if (dataSourceRegex.Match(connectionString).Value.Length > 0)
                connectionString = connectionString.Replace(dataSourceRegex.Match(connectionString).Value, "******");
            
            if(initialCatalogRegex.Match(connectionString).Value.Length > 0)
                connectionString = connectionString.Replace(initialCatalogRegex.Match(connectionString).Value, "******");
            
            if(passwordRegex.Match(connectionString).Value.Length > 0)
                connectionString = connectionString.Replace(passwordRegex.Match(connectionString).Value, "******");
            
            if(userIdRegex.Match(connectionString).Value.Length > 0)
                connectionString =connectionString.Replace(userIdRegex.Match(connectionString).Value, "******");

            return connectionString;
        }

        private List<BaseRecordModel> Initialize()
        {
            List<BaseRecordModel> list = new List<BaseRecordModel>();

            try
            {
                // Just a basic db context to show we can actually use the connectionString stored in Key Vault to make a connection to the database.
                using (var db = new demoEDMXEntities())
                {
                    var query = from br in db.BaseRecords
                                select br;

                    foreach (var item in query.ToList())
                    {
                        var record = new BaseRecordModel
                        {
                            Id = item.id,
                            Name = item.name
                        };

                        list.Add(record);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }
    }
}