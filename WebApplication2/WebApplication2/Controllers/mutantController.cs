using Models.BussinessLogic;
using Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class mutantController : ApiController
    {


        public const string HUMANO = "Humano";
        public const string MUTANTE = "Mutante";
        private DnaAnalizerEntities db;
        public mutantController()
        {
            db = new DnaAnalizerEntities();
        }
        /// <summary>
        /// Esta Api devolvera un resultado si El array contiene si encuentras más de una secuencia de cuatro letras
        /// iguales​, de forma oblicua, horizontal o vertical.
        /// </summary>
        /// <param name="Array"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage mutant(ParametersRecibed parameters)
        {
            try
            {
                MutanteLogic Modelo = new MutanteLogic();
                var result = new HttpResponseBaseModel();
                if (parameters == null)
                {
                    result.Code = 400;
                    result.Message = "ERROR, No se recibe datos en Array base nitrogenada del ADN. ";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result, Configuration.Formatters.JsonFormatter);
                }
                if (!Modelo.ValidarMatrizNXN(parameters.Dna))
                {
                    result.Code = 400;
                    result.Message = "ERROR, Array No es NxN";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result, Configuration.Formatters.JsonFormatter);
                }
                if (!Modelo.VerificacionDeLetras(parameters.Dna))
                {
                    result.Code = 400;
                    result.Message = "ERROR, Cadena de ADN desconocida , se Buscan Xmen,NO ALIENS.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result, Configuration.Formatters.JsonFormatter);
                }
                if (Modelo.isMutant(parameters.Dna))
                {
                    result.Code = 200;
                    result.Message = MUTANTE;
                    db.CadenasAnalizadas.Add(new CadenasAnalizadas { AdnAnalizado = JsonConvert.SerializeObject(parameters.Dna), FechaConsulta = DateTime.Now, Resultado = result.Message });
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    result.Code = 403;
                    result.Message = HUMANO;
                    db.CadenasAnalizadas.Add(new CadenasAnalizadas { AdnAnalizado = JsonConvert.SerializeObject(parameters.Dna), FechaConsulta = DateTime.Now, Resultado = result.Message });
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Forbidden, result, Configuration.Formatters.JsonFormatter);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, Configuration.Formatters.JsonFormatter);
            }

        }

    }
    public class statsController : ApiController
    {
        DnaAnalizerEntities db;
        public statsController()
        {
            db = new DnaAnalizerEntities();
        }
        [HttpGet]
        public HttpResponseMessage stats()
        {
            try
            {

                statsModel Stadisticas = new statsModel();
                var ListaConsultas = db.CadenasAnalizadas.ToList();
                foreach (var item in ListaConsultas)
                {
                    if (item.Resultado.Equals(mutantController.HUMANO))
                        Stadisticas.count_human_dna++;
                    if (item.Resultado.Equals(mutantController.MUTANTE))
                        Stadisticas.count_mutant_dna++;
                }
                Stadisticas.CalcularRatio();
                return Request.CreateResponse(HttpStatusCode.OK, Stadisticas, Configuration.Formatters.JsonFormatter);

            }
            catch (Exception E)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, E, Configuration.Formatters.JsonFormatter);
            }



        }
    }
}