using GraduateDeveloperAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraduateDeveloperAssignment.Controllers
{
    public class viewData{

      private  List<Client> clients = new List<Client>();
       private List<Transaction> transactions = new List<Transaction>();

        public viewData(List<Client> clients, List<Transaction> transactions)
        {
            this.clients = clients;
            this.transactions = transactions;
        }

        public List<Client> Clients { get => clients; set => clients = value; }
        public List<Transaction> Transactions { get => transactions; set => transactions = value; }
    }


    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? clientID) {


            GraduateDevAssignmentEntities gradDB = new GraduateDevAssignmentEntities();

            var lstClients = gradDB.Clients.ToList();

            var lstTransactoin = gradDB.Transactions.Where(x => x.ClientID == clientID || clientID == null).ToList();


            viewData indexView = new viewData(lstClients, lstTransactoin);

            return View(indexView);

        }


        [HttpGet]
        public ActionResult AddTransaction() {

            GraduateDevAssignmentEntities gradDB = new GraduateDevAssignmentEntities();

            var lstClients = gradDB.Clients.ToList();

            return View(lstClients);
        }

        [HttpPost]
        public ActionResult AddTransaction(int clientID , short transTypeID , decimal amount , string comment ) {

            Transaction aTransaction = new Transaction();
            GraduateDevAssignmentEntities gradDB = new GraduateDevAssignmentEntities(); 
           
            aTransaction.ClientID = clientID;
            aTransaction.Comment = comment;
            aTransaction.TransactionTypeId = transTypeID;

            if (transTypeID == 2 ) {
                aTransaction.Amount = amount * -1 ;
            }
            else {
                aTransaction.Amount = amount; 
            }

            gradDB.Transactions.Add(aTransaction);
            gradDB.SaveChanges();


            return RedirectToAction("Index");



        }

        [HttpGet]
        public ActionResult Edit(int? id) {


            GraduateDevAssignmentEntities dbGrad = new GraduateDevAssignmentEntities();

            Transaction transaction = dbGrad.Transactions.Where(x => x.TransactionID == id).FirstOrDefault();

            Session["tranactionID"] = transaction.TransactionID; 


            return View(transaction);



        }

        [HttpPost]
        public ActionResult Edit(string comment)
        {

            GraduateDevAssignmentEntities dbGrad = new GraduateDevAssignmentEntities();


            int id = Convert.ToInt32(Session["tranactionID"].ToString()); 

            var val = dbGrad.Transactions.Single(x => x.TransactionID == id);


            val.Comment = comment;

            dbGrad.SaveChanges();




            return RedirectToAction("Index");
        }












    }
}