using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peluches.Ecommerce.Controllers
{
    public class EcommerceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CatalogoProductos()
        {
            return View();
        }

        public IActionResult DetalleProducto()
        {
            return View();
        }

        public IActionResult CarroCompras()
        {
            return View();
        }

        public IActionResult PagarCompra()
        {
            return View();
        }

        public IActionResult ConfirmacionPago()
        {
            return View();
        }
    }
}
