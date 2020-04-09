using DetonaFit.Contexts;
using DetonaFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetonaFit.Controllers
{
    public class GerenciarAlunoController : Controller
    {
        [HttpGet]
        public ActionResult GerenciarAluno()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarAluno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarAluno(Aluno model)
        {
            try
            {
                using (AcademiaContext conexao = new AcademiaContext())
                {
                    string acao = "";
                    if (model.ID > 0)
                    {
                        conexao.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        acao = "Aluno alterado com sucesso!";
                    }
                    else
                    {
                        conexao.Entry(model).State = System.Data.Entity.EntityState.Added;
                        acao = "Aluno cadastrado com sucesso!";
                    }

                    TempData["msgAluno"] = acao;
                    conexao.SaveChanges();

                    return RedirectToAction("GerenciarAluno");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult AlterarAluno(int id)
        {
            try
            {
                using (AcademiaContext conexao = new AcademiaContext())
                {
                    var model = conexao.Aluno.Find(id);
                    return View("CadastrarAluno", model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetAlunos(JQueryDataTableParamModel _modelFilter)
        {
            using (AcademiaContext conexao = new AcademiaContext())
            {
                List<Aluno> qry = conexao.Aluno.OrderBy(x=> x.ID).ToList();

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

                var Source = qry.Select(x => new AlunoModel
                {

                    ID = x.ID,
                    Nome = x.Nome,
                    Identidade = x.Identidade,
                    CPF = x.CPF,
                    Situacao = ""

                });

                int totalRegistros = 0;
                List<AlunoModel> result = new PaginatedData<AlunoModel>(Source, _modelFilter.iDisplayStart, _modelFilter.iDisplayLength, ref totalRegistros);

                foreach (var item in result)
                {
                    string[] array = { "Inadimplente", "Adimplente"};
                    Random random = new Random();
                    int posicao = random.Next(0, array.Count());
                    item.Situacao =  array[posicao].ToString();
                }

                var Resultado = new
                {
                    sEcho = _modelFilter.sEcho,
                    iTotalRecords = totalRegistros,
                    iTotalDisplayRecords = totalRegistros,
                    aaData = result
                };
                return Json(Resultado, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult ExcluirAluno(int id)
        {
            using (AcademiaContext conexao = new AcademiaContext())
            {
                try
                {
                    var AlunoModel = conexao.Aluno.Find(id);

                    conexao.Aluno.Remove(AlunoModel);
                    conexao.SaveChanges();

                    TempData["msgAluno"] = "Aluno excluido com sucesso!";
                    return RedirectToAction("GerenciarAluno");
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }

    public class AlunoModel
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Identidade { get; set; }

        public string CPF { get; set; }

        public string Situacao { get; set; }

        public string ProximoPagamento { get; set; }

        public DateTime? ProximoPagamentoSemFormat { get; set; }
    }
}