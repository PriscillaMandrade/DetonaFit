using DetonaFit.Contexts;
using DetonaFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetonaFit.Controllers
{
    public class GerenciarInstrutorController : Controller
    {
        [HttpGet]
        public ActionResult GerenciarInstrutor()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarInstrutor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarInstrutor(Instrutor model)
        {
            try
            {
                using (AcademiaContext conexao = new AcademiaContext())
                {
                    string acao = "";

                    if (model.ID > 0)
                    {
                        conexao.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        acao = "Instrutor alterado com sucesso!";
                    }
                    else
                    {
                        conexao.Entry(model).State = System.Data.Entity.EntityState.Added;
                        acao = "Instrutor cadastrado com sucesso!";
                    }

                    conexao.SaveChanges();

                    TempData["msgInstrutor"] = acao;
                    return RedirectToAction("GerenciarInstrutor");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult AlterarInstrutor(int id)
        {
            try
            {
                using (AcademiaContext conexao = new AcademiaContext())
                {
                    var model = conexao.Instrutor.Find(id);
                    return View("CadastrarInstrutor", model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetInstrutores(JQueryDataTableParamModel _modelFilter)
        {
            using (AcademiaContext conexao = new AcademiaContext())
            {
                List<Instrutor> qry = conexao.Instrutor.OrderBy(x=> x.ID).ToList();

                #region paramentros
                _modelFilter.sSearch = Convert.ToString(Request["search[value]"]);
                _modelFilter.iDisplayStart = Convert.ToInt32(Request["start"]);
                _modelFilter.iDisplayLength = Convert.ToInt32(Request["length"]);
                #endregion

                #region filtros
                if (_modelFilter.sSearch != null && _modelFilter.sSearch != "")
                {
                    String Search = _modelFilter.sSearch.Trim();

                    qry = qry.Where(x => x.Nome.Contains(Search)).ToList();
                }
                #endregion

                #region ordenação
                var coluna = Convert.ToInt32(Request["order[0][column]"]);
                var direcao = Convert.ToString(Request["order[0][dir]"]);

                if (coluna == 1)
                {
                    if (direcao == "asc")
                    {
                        qry = qry.OrderBy(x => x.Nome).ToList();
                    }
                    else
                    {
                        qry = qry.OrderByDescending(x => x.Nome).ToList();
                    }
                }
                #endregion

                var Source = qry.Select(x => new InstrutorModel
                {

                    ID = x.ID,
                    Nome = x.Nome,
                    Identidade = x.Identidade,
                    CPF = x.CPF,
                    Atividade = x.Atividade == null ? "Não Cadastrado" : x.Atividade == "M" ? "Musculação" : "Atividade em Grupo"

                });

                int totalRegistros = 0;
                List<InstrutorModel> result = new PaginatedData<InstrutorModel>(Source, _modelFilter.iDisplayStart, _modelFilter.iDisplayLength, ref totalRegistros);

                var Resultado = new
                {
                    sEcho = _modelFilter.sEcho,
                    iTotalRecords = totalRegistros,
                    iTotalDisplayRecords = totalRegistros,
                    aaData = Source
                };
                return Json(Resultado, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult ExcluirInstrutor(int id)
        {
            using (AcademiaContext conexao = new AcademiaContext())
            {
                try
                {
                    var InstrutorModel = conexao.Instrutor.Find(id);

                    conexao.Instrutor.Remove(InstrutorModel);
                    conexao.SaveChanges();


                    TempData["msgInstrutor"] = "Instrutor excluido com sucesso";
                    return RedirectToAction("GerenciarInstrutor");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }

    public class InstrutorModel
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Identidade { get; set; }

        public string CPF { get; set; }

        public string Atividade { get; set; }
    }
}