using DetonaFit.Contexts;
using DetonaFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetonaFit.Controllers
{
    public class RelatoriosController : Controller
    {
        public ActionResult Relatorio()
        {
            return View();
        }

        public JsonResult GetAlunosMatriculados(JQueryDataTableParamModel _modelFilter)
        {
            using (AcademiaContext conexao = new AcademiaContext())
            {
                List<Aluno> qry = conexao.Aluno.OrderBy(x => x.ID).ToList();

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
                    Situacao = "",
                    ProximoPagamento = ""

                });



                int totalRegistros = 0;
                List<AlunoModel> result = new PaginatedData<AlunoModel>(Source, _modelFilter.iDisplayStart, _modelFilter.iDisplayLength, ref totalRegistros);

                int mesAnterior = 0;
                int diaAnterior = 0;

                foreach (var item in result)
                {
                    Random random = new Random();

                    int dia = 0;
                    int mes = 0;

                    dia = random.Next(1, 30);

                    mes = random.Next(5, 12);

                    item.ProximoPagamentoSemFormat = new DateTime(2020, mes, dia);

                    item.ProximoPagamento = string.Format("{0:dd/MM/yyyy}", item.ProximoPagamentoSemFormat);



                    string[] array = { "Inadimplente", "Adimplente" };

                    int posicao = random.Next(0, array.Count());

                    item.Situacao = array[posicao].ToString();

                    diaAnterior = dia;
                    mesAnterior = mes;
                }

                //ordenacao
                if (coluna == 5)
                {
                    if (direcao == "asc")
                    {
                        result = result.OrderBy(x => x.Nome).ToList();
                    }
                    else
                    {
                        result = result.OrderByDescending(x => x.Nome).ToList();
                    }
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
    }
}