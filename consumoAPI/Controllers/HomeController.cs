using ConsumoAPI.Models;
using ConsumoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConsumoAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly Post _postItem;
        private readonly Put _putItem;
        private readonly Get _getItem;
        private readonly Delete _deleteItem;

        private readonly ILogger<HomeController> _logger;

        public HomeController(Delete deleteItem,Get getItem, Post postItem, Put putItem, ILogger<HomeController> logger)
        {
            _logger = logger;
            _postItem = postItem;
            _putItem = putItem;
            _getItem = getItem;
            _deleteItem = deleteItem;
        }


        public async Task<IActionResult> Index()
        {
            var listaAll = await _getItem.getAll();
            return View(listaAll);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update() 
        {
            return View();
        }


        public async Task<IActionResult> formReturn(Tarefa tarefa, string action)
        {

            if (ModelState.IsValid)
            {
                switch (action)
                {
                    case "createItem":
                        await _postItem.CreateItem(tarefa);
                        return RedirectToAction("Index");

                    case "updateItem":
                        await _putItem.PutItem(tarefa);
                        return RedirectToAction("Index");

                    case "deleteItem":
                        await _deleteItem.DeleteItem(tarefa);
                        return RedirectToAction("Index");
                }
            }

            return View("Create");
        }

        public async Task<IActionResult> getItemSearch(string nameOrID)
        {
            Tarefa model = new Tarefa();
            if(nameOrID == null)
            {
                ViewBag.Message = "Para pesquisar insira um nome ou ID";

                return View("Index");
            }
            var existName = await _getItem.getName(nameOrID);
            var existId = await _getItem.getId(nameOrID);

            if (existName.name == null && existId._id == null)
            {
                ViewBag.Message = "Tarefa não encontrada, crie uma nova tarefa";

                return View("Create");
            }
            else
            {
                if (existName.name == null) {
                    model = existId;
                }
                else
                {
                    model = existName;
                }
                
                return View("Create", model);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
