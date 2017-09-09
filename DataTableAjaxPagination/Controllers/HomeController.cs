using DataTableAjaxPagination.Models;
using DataTableAjaxPagination.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace DataTableAjaxPagination.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Realiza una consulta paginada.
        /// </summary>
        /// <param name="model">Representa los datos enviados por el plug-in DataTable</param>
        /// <returns>Retorna con DataTableAdapter con la información que se va a mostrar en la tabla</returns>
        public ActionResult LoadDataTable(DataTableRequest model) {

            //Creamos un objeto data DataTableAdapter con el model view que vamos a mostrar.
            DataTableAdapter<Item> result = new DataTableAdapter<Item>();
            //Obtenemos el total de registros de la tabla.
            var totalRows = db.Items.Count();

            
            Func<Item, Object> orderByFunc = null;            
            //El ordenamiento que vamos a utilizar por default va ser por el Id.
            orderByFunc = item =>item.Id;            

            //Dependiendo de la columna que seleccionemos indicamos si se ordena por el campo Description.
            if (model.order[0]["column"].Equals("1") ) {
                orderByFunc = item => item.Description;
            }

            //Obtenemos el valor a buscar.
            var searchValue = "" + model.search["value"] + "";

            var queryItem = db.Items.Where(d => d.Description.Contains(searchValue));
            List<Item> items;
            //Indicamos cual va ser la manera en que se van a ordenar los datos.
            if (model.order[0]["dir"].Equals("desc"))
            {
                items = queryItem.OrderByDescending(orderByFunc).Skip(model.start + 1).Take(model.length).ToList();
            }
            else {
                items = queryItem.OrderBy(orderByFunc).Skip(model.start + 1).Take(model.length).ToList();
                
            }            
            
            //Llenamos con información nuestro DataTableAdapter
            result.Data = items;
            result.Draw = model.draw;
            result.RecordsTotal = totalRows;
            result.RecordsFiltered = queryItem.Count();
            //Regresamos la respuesta Json
            return Content(JsonConvert.SerializeObject(result), "application/json");

            
            
        }
    }
}